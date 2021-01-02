using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCP_App
{

    class Belt
    {
        public List<Cell[]> Path { get; private set; }
        public Color ControlDark { get; private set; }
        private int maxReached = 0;
        public int Passed { private set; get; }
        public List<Luggage> luggage = new List<Luggage>();
        private int maxLuggage = 0;
        public Belt(int lug)
        {
            Path = new List<Cell[]>();
            this.maxLuggage = lug;
        }

        public void findPath(List<Cell> cells, Cell c, List<Cell> emptyGrid)
        {
            bool error = false;
            bool reverse = true;
            while (!((cells[cells.Count - 1].Tag == Type.HORIZONTAL || cells[cells.Count - 1].Tag == Type.UPRIGHT || cells[cells.Count - 1].Tag == Type.DOWNRIGHT) && cells[cells.Count - 1].Name.Substring(1, 1) == "6"))
            {
                switch (c.Tag)
                {
                    case Type.RIGHTDOWN:
                        reverse = true;
                        if (cells[cells.Count - 2].Tag == Type.HORIZONTAL || cells[cells.Count - 2].Tag == Type.UPRIGHT || (cells[cells.Count - 2].Tag == Type.DOWNRIGHT && !cells[cells.Count - 2].Reverse)) reverse = false;
                        foreach (Cell cell in emptyGrid)
                        {
                            if (!reverse)
                            {
                                if (cell.Name == (Convert.ToInt32(c.Name.Substring(0, 1)) + 1).ToString() + c.Name.Substring(1, 1))
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.UPRIGHT || cell.Tag == Type.RIGHTDOWN)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) - 1).ToString())
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.VERTICAL || cell.Tag == Type.RIGHTUP || cell.Tag == Type.RIGHTDOWN)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case Type.UPRIGHT:
                        reverse = false;
                        if (cells[cells.Count - 2].Tag == Type.HORIZONTAL || (cells[cells.Count - 2].Tag == Type.RIGHTUP && cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == Type.RIGHTDOWN) reverse = true;
                        foreach (Cell cell in emptyGrid)
                        {
                            if (!reverse)
                            {
                                if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) + 1).ToString())
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.VERTICAL || cell.Tag == Type.DOWNRIGHT || cell.Tag == Type.UPRIGHT)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (cell.Name == (Convert.ToInt32(c.Name.Substring(0, 1)) + 1).ToString() + c.Name.Substring(1, 1))
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.RIGHTDOWN || cell.Tag == Type.UPRIGHT)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case Type.RIGHTUP:
                        reverse = true;
                        if (cells[cells.Count - 2].Tag == Type.HORIZONTAL || (cells[cells.Count - 2].Tag == Type.UPRIGHT && !cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == Type.DOWNRIGHT) reverse = false;
                        foreach (Cell cell in emptyGrid)
                        {
                            if (!reverse)
                            {
                                if (cell.Name == (Convert.ToInt32(c.Name.Substring(0, 1)) - 1).ToString() + c.Name.Substring(1, 1))
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.RIGHTUP || cell.Tag == Type.DOWNRIGHT)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) - 1).ToString())
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.VERTICAL || cell.Tag == Type.RIGHTDOWN || cell.Tag == Type.RIGHTUP)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case Type.DOWNRIGHT:
                        reverse = false;
                        if (cells[cells.Count - 2].Tag == Type.HORIZONTAL || (cells[cells.Count - 2].Tag == Type.RIGHTDOWN && cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == Type.RIGHTUP) reverse = true;
                        foreach (Cell cell in emptyGrid)
                        {
                            if (!reverse)
                            {
                                if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) + 1).ToString())
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.VERTICAL || cell.Tag == Type.UPRIGHT || cell.Tag == Type.DOWNRIGHT)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (cell.Name == (Convert.ToInt32(c.Name.Substring(0, 1)) - 1).ToString() + c.Name.Substring(1, 1))
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.RIGHTUP || cell.Tag == Type.DOWNRIGHT)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case Type.HORIZONTAL:
                        reverse = true;
                        if ((cells[cells.Count - 2].Tag == Type.HORIZONTAL && !cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == Type.DOWNRIGHT || cells[cells.Count - 2].Tag == Type.UPRIGHT) reverse = false;
                        foreach (Cell cell in emptyGrid)
                        {
                            if (!reverse)
                            {
                                if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) + 1).ToString())
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.VERTICAL || cell.Tag == Type.DOWNRIGHT || cell.Tag == Type.UPRIGHT)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) - 1).ToString())
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.VERTICAL || cell.Tag == Type.RIGHTUP || cell.Tag == Type.RIGHTDOWN)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    case Type.VERTICAL:
                        reverse = true;
                        if ((cells[cells.Count - 2].Tag == Type.VERTICAL && !cells[cells.Count - 2].Reverse) || cells[cells.Count - 2].Tag == Type.DOWNRIGHT || cells[cells.Count - 2].Tag == Type.RIGHTUP) reverse = false;
                        foreach (Cell cell in emptyGrid)
                        {
                            if (!reverse)
                            {
                                if (cell.Name == (Convert.ToInt32(c.Name.Substring(0, 1)) - 1).ToString() + c.Name.Substring(1, 1))
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.DOWNRIGHT || cell.Tag == Type.RIGHTUP)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                if (cell.Name == (Convert.ToInt32(c.Name.Substring(0, 1)) + 1).ToString() + c.Name.Substring(1, 1))
                                {
                                    if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.RIGHTDOWN || cell.Tag == Type.RIGHTDOWN)
                                    {
                                        this.drawError(cell);
                                        error = true;
                                    }
                                    else
                                    {
                                        c = cell;
                                        cells[cells.Count - 1].Reverse = reverse;
                                        cells[cells.Count - 1].nextCell = c;
                                        cells.Add(c);
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
            this.Path.Add(cells.ToArray());
        }

        public void drawError(Cell cell)
        {
            PictureBox pictureBox = cell.PicBox;
            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Red), 0, 0, pictureBox.Width, pictureBox.Height);
            switch (cell.Tag)
            {
                case Type.RIGHTDOWN:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 2, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case Type.UPRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 2, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case Type.RIGHTUP:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case Type.DOWNRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height / 2);
                    break;
                case Type.HORIZONTAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width, pictureBox.Height / 3);
                    pictureBox.Tag = Type.HORIZONTAL;
                    break;
                case Type.VERTICAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height);
                    break;
            }
        }

        public void clearBelt()
        {
            for (int i = 0; i < this.Path.Count; i++)
            {
                Cell[] cells = this.Path[i];
                foreach (Cell cell in cells)
                {
                    PictureBox pictureBox = cell.PicBox;
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(ControlDark), 0, 0, pictureBox.Width, pictureBox.Height);
                    switch (cell.Tag)
                    {
                        case Type.RIGHTDOWN:
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 2, pictureBox.Width / 3, pictureBox.Height / 2);
                            break;
                        case Type.UPRIGHT:
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 2, pictureBox.Width / 3, pictureBox.Height / 2);
                            break;
                        case Type.RIGHTUP:
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height / 2);
                            break;
                        case Type.DOWNRIGHT:
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, pictureBox.Height / 3, pictureBox.Width * 2 / 3, pictureBox.Height / 3);
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height / 2);
                            break;
                        case Type.HORIZONTAL:
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pictureBox.Height / 3, pictureBox.Width, pictureBox.Height / 3);
                            pictureBox.Tag = Type.HORIZONTAL;
                            break;
                        case Type.VERTICAL:
                            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), pictureBox.Width / 3, 0, pictureBox.Width / 3, pictureBox.Height);
                            break;
                    }
                }
            }
            this.Passed = 0;
            this.maxReached = 0;
            this.luggage = new List<Luggage>();
            for (int i = 0; i < this.Path.Count; i++)
            {
                Cell[] cells = this.Path[i];
                foreach (Cell c in cells)
                {
                    c.Luggage = null;
                }
            }
        }

        public void MoveLuggage()
        {
            if (this.luggage.Count < this.maxLuggage)
            {
                for (int i = 0; i < this.Path.Count; i++)
                {
                    Cell[] cells = this.Path[i];
                    for (int k = 0; k < cells.Length; k++)
                    {
                        if (cells[k].Luggage == null)
                        {
                            if (k == 0 && this.luggage.Count == 0)
                            {
                                this.AddLuggage();
                                this.luggage.Add(cells[k].Luggage);
                            }
                        }
                        else
                        {
                            switch (cells[k].Tag)
                            {
                                case Type.RIGHTDOWN:
                                    if(!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.Y == cells[k].PicBox.Height + 1)
                                        {
                                           
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        if (cells[k].Luggage.position.X <= 37)
                                        {
                                            cells[k].Luggage.updateLocation(2, 0);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(0, 2);
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.X == -19)
                                        {

                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);

                                        if (cells[k].Luggage.position.Y >= 37)
                                        {
                                            cells[k].Luggage.updateLocation(0, -2);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(-2, 0);
                                        }
                                    }
                                    break;
                                case Type.UPRIGHT:
                                    if(!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width + 1)
                                        {
                                            if (maxReached >= cells.Length - 1)
                                            {
                                                this.Passed++;
                                            }
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        if (cells[k].Luggage.position.Y >= 37)
                                        {
                                            cells[k].Luggage.updateLocation(0, -2);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(2, 0);
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].PicBox.Height + 1 == cells[k].Luggage.position.Y)
                                        {

                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        if (cells[k].Luggage.position.X >= 37)
                                        {
                                            cells[k].Luggage.updateLocation(-2, 0);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(0, 2);
                                        }
                                    }
                                    break;
                                case Type.RIGHTUP:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.Y == -19)
                                        {
                                            if (maxReached >= cells.Length - 1)
                                            {
                                                this.Passed++;
                                            }
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);

                                        if (cells[k].Luggage.position.X <= 37)
                                        {
                                            cells[k].Luggage.updateLocation(2, 0);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(0, -2);
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.X == -19)
                                        {

                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        if (cells[k].Luggage.position.Y <= 37)
                                        {
                                            cells[k].Luggage.updateLocation(0, 2);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(-2, 0);
                                        }
                                    }
                                    break;
                                case Type.DOWNRIGHT:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width + 1)
                                        {
                                            if (maxReached >= cells.Length - 1)
                                            {
                                                this.Passed++;
                                            }
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);

                                        if (cells[k].Luggage.position.Y <= 37)
                                        {
                                            cells[k].Luggage.updateLocation(0, 2);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(2, 0);
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.Y == -19)
                                        {

                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        if (cells[k].Luggage.position.X >= 37)
                                        {
                                            cells[k].Luggage.updateLocation(-2, 0);
                                        }
                                        else
                                        {
                                            cells[k].Luggage.updateLocation(0, -2);
                                        }
                                    }
                                    break;
                                case Type.HORIZONTAL:
                                    if(!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width)
                                        {
                                            if (k == 0)
                                            {
                                                this.luggage.Add(cells[k].Luggage);
                                            }
                                            if (maxReached >= cells.Length - 1)
                                            {
                                                this.Passed++;
                                            }
                                            if (this.luggage.Count <= this.maxLuggage)
                                            {
                                                this.maxReached++;
                                                this.AddLuggage();
                                            }
                                            else
                                            {
                                                cells[k].Luggage = null;
                                            }
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        cells[k].Luggage.updateLocation(2, 0);
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width)
                                        {
                                            if (k == 0)
                                            {
                                                this.luggage.Add(cells[k].Luggage);
                                            }
                                            if (this.luggage.Count <= this.maxLuggage)
                                            {
                                                this.maxReached++;
                                                this.AddLuggage();
                                            }
                                            else
                                            {
                                                cells[k].Luggage = null;
                                            }
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width, cells[k].PicBox.Height / 3);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        cells[k].Luggage.updateLocation(-2, 0);
                                    }
                                    break;
                                case Type.VERTICAL:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.Y == -18)
                                        {
                                            if (k == 0)
                                            {
                                                this.luggage.Add(cells[k].Luggage);
                                            }
                                            if (maxReached >= cells.Length - 1)
                                            {
                                                this.Passed++;
                                            }
                                            if (this.luggage.Count <= this.maxLuggage)
                                            {
                                                this.maxReached++;
                                                this.AddLuggage();
                                            }
                                            else
                                            {
                                                cells[k].Luggage = null;
                                            }
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        cells[k].Luggage.updateLocation(0, -2);
                                    }
                                    else
                                    {
                                        if (cells[k].PicBox.Height == cells[k].Luggage.position.Y)
                                        {
                                            if (k == 0)
                                            {
                                                this.luggage.Add(cells[k].Luggage);
                                            }
                                            if (this.luggage.Count <= this.maxLuggage)
                                            {
                                                this.maxReached++;
                                                this.AddLuggage();
                                            }
                                            else
                                            {
                                                cells[k].Luggage = null;
                                            }
                                        }
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height);
                                        cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                        cells[k].Luggage.updateLocation(0, 2);
                                    }
                                    break;

                            }       
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.Path.Count; i++)
                {
                    Cell[] cells = this.Path[i];
                    for (int k = cells.Length - 1; k >= 0; k--)
                    {
                        if (cells[k].Luggage != null)
                        {
                            switch (cells[k].Tag)
                            {
                                case Type.RIGHTDOWN:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.Y == cells[k].PicBox.Height + 1)
                                        {
                                            if (cells.Length - 1 >= k + 1)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width / 2 - 8, 0);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            if (cells[k].Luggage.position.X <= 37)
                                            {
                                                cells[k].Luggage.updateLocation(2, 0);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(0, 2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.X == -19)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width - 16, cells[k].PicBox.Height / 2 - 8);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);

                                            if (cells[k].Luggage.position.Y >= 37)
                                            {
                                                cells[k].Luggage.updateLocation(0, -2);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(-2, 0);
                                            }
                                        }
                                    }
                                    break;
                                case Type.UPRIGHT:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width + 1)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(0, cells[k].PicBox.Height / 2 - 8);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                            if (k == cells.Length - 1)
                                            {
                                                this.Passed++;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            if (cells[k].Luggage.position.Y >= 37)
                                            {
                                                cells[k].Luggage.updateLocation(0, -2);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(2, 0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].PicBox.Height + 1 == cells[k].Luggage.position.Y)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width / 2 - 8, 0);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            if (cells[k].Luggage.position.X >= 37)
                                            {
                                                cells[k].Luggage.updateLocation(-2, 0);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(0, 2);
                                            }
                                        }
                                    }
                                    break;
                                case Type.RIGHTUP:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.Y == -19)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width / 2 - 8, cells[k].PicBox.Height - 16);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);

                                            if (cells[k].Luggage.position.X <= 37)
                                            {
                                                cells[k].Luggage.updateLocation(2, 0);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(0, -2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.X == -19)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width - 16, cells[k].PicBox.Height / 2 - 8);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            if (cells[k].Luggage.position.Y <= 37)
                                            {
                                                cells[k].Luggage.updateLocation(0, 2);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(-2, 0);
                                            }
                                        }
                                    }
                                    break;
                                case Type.DOWNRIGHT:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width + 1)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(0, cells[k].PicBox.Height / 2 - 8);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);

                                            if (cells[k].Luggage.position.Y <= 37)
                                            {
                                                cells[k].Luggage.updateLocation(0, 2);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(2, 0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.Y == -19)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width / 2 - 8, cells[k].PicBox.Height - 16);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 3, cells[k].PicBox.Width * 2 / 3, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height / 2);

                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            if (cells[k].Luggage.position.X >= 37)
                                            {
                                                cells[k].Luggage.updateLocation(-2, 0);
                                            }
                                            else
                                            {
                                                cells[k].Luggage.updateLocation(0, -2);
                                            }
                                        }
                                    }
                                    break;
                                case Type.HORIZONTAL:
                                    if(!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(0, cells[k + 1].PicBox.Height / 2 - 8);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                            if (k == cells.Length - 1)
                                            {
                                                this.Passed++;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            cells[k].Luggage.updateLocation(2, 0);
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].Luggage.position.X == cells[k].PicBox.Width)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width - 16, cells[k].PicBox.Height / 2 - 8);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, cells[k].PicBox.Height / 3, cells[k].PicBox.Width, cells[k].PicBox.Height / 3);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            cells[k].Luggage.updateLocation(-2, 0);
                                        }
                                    }
                                    break;
                                case Type.VERTICAL:
                                    if (!cells[k].Reverse)
                                    {
                                        if (cells[k].Luggage.position.Y == -18)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width / 2 - 8, cells[k].PicBox.Height - 16);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            cells[k].Luggage.updateLocation(0, -2);
                                        }
                                    }
                                    else
                                    {
                                        if (cells[k].PicBox.Height == cells[k].Luggage.position.Y)
                                        {
                                            if (cells.Length - 1 > k)
                                            {
                                                Point p = new Point(cells[k].PicBox.Width / 2 - 8, 0);
                                                cells[k + 1].Luggage = new Luggage(p.X, p.Y);
                                                cells[k].Luggage = null;
                                                this.Path[i] = cells;
                                            }
                                        }
                                        else
                                        {
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), cells[k].PicBox.Width / 3, 0, cells[k].PicBox.Width / 3, cells[k].PicBox.Height);
                                            cells[k].PicBox.CreateGraphics().FillRectangle(new SolidBrush(Color.Brown), cells[k].Luggage.position.X, cells[k].Luggage.position.Y, 16, 16);
                                            cells[k].Luggage.updateLocation(0, 2);
                                        }
                                    }
                                    break;
                            } 
                        }
                    }
                }
            }
        }

        public void AddLuggage()
        {
            for (int i = 0; i < this.Path.Count; i++)
            {
                Cell[] cells = this.Path[i];
                for (int m = 0; m < cells.Length; m++)
                {
                    //check if all luggages are processed already
                    if (m <= maxReached)
                    {
                        //get starting position for each piece
                        switch (cells[m].Tag)
                        {
                            case Type.RIGHTDOWN:
                                if (!cells[m].Reverse)
                                {
                                    Point p = new Point(0, cells[m].PicBox.Height / 2 - 8);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                else
                                {
                                    Point p = new Point(cells[m].PicBox.Width / 2 - 8, cells[m].PicBox.Height - 16);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                break;
                            case Type.UPRIGHT:
                                if (!cells[m].Reverse)
                                {
                                    Point p = new Point(cells[m].PicBox.Width / 2 - 8, cells[m].PicBox.Height - 16);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                else
                                {
                                    Point p = new Point(cells[m].PicBox.Width - 16, cells[m].PicBox.Height / 2 - 8);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                break;
                            case Type.RIGHTUP:
                                if (!cells[m].Reverse)
                                {
                                    Point p = new Point(0, cells[m].PicBox.Height / 2 - 8);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                else
                                {
                                    Point p = new Point(cells[m].PicBox.Width / 2 - 8, 0);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                break;
                            case Type.DOWNRIGHT:
                                if (!cells[m].Reverse)
                                {
                                    Point p = new Point(cells[m].PicBox.Width / 2 - 8, 0);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                else
                                {
                                    Point p = new Point(cells[m].PicBox.Width - 16, cells[m].PicBox.Height / 2 - 8);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                break;
                            case Type.HORIZONTAL:
                                if (!cells[m].Reverse)
                                {
                                    Point p = new Point(0, cells[m].PicBox.Height / 2 - 8);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                else
                                {
                                    Point p = new Point(cells[m].PicBox.Width - 16, cells[m].PicBox.Height / 2 - 8);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                break;
                            case Type.VERTICAL:
                                if (!cells[m].Reverse)
                                {
                                    Point p = new Point(cells[m].PicBox.Width / 2 - 8, cells[m].PicBox.Height - 16);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                else
                                {
                                    Point p = new Point(cells[m].PicBox.Width / 2 - 8, 0);
                                    cells[m].Luggage = new Luggage(p.X, p.Y);
                                }
                                break;
                        }
                    }
                }
                this.Path[i] = cells;
            }
        }

    }
}
