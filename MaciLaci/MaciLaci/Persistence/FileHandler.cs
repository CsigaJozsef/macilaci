using MaciLaci.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci.Persistence
{
    public class FileHandler
    {
        private readonly IFileReader _fileReader;

        public string EasyFileContent { get; private set; }
        public string MediumFileContent { get; private set; }
        public string HardFileContent { get; private set; }

        public FileHandler(IFileReader fileReader)
        {
            _fileReader = fileReader;
            EasyFileContent = _fileReader.Load(Difficulty.EASY);
            MediumFileContent = _fileReader.Load(Difficulty.MEDIUM);
            HardFileContent = _fileReader.Load(Difficulty.HARD);
        }

        /*
        public void init()
        {
            EasyFileContent = _fileReader.Load(Difficulty.EASY);
            MediumFileContent = _fileReader.Load(Difficulty.MEDIUM);
            HardFileContent = _fileReader.Load(Difficulty.HARD);
        }
        */

        public void Load(Difficulty diff, ref Field map, ref Dictionary<Point, Obstacle> obstMap,
            ref Dictionary<Point, Enemy> secMap, ref Dictionary<Point, Basket> basketMap)
        {

            obstMap = new Dictionary<Point, Obstacle>();
            secMap = new Dictionary<Point, Enemy>();
            basketMap = new Dictionary<Point, Basket>();

            string[] dataLine;
            switch (diff)
            {
                case Difficulty.EASY:
                    dataLine = EasyFileContent.Split("\n");
                    break;
                case Difficulty.MEDIUM:
                    dataLine = MediumFileContent.Split("\n");
                    break;
                case Difficulty.HARD:
                    dataLine = HardFileContent.Split("\n");
                    break;
                default:
                    throw new FileHandlerException("Loading unsuccessful invalid difficulty", new FileNotFoundException());

            }

            int lc = 0;
            string[] words = dataLine[lc].Split(" ");
            map = new Field(int.Parse(words[0]), int.Parse(words[1]));

            lc++;

            int obstCount = int.Parse(dataLine[lc]);
            lc++;
            for (int i = 0; i < obstCount; ++i)
            {
                words = dataLine[lc].Split();
                obstMap[new Point(int.Parse(words[0]), int.Parse(words[1]))] = new Obstacle(new Point(int.Parse(words[0]), int.Parse(words[1])));
                lc++;
            }
            int secCount = int.Parse(dataLine[lc]);
            lc++;
            for (int i = 0; i < secCount; ++i)
            {
                words = dataLine[lc].Split();
                Facing tempFacing;

                switch (words[2])
                {
                    case "N": tempFacing = Facing.NORTH; break;
                    case "S": tempFacing = Facing.SOUTH; break;
                    case "W": tempFacing = Facing.WEST; break;
                    case "E": tempFacing = Facing.EAST; break;
                    default: tempFacing = Facing.NORTH; break;
                }


                secMap[new Point(int.Parse(words[0]), int.Parse(words[1]))] = new Enemy(new Point(int.Parse(words[0]), int.Parse(words[1])), Color.Red, tempFacing);
                lc++;
            }
            int basketCount = int.Parse(dataLine[lc]);
            lc++;
            for (int i = 0; i < basketCount; ++i)
            {
                words = dataLine[lc].Split();
                basketMap[new Point(int.Parse(words[0]), int.Parse(words[1]))] = new Basket(new Point(int.Parse(words[0]), int.Parse(words[1])), Color.Yellow);
                lc++;
            }

            
        }
    }
}
