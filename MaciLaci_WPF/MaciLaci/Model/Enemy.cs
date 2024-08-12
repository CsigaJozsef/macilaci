using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaciLaci.Persistence;

namespace MaciLaci.Model
{
    public enum Facing
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    //!!! A paneleknél az x y fel van cserélve ezért van a mozgásnál is megcserélve

    public class Enemy
    {
        private Point _startingPos;
        private Facing _startingFacing;
        private Point _pos;
        private Facing _facing;

        public Point Pos { get => _pos; }

        public Facing GetFacing { get => _facing; }

        public Enemy(Point pos, Color color, Facing facing)
        {
            _pos = pos;
            _startingPos = pos;


            _facing = facing;
            _startingFacing = facing;
        }

        public void reset()
        {
            _pos = _startingPos;
            _facing = _startingFacing;
        }


        public bool gentleMenWeGotHim(Point target)
        {
            if (target.Equals(_pos) ||
                target.X <= _pos.X + 1 && target.X >= _pos.X - 1 &&
                target.Y <= _pos.Y + 1 && target.Y >= _pos.Y - 1)
            {
                return true;
            }
            else { return false; }
        }

        public bool move(ref Fields gf, List<Point> blocking, Point target)
        {
            switch (_facing)
            {
                case Facing.NORTH:
                    return moveNorth(ref gf,blocking, target);
                case Facing.SOUTH:
                    return moveSouth(ref gf, blocking, target);
                case Facing.WEST:
                    return moveWest(ref gf, blocking, target);
                case Facing.EAST:
                    return moveEast(ref gf, blocking, target);
                default: return false;
            }
        }

        private bool moveNorth(ref Fields gf, List<Point> blocking, Point target)
        {
            for (int i = 0; i < blocking.Count; i++)
            {
                if (blocking[i].Y == _pos.Y - 1 && blocking[i].X == _pos.X)
                {
                    _facing = Facing.SOUTH;
                    return false;
                }
            }

            if (0 < _pos.Y)
            {
                gf.enemyMove(_pos,new Point(_pos.X, _pos.Y-1));
                --_pos.Y;
                return gentleMenWeGotHim(target);
            }
            else
            {
                _facing = Facing.SOUTH;
                return gentleMenWeGotHim(target);
            }

        }

        private bool moveSouth(ref Fields gf, List<Point> blocking, Point target)
        {
            for (int i = 0; i < blocking.Count; i++)
            {
                if (blocking[i].Y == _pos.Y + 1 && blocking[i].X == _pos.X)
                {
                    _facing = Facing.NORTH;
                    return false;
                }

            }

            if (gf.RowCount - 1 > _pos.Y)
            {
                gf.enemyMove(_pos, new Point(_pos.X, _pos.Y+1));
                ++_pos.Y;
                return gentleMenWeGotHim(target);
            }
            else
            {
                _facing = Facing.NORTH;
                return gentleMenWeGotHim(target);
            }

        }

        private bool moveWest(ref Fields gf, List<Point> blocking, Point target)
        {
            for (int i = 0; i < blocking.Count; i++)
            {
                if (blocking[i].Y == _pos.Y && blocking[i].X == _pos.X - 1)
                {
                    _facing = Facing.EAST;
                    return false;
                }
            }

            if (0 < _pos.X)
            {
                gf.enemyMove(_pos, new Point(_pos.X-1, _pos.Y));
                --_pos.X;
                return gentleMenWeGotHim(target);
            }
            else
            {
                _facing = Facing.EAST;
                return gentleMenWeGotHim(target);
            }
        }

        private bool moveEast(ref Fields gf, List<Point> blocking, Point target)
        {
            for (int i = 0; i < blocking.Count; i++)
            {
                if (blocking[i].Y == _pos.Y && blocking[i].X == _pos.X + 1)
                {
                    _facing = Facing.WEST;
                    return false;
                }
            }

            if (gf.ColumnCount - 1 > _pos.X)
            {
                gf.enemyMove(_pos, new Point(_pos.X+1, _pos.Y));
                ++_pos.X;
                return gentleMenWeGotHim(target);
            }
            else
            {
                _facing = Facing.WEST;
                return gentleMenWeGotHim(target);
            }

        }
    }
}
