using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP_App
{
    public class Checkpoint
    {
        public double x { private set; get; }
        public double y { private set; get; }
        public Checkpoint neighbour = null;
        public double length;
        public int id;

        public Checkpoint(double x, double y, int id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }
        public Checkpoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void AddCheckpoint(Checkpoint c)
        {
            this.neighbour = c;
            this.length = Math.Sqrt(Math.Pow(this.x - c.x, 2) + Math.Pow(this.y - c.y, 2));
        }


    }
}
