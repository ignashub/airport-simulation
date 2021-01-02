using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP_App
{
    class Luggage
    {
        public Point position;
        public int Counter;
        public Cell cell { private set; get; }
        public Luggage(int x, int y)
        {
            this.position = new Point(x, y);
            this.Counter = 0;
        }

        public Point getLocation()
        {
            return this.position;
        }

        public void updateLocation(int x, int y)
        {
            int curX = this.position.X;
            int curY = this.position.Y;
            curX += x;
            curY += y;
            this.position = new Point(curX, curY);
        }

        public void setLocation(int x, int y)
        {
            this.position = new Point(x, y);
        }
    }
}
