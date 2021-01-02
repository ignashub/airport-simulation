using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProCP_App
{
    public class Belt
    {
        public List<Cell[]> Path { get; private set; }
        public int Passed { get; private set; }
        public int MaxLuggage
        {
            get { return maxLuggage; }
            set { maxLuggage = value; }
        }
        private List<Luggage> movingLuggages = new List<Luggage>();
        private List<Luggage> availableLuggages = new List<Luggage>();
        private List<List<Point>> _pathLocations = new List<List<Point>>();
        //Luggages inputted in the input form
        public int maxLuggage = 0;
        private const int LUGGAGE_POS_STEP = 2;
        private const int LUGGAGE_WIDTH = 16;
        private const int LUGGAGE_HEIGHT = 16;
        private const int PICBOX_DIMENSIONS = 90;
        private const double CELL_MID_REF = PICBOX_DIMENSIONS / 2 + 0.5 - LUGGAGE_WIDTH / 2;
        private int timerTicks = 0;
        private int lugsPassedFirstCell = 0;
        //find active exits for the sorting area
        public List<int> activeExits = new List<int>();

        public Belt(int maxLug)
        {
            Path = new List<Cell[]>();
            this.maxLuggage = maxLug;
        }

        public void FindPath(ref List<Cell> cells, ref Cell c, List<Cell> grid)
        {
            bool error = false;
            bool reverse = true;
            while (!((cells[cells.Count - 1].Tag == BeltType.HORIZONTAL || cells[cells.Count - 1].Tag == BeltType.UPRIGHT || cells[cells.Count - 1].Tag == BeltType.DOWNRIGHT) && cells[cells.Count - 1].Col == 6))
            {
                switch (c.Tag)
                {
                    case BeltType.RIGHTDOWN:
                        reverse = true;
                        if (cells[cells.Count - 2].Tag == BeltType.HORIZONTAL || cells[cells.Count - 2].Tag == BeltType.UPRIGHT || (cells[cells.Count - 2].Tag == BeltType.DOWNRIGHT && !cells[cells.Count - 2].Reverse)) reverse = false;
                        foreach (Cell cell in grid)
                        {
                            if (!reverse)
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", (c.Row + 1), c.Col))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.HORIZONTAL || cell.Tag == BeltType.UPRIGHT || cell.Tag == BeltType.RIGHTDOWN)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col - 1)))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.RIGHTUP || cell.Tag == BeltType.RIGHTDOWN)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case BeltType.UPRIGHT:
                        reverse = false;
                        if (cells[cells.Count - 2].Tag == BeltType.HORIZONTAL || (cells[cells.Count - 2].Tag == BeltType.RIGHTUP && cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == BeltType.RIGHTDOWN) reverse = true;
                        foreach (Cell cell in grid)
                        {
                            if (!reverse)
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col + 1)))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.DOWNRIGHT || cell.Tag == BeltType.UPRIGHT)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", (c.Row + 1), c.Col))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.HORIZONTAL || cell.Tag == BeltType.RIGHTDOWN || cell.Tag == BeltType.UPRIGHT)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case BeltType.RIGHTUP:
                        reverse = true;
                        if (cells[cells.Count - 2].Tag == BeltType.HORIZONTAL || (cells[cells.Count - 2].Tag == BeltType.UPRIGHT && !cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == BeltType.DOWNRIGHT) reverse = false;
                        foreach (Cell cell in grid)
                        {
                            if (!reverse)
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", (c.Row - 1), c.Col))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.HORIZONTAL || cell.Tag == BeltType.RIGHTUP || cell.Tag == BeltType.DOWNRIGHT)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col - 1)))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.RIGHTDOWN || cell.Tag == BeltType.RIGHTUP)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case BeltType.DOWNRIGHT:
                        reverse = false;
                        if (cells[cells.Count - 2].Tag == BeltType.HORIZONTAL || (cells[cells.Count - 2].Tag == BeltType.RIGHTDOWN && cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == BeltType.RIGHTUP) reverse = true;
                        foreach (Cell cell in grid)
                        {
                            if (!reverse)
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col + 1)))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.UPRIGHT || cell.Tag == BeltType.DOWNRIGHT)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", (c.Row - 1), c.Col))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.HORIZONTAL || cell.Tag == BeltType.RIGHTUP || cell.Tag == BeltType.DOWNRIGHT)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case BeltType.HORIZONTAL:
                        reverse = true;
                        if ((cells[cells.Count - 2].Tag == BeltType.HORIZONTAL && !cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == BeltType.DOWNRIGHT || cells[cells.Count - 2].Tag == BeltType.UPRIGHT) reverse = false;
                        foreach (Cell cell in grid)
                        {
                            if (!reverse)
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col + 1)))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.DOWNRIGHT || cell.Tag == BeltType.UPRIGHT)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col - 1)))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.RIGHTUP || cell.Tag == BeltType.RIGHTDOWN)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case BeltType.VERTICAL:
                        reverse = true;
                        if ((cells[cells.Count - 2].Tag == BeltType.VERTICAL && !cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == BeltType.DOWNRIGHT || cells[cells.Count - 2].Tag == BeltType.RIGHTUP) reverse = false;
                        foreach (Cell cell in grid)
                        {
                            if (!reverse)
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", (c.Row - 1), c.Col))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.HORIZONTAL || cell.Tag == BeltType.DOWNRIGHT || cell.Tag == BeltType.RIGHTUP)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", (c.Row + 1), c.Col))
                                {
                                    if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.HORIZONTAL || cell.Tag == BeltType.RIGHTDOWN || cell.Tag == BeltType.RIGHTDOWN)
                                    {
                                        this.DrawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        AddCellWithLocations(c, ref cells, reverse);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                }
                if (error)
                {
                    throw new Exception("Inappropriate piece");
                }
            }
            List<Point> path = new List<Point>();
            this.activeExits.Add(cells[cells.Count - 1].Row);
            this.Path.Add(cells.ToArray());
            foreach (Cell cell in cells)
            {
                path.AddRange(FindCellPathPoints(cell));
            }
            _pathLocations.Add(path);
        }
        public void AddCellWithLocations(Cell c, ref List<Cell> cells, bool reverse)
        {
            cells[cells.Count - 1].Reverse = reverse;
            cells[cells.Count - 1].NextCell = c;
            cells.Add(c);
        }
        private List<Point> FindCellPathPoints(Cell c)
        {
            List<Point> cellPoints = new List<Point>();
            // Calculates the offset in the panel
            int abs_y = (c.Row - 1) * c.PicBox.Height;
            int abs_x = (c.Col - 1) * c.PicBox.Width;
            switch (c.Tag)
            {
                case BeltType.RIGHTDOWN:
                    // Horizontal points
                    for (int i = 0; i <= (int)CELL_MID_REF; i = i + LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + i, abs_y + (int)CELL_MID_REF);
                        cellPoints.Add(p);
                    }
                    // Vertical points
                    for (int i = (int)CELL_MID_REF; i <= c.PicBox.Height; i = i + LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + (int)CELL_MID_REF, abs_y + i);
                        cellPoints.Add(p);
                    }
                    break;
                case BeltType.UPRIGHT:
                    // Vertical points
                    for (int i = c.PicBox.Height; i >= (int)CELL_MID_REF; i = i - LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + (int)CELL_MID_REF, abs_y + i);
                        cellPoints.Add(p);
                    }
                    // Horizontal points
                    for (int i = (int)CELL_MID_REF; i <= PICBOX_DIMENSIONS; i = i + LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + i, abs_y + (int)CELL_MID_REF);
                        cellPoints.Add(p);
                    }
                    break;
                case BeltType.RIGHTUP:
                    // Horizontal points
                    for (int i = 0; i <= (int)CELL_MID_REF; i = i + LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + i, abs_y + (int)CELL_MID_REF);
                        cellPoints.Add(p);
                    }
                    // Vertical points
                    for (int i = (int)CELL_MID_REF; i >= 0; i = i - LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + (int)CELL_MID_REF, abs_y + i);
                        cellPoints.Add(p);
                    }
                    break;
                case BeltType.DOWNRIGHT:
                    // Vertical points
                    for (int i = 0; i <= (int)CELL_MID_REF; i = i + LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + (int)CELL_MID_REF, abs_y + i);
                        cellPoints.Add(p);
                    }
                    // Horizontal points
                    for (int i = (int)CELL_MID_REF; i <= c.PicBox.Width; i = i + LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + i, abs_y + (int)CELL_MID_REF);
                        cellPoints.Add(p);
                    }
                    break;
                case BeltType.HORIZONTAL:
                    for (int i = 0; i <= c.PicBox.Width; i = i + LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + i, abs_y + (int)CELL_MID_REF);
                        cellPoints.Add(p);
                    }
                    break;
                case BeltType.VERTICAL:
                    for (int i = c.PicBox.Height; i >= 0; i = i - LUGGAGE_POS_STEP)
                    {
                        Point p = new Point(abs_x + (int)CELL_MID_REF, abs_y + i);
                        cellPoints.Add(p);
                    }
                    break;
            }
            if (c.Reverse)
            {
                cellPoints.Reverse();
            }
            return cellPoints;
        }

        public void MoveLuggagesOnBelt(Panel panel)
        {
            if (timerTicks == PICBOX_DIMENSIONS / LUGGAGE_POS_STEP * lugsPassedFirstCell)
            {
                lugsPassedFirstCell++;
                AddToMovingLuggages();
            }
            else
            {
                timerTicks++;
            }
            for (int i = 0; i < _pathLocations.Count; i++)
            {
                List<Point> points = _pathLocations[i];
                for (int k = 0; k < movingLuggages.Count; k++)
                {
                    if (movingLuggages[k].PathID == i)
                    {
                        if (movingLuggages[k].PathLocationIndex < points.Count - 1)
                        {
                            RedrawPrevLugLocation(panel, k, points);
                            movingLuggages[k].PathLocationIndex++;
                            DrawLuggages(panel, movingLuggages[k]);
                            movingLuggages[k].UpdateLuggageLocation(points[movingLuggages[k].PathLocationIndex]);
                        }
                        else
                        {
                            movingLuggages.RemoveAt(k);
                            Passed++;
                        }
                    }
                }
            }
        }
        public void PopulateAvailableLuggages()
        {
            Random randomId = new Random();
            for (int i = 0; i < maxLuggage; i++)
            {
                int id = randomId.Next(Path.Count);
                availableLuggages.Add(new Luggage(_pathLocations[id][0], id));
            }
        }
        public void AddToMovingLuggages()
        {
            if (availableLuggages.Count > 0)
            {
                // Move luggage from available to moving
                movingLuggages.Add(availableLuggages[0]);
                availableLuggages.RemoveAt(0);
            }
        }
        private void RedrawPrevLugLocation(Panel panel, int index, List<Point> points)
        {
            Point p;
            if (movingLuggages[index].PathLocationIndex == 0)
            {
                p = points[movingLuggages[index].PathLocationIndex];
            }
            else
            {
                p = points[movingLuggages[index].PathLocationIndex - 1];
            }
            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), p.X, p.Y, 16, 16);
        }
        private void DrawLuggages(Panel panel, Luggage luggage)
        {
            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), luggage.Rectangle);
        }
        public void DrawPath(Panel panel)
        {
            int abs_x = 0;
            int abs_y = 0;
            for (int i = 0; i < Path.Count; i++)
            {
                Cell[] cells = Path[i];
                for (int k = 0; k < cells.Length; k++)
                {
                    /* Relative position inside panel 
                       Every cell has a width and height of 90
                       So the col or row multiplied by that gives the offset */
                    abs_x = (cells[k].Col - 1) * PICBOX_DIMENSIONS;
                    abs_y = (cells[k].Row - 1) * PICBOX_DIMENSIONS;
                    PictureBox pb = cells[k].PicBox;
                    switch (cells[k].Tag)
                    {
                        case BeltType.RIGHTDOWN:
                            // Horizontal part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x, abs_y + pb.Height / 3, pb.Width * 2 / 3, pb.Height / 3);
                            // Vertical part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x + pb.Width / 3, abs_y + pb.Height / 2, pb.Width / 3, pb.Height / 2);
                            break;
                        case BeltType.UPRIGHT:
                            // Horizontal part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x + pb.Width / 3, abs_y + pb.Height / 3, pb.Width * 2 / 3, pb.Height / 3);
                            // Vertical part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x + pb.Width / 3, abs_y + pb.Height / 2, pb.Width / 3, pb.Height / 2);
                            break;
                        case BeltType.RIGHTUP:
                            // Horizontal part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x, abs_y + pb.Height / 3, pb.Width * 2 / 3, pb.Height / 3);
                            // Vertical part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x + pb.Width / 3, abs_y, pb.Width / 3, pb.Height / 2);
                            break;
                        case BeltType.DOWNRIGHT:
                            // Horizontal part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x + pb.Width / 3, abs_y + pb.Height / 3, pb.Width * 2 / 3, pb.Height / 3);
                            // Vertical part
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x + pb.Width / 3, abs_y, pb.Width / 3, pb.Height / 2);
                            break;
                        case BeltType.HORIZONTAL:
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x, abs_y + pb.Height / 3, pb.Width, pb.Height / 3);
                            break;
                        case BeltType.VERTICAL:
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), abs_x + pb.Width / 3, abs_y, pb.Width / 3, pb.Height);
                            break;
                    }
                    switch (cells[k].Desk.Type)
                    {
                        case DeskType.TOPLEFT:
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), abs_x + pb.Width / 9, abs_y + pb.Height / 9, pb.Width / 9 * 2, pb.Height / 9 * 2);
                            break;
                        case DeskType.TOPRIGHT:
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), abs_x + pb.Width / 9 * 6, abs_y + pb.Height / 9, pb.Width / 9 * 2, pb.Height / 9 * 2);
                            break;
                        case DeskType.BOTTOMLEFT:
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), abs_x + pb.Width / 9, abs_y + pb.Height / 9 * 6, pb.Width / 9 * 2, pb.Height / 9 * 2);
                            break;
                        case DeskType.BOTTOMRIGHT:
                            panel.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), abs_x + pb.Width / 9 * 6, abs_y + pb.Height / 9 * 6, pb.Width / 9 * 2, pb.Height / 9 * 2);
                            break;
                    }
                }
            }
        }

        public void DrawError(Cell cell)
        {
            PictureBox pictureBox = cell.PicBox;
            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Red), 0, 0, pictureBox.Width, pictureBox.Height);
            switch (cell.Tag)
            {
                case BeltType.RIGHTDOWN:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 2, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case BeltType.UPRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 2, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case BeltType.RIGHTUP:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case BeltType.DOWNRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case BeltType.HORIZONTAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width, pictureBox.Height / 3);
                    pictureBox.Tag = BeltType.HORIZONTAL;
                    break;
                case BeltType.VERTICAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height);
                    break;
            }
        }

        public void ClearBelt()
        {
            this.Passed = 0;
            this.availableLuggages.Clear();
            this.movingLuggages.Clear();
            for (int i = 0; i < this.Path.Count; i++)
            {
                Cell[] cells = this.Path[i];
                Array.Clear(cells, 0, cells.Length);
            }
            Path.Clear();
            lugsPassedFirstCell = 0;
            timerTicks = 0;
        }
        public void RestartBelt()
        {
            this.movingLuggages.Clear();
            this.availableLuggages.Clear();
            this.Passed = 0;
            timerTicks = 0;
            lugsPassedFirstCell = 0;
            PopulateAvailableLuggages();
        }
    }
}