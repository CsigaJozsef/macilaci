using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Model
{
    public class Basket
    {
        private Point _pos;
        private bool _available;

        //public bool available { get { return _available; } }
        public Point Pos { get => _pos; }

        public Basket(Point pos, Color color)
        {
            _pos = pos;
            _available = true;
        }

        public void reset()
        {
            _available = true;
        }

        public bool notFound()
        {
            if (_available)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void find() { _available = false; }
    }
}
