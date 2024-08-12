using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Model
{
    public class Obstacle : IGameObject
    {
        private Color _color;
        private Point _pos;

        public Color Color { get => _color; }
        public Point Pos { get => _pos; }
        public bool IsBasket { get => false; }

        public Obstacle(Point position)
        {
            _color = Color.Gray;

            _pos = position;
        }
    }
}
