using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Model
{
    public class Basket : IGameObject
    {
        private Point _pos;
        private bool _available;
        private Color _color;

        //public bool available { get { return _available; } }
        public Color Color { get => _color; }
        public Point Pos { get => _pos; }
        public bool IsBasket { get => true; }

        public Basket(Point pos, Color color)
        {
            _pos = pos;
            _available = true;
            _color = color;
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
