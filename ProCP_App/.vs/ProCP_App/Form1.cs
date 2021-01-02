using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCP_App
{
    public partial class Form1 : Form
    {
        private Airport airport;
        private List<Cell> emptyGrid;
        private Piece piece;
        private Desk desk;
        PictureBox pictureBox;
        int hasDesk;
        int firstColumnPieces;
        inputForm inputForm;

        public Color ControlDark { get; private set; }

        public Form1()
        {
            InitializeComponent();
            this.emptyGrid = new List<Cell>();
            this.emptyGrid.Add(new Cell(this.pb11, this.pb11.Name.Substring(2, 2), 1, 1));
            this.emptyGrid.Add(new Cell(this.pb12, this.pb12.Name.Substring(2, 2), 1, 2));
            this.emptyGrid.Add(new Cell(this.pb13, this.pb13.Name.Substring(2, 2), 1, 3));
            this.emptyGrid.Add(new Cell(this.pb14, this.pb14.Name.Substring(2, 2), 1, 4));
            this.emptyGrid.Add(new Cell(this.pb15, this.pb15.Name.Substring(2, 2), 1, 5));
            this.emptyGrid.Add(new Cell(this.pb16, this.pb16.Name.Substring(2, 2), 1, 6));
            this.emptyGrid.Add(new Cell(this.pb21, this.pb21.Name.Substring(2, 2), 2, 1));
            this.emptyGrid.Add(new Cell(this.pb22, this.pb22.Name.Substring(2, 2), 2, 2));
            this.emptyGrid.Add(new Cell(this.pb23, this.pb23.Name.Substring(2, 2), 2, 3));
            this.emptyGrid.Add(new Cell(this.pb24, this.pb24.Name.Substring(2, 2), 2, 4));
            this.emptyGrid.Add(new Cell(this.pb25, this.pb25.Name.Substring(2, 2), 2, 5));
            this.emptyGrid.Add(new Cell(this.pb26, this.pb26.Name.Substring(2, 2), 2, 6));
            this.emptyGrid.Add(new Cell(this.pb31, this.pb31.Name.Substring(2, 2), 3, 1));
            this.emptyGrid.Add(new Cell(this.pb32, this.pb32.Name.Substring(2, 2), 3, 2));
            this.emptyGrid.Add(new Cell(this.pb33, this.pb33.Name.Substring(2, 2), 3, 3));
            this.emptyGrid.Add(new Cell(this.pb34, this.pb34.Name.Substring(2, 2), 3, 4));
            this.emptyGrid.Add(new Cell(this.pb35, this.pb35.Name.Substring(2, 2), 3, 5));
            this.emptyGrid.Add(new Cell(this.pb36, this.pb36.Name.Substring(2, 2), 3, 6));
            this.emptyGrid.Add(new Cell(this.pb41, this.pb41.Name.Substring(2, 2), 4, 1));
            this.emptyGrid.Add(new Cell(this.pb42, this.pb42.Name.Substring(2, 2), 4, 2));
            this.emptyGrid.Add(new Cell(this.pb43, this.pb43.Name.Substring(2, 2), 4, 3));
            this.emptyGrid.Add(new Cell(this.pb44, this.pb44.Name.Substring(2, 2), 4, 4));
            this.emptyGrid.Add(new Cell(this.pb45, this.pb45.Name.Substring(2, 2), 4, 5));
            this.emptyGrid.Add(new Cell(this.pb46, this.pb46.Name.Substring(2, 2), 4, 6));
            this.emptyGrid.Add(new Cell(this.pb51, this.pb51.Name.Substring(2, 2), 5, 1));
            this.emptyGrid.Add(new Cell(this.pb52, this.pb52.Name.Substring(2, 2), 5, 2));
            this.emptyGrid.Add(new Cell(this.pb53, this.pb53.Name.Substring(2, 2), 5, 3));
            this.emptyGrid.Add(new Cell(this.pb54, this.pb54.Name.Substring(2, 2), 5, 4));
            this.emptyGrid.Add(new Cell(this.pb55, this.pb55.Name.Substring(2, 2), 5, 5));
            this.emptyGrid.Add(new Cell(this.pb56, this.pb56.Name.Substring(2, 2), 5, 6));
            this.emptyGrid.Add(new Cell(this.pb61, this.pb61.Name.Substring(2, 2), 6, 1));
            this.emptyGrid.Add(new Cell(this.pb62, this.pb62.Name.Substring(2, 2), 6, 2));
            this.emptyGrid.Add(new Cell(this.pb63, this.pb63.Name.Substring(2, 2), 6, 3));
            this.emptyGrid.Add(new Cell(this.pb64, this.pb64.Name.Substring(2, 2), 6, 4));
            this.emptyGrid.Add(new Cell(this.pb65, this.pb65.Name.Substring(2, 2), 6, 5));
            this.emptyGrid.Add(new Cell(this.pb66, this.pb66.Name.Substring(2, 2), 6, 6));
            foreach (Cell c in this.emptyGrid)
            {
                c.PicBox.BorderStyle = BorderStyle.FixedSingle;
                c.PicBox.Tag = Type.EMPTY;
            }
            hasDesk = 0;
            firstColumnPieces = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bool hasStart = false;
            int piecesWithDesk = 0;
            foreach (Cell elem in emptyGrid)
                if (elem.Y == 1)
                    if (elem.Tag == Type.HORIZONTAL && elem.hasDesk == true)
                        piecesWithDesk++;
            if (piecesWithDesk == firstColumnPieces && firstColumnPieces != 0)
                hasStart = true;
            if (hasStart)
            {
                this.btnPause.Enabled = true;
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
                this.timer.Start();
                for (int i = 1; i < 7; i++)
                {
                    List<Cell> cells = new List<Cell>();
                    for (int k = 0; k < this.emptyGrid.Count; k++)
                    {
                        if (this.emptyGrid[k].Name == i.ToString() + "1" && (this.emptyGrid[k].Tag != Type.EMPTY && this.emptyGrid[k].Tag != Type.UPRIGHT && this.emptyGrid[k].Tag != Type.DOWNRIGHT))
                        {
                            cells.Add(this.emptyGrid[k]);
                            Cell c = this.emptyGrid[k];
                            try
                            {
                                switch (c.Tag)
                                {
                                    case Type.RIGHTDOWN:
                                        foreach (Cell cell in this.emptyGrid)
                                        {
                                            if (cell.Name == (Convert.ToInt32(c.Name.Substring(0, 1)) + 1).ToString() + c.Name.Substring(1, 1))
                                            {
                                                if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.UPRIGHT || cell.Tag == Type.RIGHTDOWN)
                                                {
                                                    this.airport.belt.drawError(cell);
                                                    throw new Exception("Inappropriate piece");
                                                }
                                                else
                                                {
                                                    c = cell;
                                                    cells[cells.Count - 1].nextCell = c;
                                                    cells.Add(c);
                                                }
                                                break;
                                            }
                                        }
                                        break;

                                    case Type.UPRIGHT:
                                        foreach (Cell cell in this.emptyGrid)
                                        {
                                            if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) - 1).ToString())
                                            {
                                                if (cell.Tag == Type.EMPTY || cell.Tag == Type.HORIZONTAL || cell.Tag == Type.RIGHTUP || cell.Tag == Type.DOWNRIGHT)
                                                {
                                                    this.airport.belt.drawError(cell);
                                                    throw new Exception("Inappropriate piece");
                                                }
                                                else
                                                {
                                                    c = cell;
                                                    cells[cells.Count - 1].nextCell = c;
                                                    cells.Add(c);
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    case Type.HORIZONTAL:
                                        foreach (Cell cell in this.emptyGrid)
                                        {
                                            if (cell.Name == c.Name.Substring(0, 1) + (Convert.ToInt32(c.Name.Substring(1, 1)) + 1).ToString())
                                            {
                                                if (cell.Tag == Type.EMPTY || cell.Tag == Type.VERTICAL || cell.Tag == Type.DOWNRIGHT || cell.Tag == Type.UPRIGHT)
                                                {
                                                    this.airport.belt.drawError(cell);
                                                    throw new Exception("Inappropriate piece");
                                                }
                                                else
                                                {
                                                    c = cell;
                                                    cells[cells.Count - 1].nextCell = c;
                                                    cells.Add(c);
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                }
                                this.airport.belt.findPath(cells, c, this.emptyGrid);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error");
                            }
                        }
                    }
                }
                //this.belt.MoveLuggage(this.airport);
            }
            else
            {
                bool hasPiece = false;
                foreach (Cell elem in emptyGrid)
                    if (elem.Y == 1)
                        if (elem.Tag == Type.HORIZONTAL)
                            hasPiece = true;
                if(hasPiece)
                    if (piecesWithDesk != firstColumnPieces)
                        MessageBox.Show("Cannot proceed. Missing a check-in desk");
                else
                    MessageBox.Show("Cannot proceed. Missing a horizontal piece at the start of the belt system");
            }
            //start timer
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.tbxStatistic.Text = airport.getStatistic();
            //start animation
            if(this.airport.maxLuggage == this.airport.belt.Passed)
            {
                timer.Stop();
            }
            this.airport.belt.MoveLuggage();
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
            for (int i = 0; i < this.emptyGrid.Count; i++)
            {
                if (this.emptyGrid[i].PicBox == pictureBox)
                {
                    c = i;
                }
            }
            this.btnInput.Enabled = true;
            switch (piece.Type)
            {
                case Type.RIGHTDOWN:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, this.pbUpLeft.Height / 3, this.pbUpLeft.Width * 2 / 3, this.pbUpLeft.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpLeft.Width / 3, this.pbUpLeft.Height / 2, this.pbVertical.Width / 3, this.pbVertical.Height / 2);
                    this.emptyGrid[c].Tag = Type.RIGHTDOWN;
                    break;
                case Type.UPRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpRight.Width / 3, this.pbUpRight.Height / 3, this.pbUpRight.Width * 2 / 3, this.pbUpRight.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbUpRight.Width / 3, this.pbUpRight.Height / 2, this.pbUpRight.Width / 3, this.pbUpRight.Height / 2);
                    this.emptyGrid[c].Tag = Type.UPRIGHT;
                    break;
                case Type.RIGHTUP:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, this.pbDownLeft.Height / 3, this.pbDownLeft.Width * 2 / 3, this.pbDownLeft.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownLeft.Width / 3, 0, this.pbDownLeft.Width / 3, this.pbDownLeft.Height / 2);
                    this.emptyGrid[c].Tag = Type.RIGHTUP;
                    break;
                case Type.DOWNRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownRight.Width / 3, this.pbDownRight.Height / 3, this.pbDownRight.Width * 2 / 3, this.pbDownRight.Height / 3);
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbDownRight.Width / 3, 0, this.pbDownRight.Width / 3, this.pbDownRight.Height / 2);
                    this.emptyGrid[c].Tag = Type.DOWNRIGHT;
                    break;
                case Type.HORIZONTAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), 0, pbHorizontal.Height / 3, pbHorizontal.Width, pbHorizontal.Height / 3);
                    pictureBox.Tag = Type.HORIZONTAL;
                    this.emptyGrid[c].Tag = Type.HORIZONTAL;
                    break;
                case Type.VERTICAL:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkSlateGray), this.pbVertical.Width / 3, 0, this.pbVertical.Width / 3, this.pbVertical.Height);
                    this.emptyGrid[c].Tag = Type.VERTICAL;
                    break;
            }
        }

        private void pbUpperRight_Click(object sender, EventArgs e)
        {
            piece = new Piece(Type.RIGHTDOWN);
            desk = null;
            pictureBox = pbUpLeft;
        }

        private void pbUpperLeft_Click(object sender, EventArgs e)
        {
            piece = new Piece(Type.UPRIGHT);
            desk = null;
            pictureBox = pbUpRight;
        }

        private void pbLowerRight_Click(object sender, EventArgs e)
        {
            piece = new Piece(Type.RIGHTUP);
            desk = null;
            pictureBox = pbDownLeft;
        }

        private void pbLowerLeft_Click(object sender, EventArgs e)
        {
            piece = new Piece(Type.DOWNRIGHT);
            desk = null;
            pictureBox = pbDownRight;
        }

        private void pbHorizontal_Click(object sender, EventArgs e)
        {
            piece = new Piece(Type.HORIZONTAL);
            desk = null;
            pictureBox = pbHorizontal;
        }

        private void pbVertical_Click(object sender, EventArgs e)
        {
            piece = new Piece(Type.VERTICAL);
            desk = null;
            pictureBox = pbHorizontal;
        }

        private void pb11_Click(object sender, EventArgs e)
        {
            if (desk == null)
            {
                SetPiece(pb11);
                if (piece != null)
                    if (piece.Type == Type.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb11.Tag.ToString() == "HORIZONTAL" && this.pb11.Tag.ToString() != "")
                {
                    SetDesk(pb11);
                    if (desk != null)
                        foreach (Cell c in emptyGrid)
                            if (c.X == 1 && c.Y == 1)
                                c.hasDesk = true;
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
                    if (piece.Type == Type.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb21.Tag.ToString() == "HORIZONTAL" && this.pb21.Tag.ToString() != "")
                {
                    SetDesk(pb21);
                    if (desk != null)
                        foreach (Cell c in emptyGrid)
                            if (c.X == 2 && c.Y == 1)
                                c.hasDesk = true;
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
                    if (piece.Type == Type.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb31.Tag.ToString() == "HORIZONTAL" && this.pb31.Tag.ToString() != "")
                {
                    SetDesk(pb31);
                    if (desk != null)
                        foreach (Cell c in emptyGrid)
                            if (c.X == 3 && c.Y == 1)
                                c.hasDesk = true;
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
                if(piece != null)
                    if (piece.Type == Type.HORIZONTAL)
                            firstColumnPieces++;
            }
            else
            {
                if (this.pb41.Tag.ToString() == "HORIZONTAL" && this.pb41.Tag.ToString() != "")
                {
                    SetDesk(pb41);
                    if (desk != null)
                        foreach (Cell c in emptyGrid)
                            if (c.X == 4 && c.Y == 1)
                                c.hasDesk = true;
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
                    if (piece.Type == Type.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb51.Tag.ToString() == "HORIZONTAL" && this.pb51.Tag.ToString() != "")
                {
                    SetDesk(pb51);
                    if (desk != null)
                        foreach (Cell c in emptyGrid)
                            if (c.X == 5 && c.Y == 1)
                                c.hasDesk = true;
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
                    if (piece.Type == Type.HORIZONTAL)
                        firstColumnPieces++;
            }
            else
            {
                if (this.pb61.Tag.ToString() == "HORIZONTAL" && this.pb61.Tag.ToString() != "")
                {
                    SetDesk(pb61);
                    if(desk != null)
                        foreach (Cell c in emptyGrid)
                            if (c.X == 6 && c.Y == 1)
                                c.hasDesk = true;
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
            switch (desk.Type)
            {
                case DeskType.TOPLEFT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskTL.Width / 9, this.pbDeskTL.Height / 9, this.pbDeskTL.Width / 9 * 2, this.pbDeskTL.Height / 9 * 2);
                    break;
                case DeskType.TOPRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskTR.Width / 9 * 6, this.pbDeskTR.Height / 9, this.pbDeskTR.Width / 9 * 2, this.pbDeskTR.Height / 9 * 2);
                    break;
                case DeskType.BOTTOMLEFT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskBL.Width / 9, this.pbDeskBL.Height / 9 * 6, this.pbDeskBL.Width / 9 * 2, this.pbDeskBL.Height / 9 * 2);
                    break;
                case DeskType.BOTTOMRIGHT:
                    pictureBox.CreateGraphics().FillRectangle(new SolidBrush(Color.DarkBlue), this.pbDeskBR.Width / 9 * 6, this.pbDeskBR.Height / 9 * 6, this.pbDeskBR.Width / 9 * 2, this.pbDeskBR.Height / 9 * 2);
                    break;
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
            inputForm = new inputForm(this);
            this.btnStart.Enabled = false;
            this.btnPause.Enabled = false;
            this.btnStop.Enabled = false;
            this.btnContinue.Enabled = false;
        }

        public void activateForm(int cart, int pas, int emp)
        {
            bool hasStart = false;
            foreach (Cell elem in emptyGrid)
                if (elem.Tag != Type.EMPTY)
                {
                    hasStart = true;
                    break;
                }
            if (hasStart)
            {
                this.airport = new Airport(pas, cart, emp);
                this.btnStart.Enabled = true;
                this.btnPause.Enabled = false;
                this.btnContinue.Enabled = false;
                this.btnStop.Enabled = false;
                int inc = 0;
                for (int i = 0; i < 6; i++)
                {
                    int y = 0;
                    if (i == 0)
                    {
                        y = this.emptyGrid[0].PicBox.Location.Y;
                    }
                    else
                    {
                        y = this.emptyGrid[inc - 6].PicBox.Location.Y + this.emptyGrid[inc].PicBox.Height;
                    }
                    for (int k = 0; k < 6; k++)
                    {
                        if (k * i != 36 && k + inc != 0)
                        {
                            if (k + inc == 6 || k + inc == 12 || k + inc == 18 || k + inc == 24 || k + inc == 30)
                            {
                                this.emptyGrid[k + inc].PicBox.Location = new Point(this.emptyGrid[0].PicBox.Location.X, y);
                            }
                            else
                            {
                                this.emptyGrid[k + inc].PicBox.Location = new Point(this.emptyGrid[k + inc - 1].PicBox.Location.X + this.emptyGrid[k + inc - 1].PicBox.Width, y);
                            }
                        }
                    }
                    inc += 6;
                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = true;
            timer.Stop();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.btnStop.Enabled = false;
            this.btnStart.Enabled = true;
            this.btnContinue.Enabled = false;
            this.btnPause.Enabled = false;
            this.airport.belt.clearBelt();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            inputForm.Show();
        }
    }
}
