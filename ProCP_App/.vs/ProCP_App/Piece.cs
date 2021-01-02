using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP_App
{
    class Piece
    {
        public Piece(Type type)
        {
            Type = type;
        }
        private Type type;
        public Type Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
