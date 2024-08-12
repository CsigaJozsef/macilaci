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

        public void Load(ref macilaciGameModel model, ref Fields fields ,Difficulty diff)
        {

            string[] dataLine;
            switch (diff)
            {
                case Difficulty.EASY:
                    dataLine = EasyFileContent.Split("\n");
                    model.difficulty = Difficulty.EASY;
                    break;
                case Difficulty.MEDIUM:
                    dataLine = MediumFileContent.Split("\n");
                    model.difficulty = Difficulty.MEDIUM;
                    break;
                case Difficulty.HARD:
                    dataLine = HardFileContent.Split("\n");
                    model.difficulty = Difficulty.HARD;
                    break;
                default:
                    dataLine = EasyFileContent.Split("\n");
                    model.difficulty = Difficulty.EASY;
                    break;

            }

            int lc = 0;
            string[] words = dataLine[lc].Split(" ");
            fields.ColumnCount = int.Parse(words[0]);
            fields.RowCount = int.Parse(words[1]);

            fields.init();

            lc++;

            int oc = int.Parse(dataLine[lc]);
            lc++;
            for (int i = 0; i < oc; ++i)
            {
                words = dataLine[lc].Split();

                int col = int.Parse(words[0]);
                int row = int.Parse(words[1]);

                model.Obstacles.Add(new Obstacle(new Point(col, row)));
                model.blocking.Add(new Point(col, row));
                fields.set(col, row, fType.OBSTACLE);
                lc++;
            }
            int sc = int.Parse(dataLine[lc]);
            lc++;
            for (int i = 0; i < sc; ++i)
            {
                words = dataLine[lc].Split();
                Facing tf;

                int col = int.Parse(words[0]);
                int row = int.Parse(words[1]);

                switch (words[2])
                {
                    case "N": tf = Facing.NORTH; break;
                    case "S": tf = Facing.SOUTH; break;
                    case "W": tf = Facing.WEST; break;
                    case "E": tf = Facing.EAST; break;
                    default: tf = Facing.NORTH; break;
                }

                model.Enemies.Add(new Enemy(new Point(col,row), Color.Red, tf));
                fields.set(col, row, fType.ENEMY);
                lc++;
            }
            int basketCount = int.Parse(dataLine[lc]);
            lc++;
            for (int i = 0; i < basketCount; ++i)
            {
                words = dataLine[lc].Split();

                int col = int.Parse(words[0]);
                int row = int.Parse(words[1]);

                model.Baskets.Add(new Basket(new Point(int.Parse(words[0]), int.Parse(words[1])), Color.Yellow));
                fields.set(col, row, fType.BASKET);
                lc++;
            }
            fields.set(0, 0, fType.PLAYER);

            model.currPoints = 0;
            model.maxPoints = model.Baskets.Count;
        }
    }
}
