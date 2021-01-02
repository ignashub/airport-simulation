using System.Drawing;

namespace ProCP_App
{
    class Luggage
    {
        private Rectangle rectangle;
        private const int LUGGAGE_WIDTH = 16;
        private const int LUGGAGE_HEIGHT = 16;
        public Luggage(Point p, int id)
        {
            rectangle = new Rectangle(p.X, p.Y, LUGGAGE_WIDTH, LUGGAGE_HEIGHT);
            PathID = id;
        }
        public Rectangle Rectangle
        {
            get { return rectangle; }
            private set { rectangle = value; }
        }

        public int PathLocationIndex { get; set; }
        public int PathID { get; set; }
        public void UpdateLuggageLocation(Point p)
        {
            rectangle.X = p.X;
            rectangle.Y = p.Y;
        }
    }
}
