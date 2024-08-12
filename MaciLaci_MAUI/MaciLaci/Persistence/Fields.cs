using MaciLaci.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
    public enum fType
    {
        PLAYER,
        GRASS,
        OBSTACLE,
        ENEMY,
        BASKET
    }
    public class Fields
    {
        private int _col;
        private int _row;
        private fType[,] _type;

        public Fields(){}

        public int RowCount { get => _row; set { _row = value; } }
        public int ColumnCount { get => _col; set { _col = value; } }
        public fType get(int x, int y) { return _type[x, y]; }
        public void set(int x, int y, fType newType) { _type[x, y] = newType; }

        public void playerMove(Point from, Point to)
        {
            _type[from.X, from.Y] = fType.GRASS;
            _type[to.X, to.Y] = fType.PLAYER;
        }

        public void enemyMove(Point from, Point to)
        {
            _type[from.X, from.Y] = fType.GRASS;
            _type[to.X, to.Y] = fType.ENEMY;
        }

        public void init()
        {
            _type = new fType[_col, _row];

            for (int i = 0; i < _col; ++i)
            {
                for(int j = 0; j < _row; ++j)
                {
                    _type[i, j] = fType.GRASS;
                }
            }
        }
    }
}
