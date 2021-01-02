using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProCP_App;
using Type = ProCP_App.BeltType;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Belt_Draw_Error()
        {
            Belt belt = new Belt(15);
            PictureBox pb1 = new PictureBox();
            Desk d = new Desk(DeskType.TOPLEFT);
            Cell c1 = new Cell(pb1, "c1", 1, 1, d);
            c1.Tag = Type.DOWNRIGHT;
            belt.DrawError(c1);
            c1.Tag = Type.HORIZONTAL;
            belt.DrawError(c1);
            c1.Tag = Type.RIGHTDOWN;
            belt.DrawError(c1);
            c1.Tag = Type.RIGHTUP;
            belt.DrawError(c1);
            c1.Tag = Type.UPRIGHT;
            belt.DrawError(c1);
            c1.Tag = Type.VERTICAL;
            belt.DrawError(c1);

            Assert.IsInstanceOfType(c1, typeof(Cell));
        }

        [TestMethod]
        public void Belt_Find_Path()
        {
            Form1 f = new Form1();
            f.airport = new Airport(10, 5);
            f.hasStart = true;

            f.grid[0].Tag = Type.HORIZONTAL;
            f.grid[0].PicBox.Tag = Type.HORIZONTAL;
            f.grid[1].Tag = Type.RIGHTDOWN;
            f.grid[1].PicBox.Tag = Type.RIGHTDOWN;
            f.grid[7].Tag = Type.VERTICAL;
            f.grid[7].PicBox.Tag = Type.VERTICAL;
            f.grid[13].Tag = Type.DOWNRIGHT;
            f.grid[13].PicBox.Tag = Type.DOWNRIGHT;
            f.grid[14].Tag = Type.RIGHTUP;
            f.grid[14].PicBox.Tag = Type.RIGHTUP;
            f.grid[8].Tag = Type.VERTICAL;
            f.grid[8].PicBox.Tag = Type.VERTICAL;
            f.grid[2].Tag = Type.UPRIGHT;
            f.grid[2].PicBox.Tag = Type.UPRIGHT;
            f.grid[3].Tag = Type.HORIZONTAL;
            f.grid[3].PicBox.Tag = Type.HORIZONTAL;
            f.grid[4].Tag = Type.HORIZONTAL;
            f.grid[4].PicBox.Tag = Type.HORIZONTAL;
            f.grid[5].Tag = Type.HORIZONTAL;
            f.grid[5].PicBox.Tag = Type.HORIZONTAL;

            f.btnStartEvent();
            Assert.IsTrue(f.GetTimer.Enabled);

            f.airport.belt.ClearBelt();
            Assert.AreEqual(0, f.airport.belt.Passed);
        }
    }
}
