using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP_App
{
    class Airport
    {
        private int maxCart { get; set; }
        private int maxEmployee { get; set; }
        public Belt belt { get; private set; }
        public int maxLuggage = 0;
        public int counter = 0;
        public Airport(int lug, int cart, int emp)
        {
            this.maxCart = cart;
            this.maxEmployee = emp;
            this.belt = new Belt(lug);
            this.maxLuggage = lug;
        }
        public string getStatistic()
        {
            return "Statistics: " + Environment.NewLine + "So far, " + this.belt.Passed.ToString() + " pieces of luggage are transported to planes";
        }
    }
}
