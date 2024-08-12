using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaciLaci.Persistence;

namespace MaciLaci.Model
{
    
    public class Player
    {
        private Point _pos;
        public Point Pos { get => _pos; }

        public Player()
        {
            _pos = new Point(0, 0);
        }

        public void reset()
        {
            _pos = new Point(0, 0);
        }

        public bool move(ref Fields gf, List<Point> blocking, char dir)
        {
            if (dir == 'w') { return moveNorth(ref gf, blocking); }
            if (dir == 's') { return moveSouth(ref gf, blocking); }
            if (dir == 'd') { return moveEast(ref gf, blocking); }
            if (dir == 'a') { return moveWest(ref gf, blocking); }
            return false;
        }

        private bool moveNorth(ref Fields gf, List<Point> blocking)
        {
            for (int i = 0; i < blocking.Count; i++)
            { if (blocking[i].X == _pos.X && blocking[i].Y == _pos.Y - 1) return false; }
            if (0 < _pos.Y)
            {
                gf.playerMove(_pos, new Point(_pos.X,_pos.Y-1));
                --_pos.Y;
                return true;
            }
            return false;
        }

        private bool moveSouth(ref Fields gf, List<Point> blocking)
        {
            foreach (Point block in blocking)
            { if (block.Y == _pos.Y + 1 && block.X == _pos.X) return false; }
            if (gf.RowCount - 1 > _pos.Y)
            {
                gf.playerMove(_pos, new Point(_pos.X, _pos.Y+1));
                ++_pos.Y;
                return true;
            }
            return false;
        }

        private bool moveWest(ref Fields gf, List<Point> blocking)
        {
            for (int i = 0; i < blocking.Count; i++)
            { if (blocking[i].X == _pos.X - 1 && blocking[i].Y == _pos.Y) return false; }
            if (0 < _pos.X)
            {
                gf.playerMove(_pos, new Point(_pos.X - 1, _pos.Y));
                --_pos.X;
                return true;
            }
            return false;
        }

        private bool moveEast(ref Fields gf, List<Point> blocking)
        {
            for (int i = 0; i < blocking.Count; i++)
            { if (blocking[i].X == _pos.X + 1 && blocking[i].Y == _pos.Y) return false; }
            if (gf.ColumnCount - 1 > _pos.X)
            {
                gf.playerMove(_pos, new Point(_pos.X + 1, _pos.Y));
                ++_pos.X;
                return true;
            }
            return false;
                
        }
    }
}
