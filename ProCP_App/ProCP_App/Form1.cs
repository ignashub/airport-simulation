using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace ProCP_App
{
    public partial class Form1 : Form
    {
        public Airport airport = null;
        public List<Cell> grid;
        private Piece piece;
        private Desk desk;
        PictureBox pictureBox;
        int firstColumnPieces;
        int lugs = 0;
        int percentage;
        public bool hasStart;
        int tempCartCapacity;
        //private SortingArea area;
        public Color ControlDark { get; private set; }

        double busyCartsTimerSpeed;

        public System.Windows.Forms.Timer GetTimer { get { return this.timer; } }

        public Form1()
        {
            InitializeComponent();
            this.grid = new List<Cell>();
            this.grid.Add(new Cell(this.pb11, this.pb11.Name.Substring(2, 2), 1, 1, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb12, this.pb12.Name.Substring(2, 2), 1, 2, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb13, this.pb13.Name.Substring(2, 2), 1, 3, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb14, this.pb14.Name.Substring(2, 2), 1, 4, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb15, this.pb15.Name.Substring(2, 2), 1, 5, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb16, this.pb16.Name.Substring(2, 2), 1, 6, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb21, this.pb21.Name.Substring(2, 2), 2, 1, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb22, this.pb22.Name.Substring(2, 2), 2, 2, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb23, this.pb23.Name.Substring(2, 2), 2, 3, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb24, this.pb24.Name.Substring(2, 2), 2, 4, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb25, this.pb25.Name.Substring(2, 2), 2, 5, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb26, this.pb26.Name.Substring(2, 2), 2, 6, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb31, this.pb31.Name.Substring(2, 2), 3, 1, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb32, this.pb32.Name.Substring(2, 2), 3, 2, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb33, this.pb33.Name.Substring(2, 2), 3, 3, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb34, this.pb34.Name.Substring(2, 2), 3, 4, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb35, this.pb35.Name.Substring(2, 2), 3, 5, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb36, this.pb36.Name.Substring(2, 2), 3, 6, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb41, this.pb41.Name.Substring(2, 2), 4, 1, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb42, this.pb42.Name.Substring(2, 2), 4, 2, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb43, this.pb43.Name.Substring(2, 2), 4, 3, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb44, this.pb44.Name.Substring(2, 2), 4, 4, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb45, this.pb45.Name.Substring(2, 2), 4, 5, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb46, this.pb46.Name.Substring(2, 2), 4, 6, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb51, this.pb51.Name.Substring(2, 2), 5, 1, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb52, this.pb52.Name.Substring(2, 2), 5, 2, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb53, this.pb53.Name.Substring(2, 2), 5, 3, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb54, this.pb54.Name.Substring(2, 2), 5, 4, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb55, this.pb55.Name.Substring(2, 2), 5, 5, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb56, this.pb56.Name.Substring(2, 2), 5, 6, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb61, this.pb61.Name.Substring(2, 2), 6, 1, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb62, this.pb62.Name.Substring(2, 2), 6, 2, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb63, this.pb63.Name.Substring(2, 2), 6, 3, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb64, this.pb64.Name.Substring(2, 2), 6, 4, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb65, this.pb65.Name.Substring(2, 2), 6, 5, new Desk(DeskType.EMPTY)));
            this.grid.Add(new Cell(this.pb66, this.pb66.Name.Substring(2, 2), 6, 6, new Desk(DeskType.EMPTY)));
            foreach (Cell c in this.grid)
            {
                c.PicBox.BorderStyle = BorderStyle.FixedSingle;
                c.PicBox.Tag = BeltType.EMPTY;
            }
            panelGrid.BorderStyle = BorderStyle.FixedSingle;
            panelGrid.BackColor = SystemColors.ControlDark;
            firstColumnPieces = 0;
            lblSpeedPercentage.Text = CalculateSpeedPercentage() + "%";

            busyCartsTimerSpeed = 6 + (double)timerSpeedTrackbar.Value / 5;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            hasStart = false;
            int piecesWithDesk = 0;
            foreach (Cell elem in grid)
                if (elem.Col == 1)
                    if (elem.Tag == BeltType.HORIZONTAL && elem.HasDesk == true)
                        piecesWithDesk++;
            if (piecesWithDesk == firstColumnPieces && firstColumnPieces != 0)
                hasStart = true;
            if (hasStart)
            {
                this.btnPause.Enabled = true;
                this.btnStart.Enabled = false;
                this.btnRestart.Enabled = true;
                this.btnReset.Enabled = true;
                timer.Interval = timerSpeedTrackbar.Value;
                for (int i = 1; i < 7; i++)
                {
                    List<Cell> cells = new List<Cell>();
                    for (int k = 0; k < this.grid.Count; k++)
                    {
                        if (string.Format("{0}{1}", this.grid[k].Row, this.grid[k].Col) == string.Format("{0}{1}", i, 1) && (this.grid[k].Tag != BeltType.EMPTY && this.grid[k].Tag != BeltType.UPRIGHT && this.grid[k].Tag != BeltType.DOWNRIGHT))
                        {
                            cells.Add(this.grid[k]);
                            Cell c = this.grid[k];
                            try
                            {
                                switch (c.Tag)
                                {
                                    case BeltType.HORIZONTAL:
                                        foreach (Cell cell in this.grid)
                                        {
                                            if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col + 1)))
                                            {
                                                if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.DOWNRIGHT || cell.Tag == BeltType.UPRIGHT)
                                                {
                                                    this.airport.belt.DrawError(cell);
                                                    throw new Exception("Inappropriate piece");
                                                }
                                                else
                                                {
                                                    c = cell;
                                                    this.airport.belt.AddCellWithLocations(c, ref cells, false);
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                }
                                this.airport.belt.FindPath(ref cells, ref c, this.grid);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error");
                            }
                        }
                    }
                }
                foreach (Cell cell in grid)
                {
                    cell.PicBox.Visible = false;
                }
                this.airport.belt.DrawPath(panelGrid);
                this.airport.belt.PopulateAvailableLuggages();
                //this.area = new SortingArea(this.airport.belt.activeExits);
                //start timer
                this.timer.Start();

            }
            else
            {
                bool hasPiece = false;
                foreach (Cell elem in grid)
                    if (elem.Col == 1)
                        if (elem.Tag == BeltType.HORIZONTAL)
                            hasPiece = true;
                if (hasPiece)
                    if (piecesWithDesk != firstColumnPieces)
                        MessageBox.Show("Cannot proceed. Missing a check-in desk");
                    else
                        MessageBox.Show("Cannot proceed. Missing a horizontal piece at the start of the belt system");
            }
        }
        public void btnStartEvent()
        {
            if (hasStart)
            {
                this.btnPause.Enabled = true;
                this.btnStart.Enabled = false;
                this.btnRestart.Enabled = true;
                this.btnReset.Enabled = true;
                timer.Interval = timerSpeedTrackbar.Value;
                for (int i = 1; i < 7; i++)
                {
                    List<Cell> cells = new List<Cell>();
                    for (int k = 0; k < this.grid.Count; k++)
                    {
                        if (string.Format("{0}{1}", this.grid[k].Row, this.grid[k].Col) == string.Format("{0}{1}", i, 1) && (this.grid[k].Tag != BeltType.EMPTY && this.grid[k].Tag != BeltType.UPRIGHT && this.grid[k].Tag != BeltType.DOWNRIGHT))
                        {
                            cells.Add(this.grid[k]);
                            Cell c = this.grid[k];
                            try
                            {
                                switch (c.Tag)
                                {
                                    case BeltType.HORIZONTAL:
                                        foreach (Cell cell in this.grid)
                                        {
                                            if (string.Format("{0}{1}", cell.Row, cell.Col) == string.Format("{0}{1}", c.Row, (c.Col + 1)))
                                            {
                                                if (cell.Tag == BeltType.EMPTY || cell.Tag == BeltType.VERTICAL || cell.Tag == BeltType.DOWNRIGHT || cell.Tag == BeltType.UPRIGHT)
                                                {
                                                    this.airport.belt.DrawError(cell);
                                                    throw new Exception("Inappropriate piece");
                                                }
                                                else
                                                {
                                                    c = cell;
                                                    this.airport.belt.AddCellWithLocations(c, ref cells, false);
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                }
                                this.airport.belt.FindPath(ref cells, ref c, this.grid);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error");
                            }
                        }
                    }
                }
                foreach (Cell cell in grid)
                {
                    cell.PicBox.Visible = false;
                }
                this.airport.belt.DrawPath(panelGrid);
                this.airport.belt.PopulateAvailableLuggages();
                //this.area = new SortingArea(this.airport.belt.activeExits);
                //start timer
                this.timer.Start();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (lugs != airport.belt.Passed)
            {
                UpdateStatistics();
            }
            if (this.airport.maxLuggage == this.airport.belt.Passed)
            {
                btnContinue.Enabled = false;
                btnPause.Enabled = false;
                btnStart.Enabled = false;
                timer.Stop();
                System.Timers.Timer t = new System.Timers.Timer();
                t.Interval = busyCartsTimerSpeed * 1000 + 1000;
                t.Elapsed += new ElapsedEventHandler(t_Elapsed);
                t.AutoReset = false;
                t.Start();
                MessageBox.Show("Simulation ended successfully");
            }
            else
            {
                lugs = airport.belt.Passed;
                this.airport.belt.MoveLuggagesOnBelt(panelGrid);
            }
        }
        private void UpdateStatistics()
        {
            this.lblPassedLuggage.Text = airport.getStatsPassed().ToString();
            this.lblCartsNeeded.Text = airport.getStatsCartsNeeded().ToString();
            this.lblEmpsNeeded.Text = airport.getStatsEmpsNeeded().ToString();
            this.lblAvTrucks.Text = airport.getStatsAvailableCarts().ToString();
            this.lblBusyTrucks.Text = airport.getStatsBusyCarts().ToString();
            this.lblTransported.Text = airport.getStatsTransported().ToString();
        }
        public void t_Elapsed(object sender, EventArgs e)
        {
            SetText(airport.getStatsAvailableCarts().ToString(), this.lblAvTrucks);
            SetText(airport.getStatsBusyCarts().ToString(), this.lblBusyTrucks);
        }
        delegate void SetTextCallback(string text, Label l);
        private void SetText(string text, Label l)
        {
            if (l.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, l });
            }
            else
            {
                l.Text = text;
            }
        }

        private void UpLeft_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbUpLeft.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            //horizontal line
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, this.pbUpLeft.Height / 3, this.pbUpLeft.Width * 2 / 3, this.pbUpLeft.Height / 3);
            //vertical line
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpLeft.Width / 3, this.pbUpLeft.Height / 2, this.pbVertical.Width / 3, this.pbVertical.Height / 2);

        }
        private void pbUpRight_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbUpRight.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpRight.Width / 3, this.pbUpRight.Height / 3, this.pbUpRight.Width * 2 / 3, this.pbUpRight.Height / 3);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpRight.Width / 3, this.pbUpRight.Height / 2, this.pbUpRight.Width / 3, this.pbUpRight.Height / 2);
        }

        private void pbLowerRight_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbDownLeft.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, this.pbDownLeft.Height / 3, this.pbDownLeft.Width * 2 / 3, this.pbDownLeft.Height / 3);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownLeft.Width / 3, 0, this.pbDownLeft.Width / 3, this.pbDownLeft.Height / 2);
        }

        private void pbLowerLeft_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbDownRight.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownRight.Width / 3, this.pbDownRight.Height / 3, this.pbDownRight.Width * 2 / 3, this.pbDownRight.Height / 3);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownRight.Width / 3, 0, this.pbDownRight.Width / 3, this.pbDownRight.Height / 2);
        }

        private void pbHorizontal_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbHorizontal.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pbHorizontal.Height / 3, pbHorizontal.Width, pbHorizontal.Height / 3);
        }
        private void pbVertical_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbVertical.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbVertical.Width / 3, 0, this.pbVertical.Width / 3, this.pbVertical.Height);
        }

        private void SetPiece(PictureBox pictureBox)
        {
            if (piece == null) return;
            pictureBox.CreateGraphics().Clear(Color.DarkGray);
            pictureBox.CreateGraphics().FillRectangle(new SolidBrush(ControlDark), 0, 0, pictureBox.Width, pictureBox.Height);
            int c = -1;
            for (int i = 0; i < this.grid.Count; i++)
            {
                if (this.grid[i].PicBox == pictureBox)
                {
                    c = i;
                }
            }
            this.btnInput.Enabled = true;
            switch (piece.Type)
            {
                case BeltType.RIGHTDOWN:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, this.pbUpLeft.Height / 3, this.pbUpLeft.Width * 2 / 3, this.pbUpLeft.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpLeft.Width / 3, this.pbUpLeft.Height / 2, this.pbVertical.Width / 3, this.pbVertical.Height / 2);
                    this.grid[c].Tag = BeltType.RIGHTDOWN;
                    break;
                case BeltType.UPRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpRight.Width / 3, this.pbUpRight.Height / 3, this.pbUpRight.Width * 2 / 3, this.pbUpRight.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpRight.Width / 3, this.pbUpRight.Height / 2, this.pbUpRight.Width / 3, this.pbUpRight.Height / 2);
                    this.grid[c].Tag = BeltType.UPRIGHT;
                    break;
                case BeltType.RIGHTUP:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, this.pbDownLeft.Height / 3, this.pbDownLeft.Width * 2 / 3, this.pbDownLeft.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownLeft.Width / 3, 0, this.pbDownLeft.Width / 3, this.pbDownLeft.Height / 2);
                    this.grid[c].Tag = BeltType.RIGHTUP;
                    break;
                case BeltType.DOWNRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownRight.Width / 3, this.pbDownRight.Height / 3, this.pbDownRight.Width * 2 / 3, this.pbDownRight.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownRight.Width / 3, 0, this.pbDownRight.Width / 3, this.pbDownRight.Height / 2);
                    this.grid[c].Tag = BeltType.DOWNRIGHT;
                    break;
                case BeltType.HORIZONTAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pbHorizontal.Height / 3, pbHorizontal.Width, pbHorizontal.Height / 3);
                    pictureBox.Tag = BeltType.HORIZONTAL;
                    this.grid[c].Tag = BeltType.HORIZONTAL;
                    break;
                case BeltType.VERTICAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbVertical.Width / 3, 0, this.pbVertical.Width / 3, this.pbVertical.Height);
                    this.grid[c].Tag = BeltType.VERTICAL;
                    break;
            }
        }

        private void pbUpLeft_Click(object sender, EventArgs e)
        {
            piece = new Piece(BeltType.RIGHTDOWN);
            desk = null;
            pictureBox = pbUpLeft;
        }

        private void pbUpRight_Click(object sender, EventArgs e)
        {
            piece = new Piece(BeltType.UPRIGHT);
            desk = null;
            pictureBox = pbUpRight;
        }

        private void pbDownLeft_Click(object sender, EventArgs e)
        {
            piece = new Piece(BeltType.RIGHTUP);
            desk = null;
            pictureBox = pbDownLeft;
        }

        private void pbDownRight_Click(object sender, EventArgs e)
        {
            piece = new Piece(BeltType.DOWNRIGHT);
            desk = null;
            pictureBox = pbDownRight;
        }

        private void pbHorizontal_Click(object sender, EventArgs e)
        {
            piece = new Piece(BeltType.HORIZONTAL);
            desk = null;
            pictureBox = pbHorizontal;
        }

        private void pbVertical_Click(object sender, EventArgs e)
        {
            piece = new Piece(BeltType.VERTICAL);
            desk = null;
            pictureBox = pbHorizontal;
        }

        private void pb11_Click(object sender, EventArgs e)
        {
            if (desk == null)
            {
                SetPiece(pb11);
                if (piece != null)
                    if (piece.Type == BeltType.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb11.Tag.ToString() == "HORIZONTAL" && this.pb11.Tag.ToString() != "")
                {
                    SetDesk(pb11);
                    if (desk != null)
                        foreach (Cell c in grid)
                            if (c.Row == 1 && c.Col == 1)
                                c.HasDesk = true;
                    desk = null;
                }
                else
                    MessageBox.Show("Desks can only be placed on horizontal pieces");
            }
        }

        private void pb12_Click(object sender, EventArgs e)
        {
            SetPiece(pb12);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb13_Click(object sender, EventArgs e)
        {
            SetPiece(pb13);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb14_Click(object sender, EventArgs e)
        {
            SetPiece(pb14);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb15_Click(object sender, EventArgs e)
        {
            SetPiece(pb15);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb16_Click(object sender, EventArgs e)
        {
            SetPiece(pb16);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb21_Click(object sender, EventArgs e)
        {
            if (desk == null)
            {
                SetPiece(pb21);
                if (piece != null)
                    if (piece.Type == BeltType.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb21.Tag.ToString() == "HORIZONTAL" && this.pb21.Tag.ToString() != "")
                {
                    SetDesk(pb21);
                    if (desk != null)
                        foreach (Cell c in grid)
                            if (c.Row == 2 && c.Col == 1)
                                c.HasDesk = true;
                    desk = null;
                }
                else
                    MessageBox.Show("Desks can only be placed on horizontal pieces");
            }
        }

        private void pb22_Click(object sender, EventArgs e)
        {
            SetPiece(pb22);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb23_Click(object sender, EventArgs e)
        {
            SetPiece(pb23);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb24_Click(object sender, EventArgs e)
        {
            SetPiece(pb24);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb25_Click(object sender, EventArgs e)
        {
            SetPiece(pb25);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb26_Click(object sender, EventArgs e)
        {
            SetPiece(pb26);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb31_Click(object sender, EventArgs e)
        {
            if (desk == null)
            {
                SetPiece(pb31);
                if (piece != null)
                    if (piece.Type == BeltType.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb31.Tag.ToString() == "HORIZONTAL" && this.pb31.Tag.ToString() != "")
                {
                    SetDesk(pb31);
                    if (desk != null)
                        foreach (Cell c in grid)
                            if (c.Row == 3 && c.Col == 1)
                                c.HasDesk = true;
                    desk = null;
                }
                else
                    MessageBox.Show("Desks can only be placed on horizontal pieces");
            }
        }

        private void pb32_Click(object sender, EventArgs e)
        {
            SetPiece(pb32);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb33_Click(object sender, EventArgs e)
        {
            SetPiece(pb33);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb34_Click(object sender, EventArgs e)
        {
            SetPiece(pb34);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb35_Click(object sender, EventArgs e)
        {
            SetPiece(pb35);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb36_Click(object sender, EventArgs e)
        {
            SetPiece(pb36);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb41_Click(object sender, EventArgs e)
        {
            if (desk == null)
            {
                SetPiece(pb41);
                if (piece != null)
                    if (piece.Type == BeltType.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb41.Tag.ToString() == "HORIZONTAL" && this.pb41.Tag.ToString() != "")
                {
                    SetDesk(pb41);
                    if (desk != null)
                        foreach (Cell c in grid)
                            if (c.Row == 4 && c.Col == 1)
                                c.HasDesk = true;
                    desk = null;
                }
                else
                    MessageBox.Show("Desks can only be placed on horizontal pieces");
            }
        }

        private void pb42_Click(object sender, EventArgs e)
        {
            SetPiece(pb42);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb43_Click(object sender, EventArgs e)
        {
            SetPiece(pb43);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb44_Click(object sender, EventArgs e)
        {
            SetPiece(pb44);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb45_Click(object sender, EventArgs e)
        {
            SetPiece(pb45);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb46_Click(object sender, EventArgs e)
        {
            SetPiece(pb46);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb51_Click(object sender, EventArgs e)
        {
            if (desk == null)
            {
                SetPiece(pb51);
                if (piece != null)
                    if (piece.Type == BeltType.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb51.Tag.ToString() == "HORIZONTAL" && this.pb51.Tag.ToString() != "")
                {
                    SetDesk(pb51);
                    if (desk != null)
                        foreach (Cell c in grid)
                            if (c.Row == 5 && c.Col == 1)
                                c.HasDesk = true;
                    desk = null;
                }
                else
                    MessageBox.Show("Desks can only be placed on horizontal pieces");
            }
        }

        private void pb52_Click(object sender, EventArgs e)
        {
            SetPiece(pb52);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb53_Click(object sender, EventArgs e)
        {
            SetPiece(pb53);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb54_Click(object sender, EventArgs e)
        {
            SetPiece(pb54);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb55_Click(object sender, EventArgs e)
        {
            SetPiece(pb55);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb56_Click(object sender, EventArgs e)
        {
            SetPiece(pb56);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb61_Click(object sender, EventArgs e)
        {
            if (desk == null)
            {
                SetPiece(pb61);
                if (piece != null)
                    if (piece.Type == BeltType.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb61.Tag.ToString() == "HORIZONTAL" && this.pb61.Tag.ToString() != "")
                {
                    SetDesk(pb61);
                    if (desk != null)
                        foreach (Cell c in grid)
                            if (c.Row == 6 && c.Col == 1)
                                c.HasDesk = true;
                    desk = null;
                }
                else
                    MessageBox.Show("Desks can only be placed on horizontal pieces");
            }
        }

        private void pb62_Click(object sender, EventArgs e)
        {
            SetPiece(pb62);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb63_Click(object sender, EventArgs e)
        {
            SetPiece(pb63);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb64_Click(object sender, EventArgs e)
        {
            SetPiece(pb64);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb65_Click(object sender, EventArgs e)
        {
            SetPiece(pb65);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pb66_Click(object sender, EventArgs e)
        {
            SetPiece(pb66);
            if (desk != null)
                MessageBox.Show("You can only add a desk on the first column");
        }

        private void pbDeskTL_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbDeskTL.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskTL.Width / 9, this.pbDeskTL.Height / 9, this.pbDeskTL.Width / 9 * 2, this.pbDeskTL.Height / 9 * 2);
        }

        private void pbDeskTR_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbDeskTR.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskTR.Width / 9 * 6, this.pbDeskTR.Height / 9, this.pbDeskTR.Width / 9 * 2, this.pbDeskTR.Height / 9 * 2);
        }

        private void pbDeskBL_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbDeskBL.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskBL.Width / 9, this.pbDeskBL.Height / 9 * 6, this.pbDeskBL.Width / 9 * 2, this.pbDeskBL.Height / 9 * 2);
        }

        private void pbDeskBR_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbDeskBR.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskBR.Width / 9 * 6, this.pbDeskBR.Height / 9 * 6, this.pbDeskBR.Width / 9 * 2, this.pbDeskBR.Height / 9 * 2);
        }
        private void SetDesk(PictureBox pictureBox)
        {
            if (desk == null) return;
            int c = -1;
            for (int i = 0; i < this.grid.Count; i++)
            {
                if (this.grid[i].PicBox == pictureBox)
                {
                    c = i;
                }
            }
            if (this.grid[c].Desk.Type == DeskType.EMPTY)
            {
                switch (desk.Type)
                {
                    case DeskType.TOPLEFT:
                        pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskTL.Width / 9, this.pbDeskTL.Height / 9, this.pbDeskTL.Width / 9 * 2, this.pbDeskTL.Height / 9 * 2);
                        this.grid[c].Desk.Type = DeskType.TOPLEFT;
                        break;
                    case DeskType.TOPRIGHT:
                        pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskTR.Width / 9 * 6, this.pbDeskTR.Height / 9, this.pbDeskTR.Width / 9 * 2, this.pbDeskTR.Height / 9 * 2);
                        this.grid[c].Desk.Type = DeskType.TOPRIGHT;
                        break;
                    case DeskType.BOTTOMLEFT:
                        pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskBL.Width / 9, this.pbDeskBL.Height / 9 * 6, this.pbDeskBL.Width / 9 * 2, this.pbDeskBL.Height / 9 * 2);
                        this.grid[c].Desk.Type = DeskType.BOTTOMLEFT;
                        break;
                    case DeskType.BOTTOMRIGHT:
                        pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskBR.Width / 9 * 6, this.pbDeskBR.Height / 9 * 6, this.pbDeskBR.Width / 9 * 2, this.pbDeskBR.Height / 9 * 2);
                        this.grid[c].Desk.Type = DeskType.BOTTOMRIGHT;
                        break;
                }
            }
            else
            {
                MessageBox.Show("Can only place one desk on a belt");
            }
        }

        private void pbDeskTL_Click(object sender, EventArgs e)
        {
            desk = new Desk(DeskType.TOPLEFT);
            piece = null;
        }

        private void pbDeskTR_Click(object sender, EventArgs e)
        {
            desk = new Desk(DeskType.TOPRIGHT);
            piece = null;
        }

        private void pbDeskBL_Click(object sender, EventArgs e)
        {
            desk = new Desk(DeskType.BOTTOMLEFT);
            piece = null;
        }

        private void pbDeskBR_Click(object sender, EventArgs e)
        {
            desk = new Desk(DeskType.BOTTOMRIGHT);
            piece = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            this.btnPause.Enabled = false;
            this.btnRestart.Enabled = false;
            this.btnContinue.Enabled = false;
            this.RearrangeGrid();
            this.RearrangePanelGrid();
        }

        public void activateForm(int lug, int cartCapacity)
        {
            hasStart = false;
            foreach (Cell elem in grid)
                if (elem.Tag != BeltType.EMPTY)
                {
                    hasStart = true;
                    break;
                }
            if (hasStart)
            {
                this.tempCartCapacity = cartCapacity;
                this.airport = new Airport(lug, cartCapacity);
                this.airport.UpdateBusyCartsSpeed(busyCartsTimerSpeed);
                this.btnStart.Enabled = true;
                this.btnPause.Enabled = false;
                this.btnContinue.Enabled = false;
                this.btnRestart.Enabled = false;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer.Stop();
            btnContinue.Enabled = true;
            this.btnPause.Enabled = false;
        }
        private void btnContinue_Click(object sender, EventArgs e)
        {
            timer.Start();
            btnContinue.Enabled = false;
            this.btnPause.Enabled = true;
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.btnStart.Enabled = false;
            this.btnReset.Enabled = true;
            this.btnPause.Enabled = true;
            lugs = 0;
            this.airport.Statistics.ClearStatistics();
            this.airport.Statistics.SetCartCapacity(tempCartCapacity);
            this.airport.belt.DrawPath(panelGrid);
            this.airport.belt.RestartBelt();
            this.lblPassedLuggage.Text = "0";
            this.lblCartsNeeded.Text = "0";
            this.lblEmpsNeeded.Text = "0";
            this.lblAvTrucks.Text = "0";
            this.lblBusyTrucks.Text = "0";
            this.lblTransported.Text = "0";
            timer.Start();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset simulation
            timer.Stop();
            this.btnRestart.Enabled = false;
            this.btnStart.Enabled = false;
            this.btnContinue.Enabled = false;
            this.btnPause.Enabled = false;
            this.btnReset.Enabled = false;
            this.airport.belt.ClearBelt();
            panelGrid.Refresh();
            foreach (Cell cell in grid)
            {
                cell.PicBox.Visible = true;
                cell.Tag = BeltType.EMPTY;
                cell.Desk.Type = DeskType.EMPTY;
            }
            firstColumnPieces = 0;
            lugs = 0;
            airport.Statistics.ClearStatistics();
            UpdateStatistics();
        }
        private void btnInput_Click(object sender, EventArgs e)
        {
            inputForm inputForm = new inputForm(this);
            inputForm.Show();
        }
        private void RearrangePanelGrid()
        {
            // Starting corner
            panelGrid.Location = pb11.Location;
            // Width and height is located by row count * height and col count * width
            panelGrid.Width = 540;
            panelGrid.Height = 540;
        }
        private void RearrangeGrid()
        {
            int inc = 0;
            for (int i = 0; i < 6; i++)
            {
                int y = 0;
                if (i == 0)
                {
                    y = this.grid[0].PicBox.Location.Y;
                }
                else
                {
                    y = this.grid[inc - 6].PicBox.Location.Y + this.grid[inc].PicBox.Height;
                }
                for (int k = 0; k < 6; k++)
                {
                    if (k * i != 36 && k + inc != 0)
                    {
                        if (k + inc == 6 || k + inc == 12 || k + inc == 18 || k + inc == 24 || k + inc == 30)
                        {
                            this.grid[k + inc].PicBox.Location = new Point(this.grid[0].PicBox.Location.X, y);
                        }
                        else
                        {
                            this.grid[k + inc].PicBox.Location = new Point(this.grid[k + inc - 1].PicBox.Location.X + this.grid[k + inc - 1].PicBox.Width, y);
                        }
                    }
                }
                inc += 6;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FileStream fs;
            StreamWriter sw;
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filename = sfd.FileName + ".txt";
                    fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    string[] s = this.airport.saveStatistics();
                    for (int i = 0; i < s.Length; i++)
                    {
                        sw.WriteLine(s[i]);
                    }
                    sw.Close();
                    MessageBox.Show("Saved");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void timerSpeedTrackbar_Scroll(object sender, EventArgs e)
        {
            busyCartsTimerSpeed = 6 + (double)timerSpeedTrackbar.Value / 5;
            if (this.airport != null)
                this.airport.UpdateBusyCartsSpeed(busyCartsTimerSpeed);
            timer.Interval = timerSpeedTrackbar.Value;
            lblSpeedPercentage.Text = CalculateSpeedPercentage() + "%";
        }

        private int CalculateSpeedPercentage()
        {
            // Min interval is 1 and max is 50 so every interval is 2%
            int change = (1 - timerSpeedTrackbar.Value) * 2;
            percentage = 100 + change;
            return percentage;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SortingArea s = new SortingArea(this.airport.belt.activeExits);
        //    s.Show();
        //}
    }
}
