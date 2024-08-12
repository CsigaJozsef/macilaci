using MaciLaci.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Persistence
{
    public enum GameStates
    {
        PRE_GAME,
        PAUSED,
        IN_GAME,
        WON,
        LOST
    }
    public class Field
    {
        private Point _size;
        private IGameObject[,] _gObj;

        public Field(int x, int y)
        {
            _size = new Point(x, y);
            _gObj = new IGameObject[x, y];

        }

        public Point Size { get => _size; }

        public bool isNull() { return (_gObj == null); }

        public void setNull() { _gObj = null; }
        public void set(Point pos, IGameObject entity) { _gObj[pos.X, pos.Y] = entity; }
        public void free(Point pos) { _gObj[pos.X, pos.Y] = null; }
        public IGameObject get(Point pos) { if (_gObj[pos.X, pos.Y] != null) return _gObj[pos.Y, pos.Y]; else return null; }
        public void initField(ref Dictionary<Point, Enemy> security,
            ref Dictionary<Point, Obstacle> obstacles,ref Dictionary<Point, Basket> baskets, ref Player player)
        {
            _gObj = new IGameObject[_size.X, _size.Y];
            _gObj[player.Pos.X, player.Pos.Y] = player;
            for (int i = 0; i < _size.X; i++)
            {
                for (int j = 0; j < _size.Y; j++)
                {
                    Point temp = new Point(i, j);
                    if (security.ContainsKey(temp))
                    {
                        _gObj[i, j] = security[temp];
                    }
                    else if (obstacles.ContainsKey(temp))
                    {
                        _gObj[i, j] = obstacles[temp];
                    }
                    else if (baskets.ContainsKey(temp))
                    {
                        _gObj[i, j] = baskets[temp];
                    }
                }
            }
        }
    }
}
