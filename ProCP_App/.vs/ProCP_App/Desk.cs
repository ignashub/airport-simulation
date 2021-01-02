using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP_App
{
    public enum DeskType
    {
        TOPLEFT,
        TOPRIGHT,
        BOTTOMLEFT,
        BOTTOMRIGHT
    }
    class Desk
    {
        public Desk(DeskType type)
        {
            Type = type;
        }
        private DeskType type;
        public DeskType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
