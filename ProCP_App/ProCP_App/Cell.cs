using System.Windows.Forms;

namespace ProCP_App
{
    public class Cell
    {
        public PictureBox PicBox { get; private set; }
        public BeltType Tag { get; set; }
        public string Name { get; private set; }
        public bool Reverse { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public Cell NextCell { get; set; }
        public bool HasDesk { get; set; }
        public Desk Desk { get; set; }
        public Cell(PictureBox p, string name, int row, int col, Desk desk)
        {
            this.PicBox = p;
            this.Name = name;
            this.Tag = BeltType.EMPTY;
            this.Reverse = false;
            HasDesk = false;
            Row = row;
            Col = col;
            this.Desk = desk;
        }
    }
}
