using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCP_App
{
    class Cell
    {
        public PictureBox PicBox { get; private set; }
        public Type Tag { get; set; }
        public string Name { get; private set; }
        public bool Reverse { get; set; }
        public Luggage Luggage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Cell nextCell { get; set; }
        public bool hasDesk = false;
        public Cell(PictureBox p, string name, int x, int y)
        {
            this.PicBox = p;
            this.Name = name;
            this.Tag = Type.EMPTY;
            this.Reverse = false;
            X = x;
            Y = y;
        }


    }
}
