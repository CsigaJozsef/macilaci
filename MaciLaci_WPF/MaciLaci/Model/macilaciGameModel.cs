using MaciLaci.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Model
{
    
    public class macilaciGameModel
    {

        Player macilaci;

        public int maxPoints;
        public int currPoints;

        List<Obstacle> _obstacles;
        List<Enemy> _enemies;
        List<Basket> _baskets;

        public Difficulty difficulty;
        public GameStates gameState;

        public event EventHandler<bool> gameOver;

        public List<Enemy> Enemies { get => _enemies; set { _enemies = value; } }
        public List<Obstacle> Obstacles { get => _obstacles; set { _obstacles = value; } }
        public List<Basket> Baskets { get => _baskets; set { _baskets = value; } }

        public List<Point> blocking;
        public Point Laszlo { get => macilaci.Pos; }

        public Player MedveLaszlo { get => macilaci ; }

        public bool move(ref Fields fields, char dir)
        {
            bool l = macilaci.move(ref fields, blocking, dir);
            if (!weGood()) { gameState = GameStates.LOST; return l; }
            whereBasket();
            return l;
        }
        public bool weGood()
        {
            foreach(Enemy enemy in _enemies)
            {
                if(enemy.gentleMenWeGotHim(Laszlo)) 
                {
                    gameOver.Invoke(this, false);
                    return false; 
                }
                
            }
            return true;
        }
        public void whereBasket()
        {
            foreach(Basket basket in Baskets)
            {
                if (basket.Pos == macilaci.Pos && basket.notFound())
                {
                    ++currPoints;
                    basket.find();
                }
            }
            if (currPoints == maxPoints)
            {
                gameState = GameStates.WON;
                gameOver.Invoke(this, true);
            }
        }
        public bool moveEnemy(ref Fields fields)
        {
            bool l = false;
            foreach(Enemy enemy in _enemies)
            {
                if(enemy.move(ref fields, blocking, macilaci.Pos))
                {
                    l = true;
                }
            }
            if(l) gameState = GameStates.LOST;
            return l;
        }

        public macilaciGameModel()
        {
            macilaci = new Player();

            _enemies = new List<Enemy>();
            _obstacles = new List<Obstacle>();
            _baskets = new List<Basket>();

            blocking = new List<Point>();

            gameState = GameStates.PRE_GAME;
        }

    }
}
