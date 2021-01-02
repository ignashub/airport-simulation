using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCP_App
{
    public partial class SortingArea : Form
    {
        private List<Checkpoint> checkpoints = new List<Checkpoint>();
        private List<float> entries = new List<float>();
        private List<int> activeEntry = new List<int>();

        public SortingArea(List<int> active)
        {
            InitializeComponent();
            float meetpoint = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    meetpoint = (this.pbSorting.Height / 7 - 3) * (i + 1) + (this.pbSorting.Height / 7 - 3) / 2;
                    entries.Add(meetpoint);
                }
            }
            this.activeEntry = active;
        }
        

        private void SortingArea_Load(object sender, EventArgs e)
        {
            this.pbSorting.CreateGraphics();
            pbSorting.Paint += PbSorting_Paint_1;

        }

        private void pbSorting_Paint(object sender, PaintEventArgs e)
        {
            float meetpoint = 0;
            for (int i = 0; i < 6; i++)
            {
                //entry points
                e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), 0, (this.pbSorting.Height / 7 - 3) * (i + 1), 20, (this.pbSorting.Height / 7 - 3) * (i + 1));
                checkpoints.Add(new Checkpoint(0, (this.pbSorting.Height / 7 - 3) * (i + 1)));

                if (i % 2 == 0)
                {
                    meetpoint = (this.pbSorting.Height / 7 - 3) * (i + 1) + (this.pbSorting.Height / 7 - 3) / 2;
                    e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), 15, (this.pbSorting.Height / 7 - 3) * (i + 1), 70, meetpoint);
                    checkpoints.Add(new Checkpoint(15, (this.pbSorting.Height / 7 - 3) * (i + 1)));
                }
                else
                {
                    e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), 15, (this.pbSorting.Height / 7 - 3) * (i + 1), 70, meetpoint);
                    checkpoints.Add(new Checkpoint(15, (this.pbSorting.Height / 7 - 3) * (i + 1)));
                }
            }
        }
        public void modifyBelt()
        {
            pbSorting.Paint += PbSorting_Paint_1;
        }

        private void PbSorting_Paint_1(object sender, PaintEventArgs e)
        {
            float maxX = this.pbSorting.Width - 75;
            Random r = new Random();
            for (int i = 0; i < 3; i++)
            {
                int times = r.Next(1, 4);
                e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), maxX * i / 3 + 80, entries[0], maxX * i / 3 + 80 + 50 * times, entries[1]);
                Checkpoint c = new Checkpoint(maxX * i / 4 + 80, entries[0], i);
                c.AddCheckpoint(new Checkpoint(maxX * i / 4 + 80 + 50 * times, entries[1]));
                checkpoints.Add(c);
                if (i == 2)
                {
                    e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), 70, entries[0], maxX * i / 3 + 80, entries[0]);
                }
                times = r.Next(1, 4);
                e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), maxX * i / 3 + 100, entries[2], maxX * i / 3 + 100 + 50 * times, entries[1]);
                c = new Checkpoint(maxX * i / 4 + 100, entries[2], i);
                c.AddCheckpoint(new Checkpoint(maxX * i / 4 + 100 + 50 * times, entries[1]));
                checkpoints.Add(c);
                if (i == 2)
                {
                    e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), 70, entries[2], maxX * i / 3 + 100, entries[2]);
                }
            }
            e.Graphics.DrawLine(new Pen(Color.SlateGray, 6), 70, entries[1], this.pbSorting.Width, entries[1]);
            //Draw shortest
            for (int i = 0; i < this.activeEntry.Count; i++)
            {
                
                if (this.activeEntry[i] == 1 || this.activeEntry[i] == 2)
                {
                    
                    Checkpoint ch = this.findShortestLine(entries[0], entries[1]);
                    if (this.activeEntry[i] == 1)
                    {
                        lblsortingA1.Visible = true;
                        pbxRed.Visible = true;
                        textBox1.Visible = true;
                        Color color = Color.Red;
                        e.Graphics.DrawLine(new Pen(color, 3), 70, entries[0] + 3, Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[0] + 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[0] + 3, Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] + 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] + 3, this.pbSorting.Width, entries[1] + 3);
                        e.Graphics.DrawLine(new Pen(color, 3), 0, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 20, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]));
                        e.Graphics.DrawLine(new Pen(color, 3), 15, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 70, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]) + (this.pbSorting.Height / 7 - 3) / 2);
                    }
                    else
                    {
                        lblsortingA2.Visible = true;
                        pbxBlue.Visible = true;
                        textBox2.Visible = true;
                        Color color = Color.Blue;
                        e.Graphics.DrawLine(new Pen(color, 3), 70, entries[0] - 3, Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[0] - 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[0] - 3, Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] - 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] - 3, this.pbSorting.Width, entries[1] - 3);
                        e.Graphics.DrawLine(new Pen(color, 3), 0, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 20, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]));
                        e.Graphics.DrawLine(new Pen(color, 3), 15, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 70, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]-1) + (this.pbSorting.Height / 7 - 3) / 2);
                    }
                }
                else if (this.activeEntry[i] == 4 || this.activeEntry[i] == 3)
                {
                    if (this.activeEntry[i] == 4)
                    {
                        lblsortingA4.Visible = true;
                        pbxTurquoise.Visible = true;
                        textBox4.Visible = true;
                        Color color = Color.Turquoise;
                        e.Graphics.DrawLine(new Pen(color, 6), 70, entries[1] + 3, this.pbSorting.Width, entries[1] + 3);
                        e.Graphics.DrawLine(new Pen(color, 3), 0, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 20, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]));
                        e.Graphics.DrawLine(new Pen(color, 3), 15, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 70, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]-1) + (this.pbSorting.Height / 7 - 3) / 2);
                    }
                    else
                    {
                        lblsortingA3.Visible = true;
                        pbxGreen.Visible = true;
                        textBox3.Visible = true;
                        Color color = Color.Green;
                        e.Graphics.DrawLine(new Pen(color, 6), 70, entries[1] - 3, this.pbSorting.Width, entries[1] - 3);
                        e.Graphics.DrawLine(new Pen(color, 3), 0, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 20, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]));
                        e.Graphics.DrawLine(new Pen(color, 3), 15, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 70, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]) + (this.pbSorting.Height / 7 - 3) / 2);
                    }
                }
                else if (this.activeEntry[i] == 5 || this.activeEntry[i] == 6)
                {
                    Checkpoint ch = this.findShortestLine(entries[2], entries[1]);
                    if (this.activeEntry[i] == 5)
                    {
                        lblsortingA5.Visible = true;
                        pbxHotPink.Visible = true;
                        textBox5.Visible = true;
                        Color color = Color.HotPink;
                        e.Graphics.DrawLine(new Pen(color, 3), 70, entries[2] + 3, Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[2] + 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[2] + 3, Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] + 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] + 3, this.pbSorting.Width, entries[1] + 3);
                        e.Graphics.DrawLine(new Pen(color, 3), 0, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 20, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]));
                        e.Graphics.DrawLine(new Pen(color, 3), 15, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 70, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]) + (this.pbSorting.Height / 7 - 3) / 2);
                    }
                    else
                    {
                        lblsortingA6.Visible = true;
                        pbxPurple.Visible = true;
                        textBox6.Visible = true;
                        Color color = Color.Purple;
                        e.Graphics.DrawLine(new Pen(color, 3), 70, entries[2] - 3, Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[2] - 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.x + 50 * ch.id * 1.5), entries[2] - 3, Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] - 3);
                        e.Graphics.DrawLine(new Pen(color, 3), Convert.ToUInt32(ch.neighbour.x + 50 * ch.id * 1.5), entries[1] - 3, this.pbSorting.Width, entries[1] - 3);
                        e.Graphics.DrawLine(new Pen(color, 3), 0, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 20, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]));
                        e.Graphics.DrawLine(new Pen(color, 3), 15, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]), 70, (this.pbSorting.Height / 7 - 3) * (this.activeEntry[i]-1) + (this.pbSorting.Height / 7 - 3) / 2);
                    }
                }
            }

        }
        private Checkpoint findShortestLine(float entryLine, float destinationLine)
        {
            Checkpoint ch = new Checkpoint(0, 0);
            double min = 10000000;
            foreach (Checkpoint c in this.checkpoints)
            {
                if (c.y == entryLine && c.neighbour.y == destinationLine)
                {
                    if (c.length < min)
                    {
                        ch = c;
                        min = c.length;
                    }
                }
            }
            return ch;
        }

        private void pbSorting_Click(object sender, EventArgs e)
        {

        }
    }
}
