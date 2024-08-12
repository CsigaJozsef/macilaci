using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Persistence
{
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }
    public interface IFileReader
    {
        string Load(Difficulty diff);
    }
}
