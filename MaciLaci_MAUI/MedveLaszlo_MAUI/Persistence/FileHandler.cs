using System;
using MaciLaci.Model;
using MaciLaci.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedveLaszlo_MAUI.Persistence
{
    public class FileHandler
    {
        private readonly FileReader _fileReader;

        public string EasyFileContent { get; private set; }
        public string MediumFileContent { get; private set; }
        public string HardFileContent { get; private set; }

        public FileHandler()
        {
            EasyFileContent = string.Empty;
            MediumFileContent = string.Empty;
            HardFileContent = string.Empty;
            ReadEasyMap();
            ReadMediumMap();
            ReadHardMap();
        }

        private async Task<string> LoadMauiAsset(string fileName)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        public async Task ReadEasyMap()
        {
            EasyFileContent = await LoadMauiAsset("maps/easyMap.txt");
        }
        public async Task ReadMediumMap()
        {
            MediumFileContent = await LoadMauiAsset("maps/mediumMap.txt");
        }

        public async Task ReadHardMap()
        {
            HardFileContent = await LoadMauiAsset("maps/hardMap.txt");
        }

        /*
        public void init()
        {
            EasyFileContent = _fileReader.Load(Difficulty.EASY);
            MediumFileContent = _fileReader.Load(Difficulty.MEDIUM);
            HardFileContent = _fileReader.Load(Difficulty.HARD);
        }
        */

        public void Load(ref macilaciGameModel model, ref Fields fields, Difficulty diff)
        {

            string[] dataLine;
            switch (diff)
            {
                case Difficulty.EASY:
                    dataLine = EasyFileContent.Split("\r\n");
                    model.difficulty = Difficulty.EASY;
                    break;
                case Difficulty.MEDIUM:
                    dataLine = MediumFileContent.Split("\r\n");
                    model.difficulty = Difficulty.MEDIUM;
                    break;
                case Difficulty.HARD:
                    dataLine = HardFileContent.Split("\r\n");
                    model.difficulty = Difficulty.HARD;
                    break;
                default:
                    dataLine = EasyFileContent.Split("\r\n");
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

                model.Obstacles.Add(new Obstacle(new System.Drawing.Point(col, row)));
                model.blocking.Add(new System.Drawing.Point(col, row));
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

                model.Enemies.Add(new Enemy(new System.Drawing.Point(col, row), tf));
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

                model.Baskets.Add(new Basket(new System.Drawing.Point(int.Parse(words[0]), int.Parse(words[1]))));
                fields.set(col, row, fType.BASKET);
                lc++;
            }
            fields.set(0, 0, fType.PLAYER);

            model.currPoints = 0;
            model.maxPoints = model.Baskets.Count;
        }
    }
}
