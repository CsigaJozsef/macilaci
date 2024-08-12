using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Model
{
    public interface IGameObject
    {
        public Color Color { get; }
        public Point Pos { get; }
        public bool IsBasket { get; }

    }
}
