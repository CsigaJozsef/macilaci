using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Model
{
    public class Obstacle
    {
        private Point _pos;

        public Point Pos { get => _pos; }

        public Obstacle(Point position)
        {
            _pos = position;
        }
    }
}
