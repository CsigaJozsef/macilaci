using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaciWinForms
{
    public class Grid : Panel
    {
        private int _x;
        private int _y;

        public int GridX { get { return _x; } }
        public int GridY { get { return _y; } }

        public Grid(int x, int y) { _x = x; _y = y; }

        public void resetColor()
        {
            BackColor = Color.LawnGreen;
        }

        public void draw(Color color)
        {
            BackColor = color;
        }
    }
}
