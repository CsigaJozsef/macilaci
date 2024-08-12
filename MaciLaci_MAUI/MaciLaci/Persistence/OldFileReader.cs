using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaciLaci.Model;

namespace MaciLaci.Persistence
{
    public class OldFileReader
    {
        private readonly string _easyMapPath;
        private readonly string _mediumMapPath;
        private readonly string _hardMapPath;

        public OldFileReader(string easyMapPath, string mediumMapPath, string hardMapPath)
        {
            _easyMapPath = easyMapPath;
            _mediumMapPath = mediumMapPath;
            _hardMapPath = hardMapPath;
        }

        public string Load(Difficulty diff)
        {
            try
            {
                switch (diff)
                {
                    case Difficulty.EASY:
                        return File.ReadAllText(_easyMapPath);
                    case Difficulty.MEDIUM:
                        return File.ReadAllText(_mediumMapPath);
                    case Difficulty.HARD:
                        return File.ReadAllText(_hardMapPath);
                    default:
                        throw new OldFileReaderException("Invalid difficulty", new FileNotFoundException());
                }
            }
            catch (Exception ex)
            {
                throw new OldFileReaderException(ex.Message, ex);
            }

        }
    }
}
