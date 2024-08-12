using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaciLaci.Persistence;

namespace MaciLaci.Model
{
    //!!! A paneleknél az x y fel van cserélve ezért van a mozgásnál is megcserélve

    public class Player : IGameObject
    {
        private Point _pos;
        private Color _color;
        public Color Color { get => _color; }
        public Point Pos { get => _pos; }
        public bool IsBasket { get => false; }

        public Player(Color color)
        {
            _color = color;
            _pos = new Point(0, 0);
        }

        public void reset()
        {
            _pos = new Point(0, 0);
        }

        public void move(ref Field gf, List<Point> blocking, char dir)
        {
            if (dir == 'w') { moveNorth(ref gf, blocking); }
            if (dir == 's') { moveSouth(ref gf, blocking); }
            if (dir == 'd') { moveEast(ref gf, blocking); }
            if (dir == 'a') { moveWest(ref gf, blocking); }
        }

        private void moveNorth(ref Field gf, List<Point> blocking)
        {
            for (int i = 0; i < blocking.Count; i++)
            { if (blocking[i].X == _pos.X - 1 && blocking[i].Y == _pos.Y) return; }
            if (0 < _pos.X)
            {
                gf.free(_pos);
                _pos.X--;
                gf.set(_pos, this);
            }
                
        }

        private void moveSouth(ref Field gf, List<Point> blocking)
        {
            for (int i = 0; i < blocking.Count; i++)
            { if (blocking[i].X == _pos.X + 1 && blocking[i].Y == _pos.Y) return; }
            if (gf.Size.X - 1 > _pos.X)
            {
                gf.free(_pos);
                _pos.X++;
                gf.set(_pos, this);
            }
                
        }

        private void moveWest(ref Field gf, List<Point> blocking)
        {
            for (int i = 0; i < blocking.Count; i++)
            { if (blocking[i].X == _pos.X && blocking[i].Y == _pos.Y - 1) return; }
            if (0 < _pos.Y)
            {
                gf.free(_pos);
                _pos.Y--;
                gf.set(_pos, this);
            }
                
        }

        private void moveEast(ref Field gf, List<Point> blocking)
        {
            for (int i = 0; i < blocking.Count; i++)
            { if (blocking[i].X == _pos.X && blocking[i].Y == _pos.Y + 1) return; }
            if (gf.Size.Y - 1 > _pos.Y)
            {
                gf.free(_pos);
                _pos.Y++;
                gf.set(_pos, this);
            }
                
        }
    }
}
