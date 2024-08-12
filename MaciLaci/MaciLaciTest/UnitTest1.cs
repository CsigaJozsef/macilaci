using Castle.Components.DictionaryAdapter.Xml;
using MaciLaci.Model;
using MaciLaci.Persistence;
using Moq;
using System.Drawing;
using System.Reflection;

namespace MaciLaciTest
{
    [TestClass]
    public class UnitTest1
    {

        //!!! A paneleknél az x y fel van cserélve ezért van a mozgásnál is megcserélve

        //private Field _testField = null!;
        private Player _player = null!;
        private Basket _basket = null!;
        private Obstacle _obstacle = null!;
        private Enemy _enemy = null!;

        Player macilaci;
        private Dictionary<Point, Enemy> secMap = new Dictionary<Point, Enemy>();
        private Dictionary<Point, Basket> basketMap = new Dictionary<Point, Basket>();
        private Dictionary<Point, Obstacle> obstMap = new Dictionary<Point, Obstacle>();

        private Field _mockedField = null!;
        private IFileReader _fileReader = null!;
        private FileHandler _fileHandler = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockedField = new Field(10, 10);

            
            obstMap[new Point(3, 3)] = new Obstacle(new Point(3, 3));
            obstMap[new Point(6, 6)] = new Obstacle(new Point(6, 6));
            obstMap[new Point(4, 7)] = new Obstacle(new Point(4, 7));
            obstMap[new Point(1, 9)] = new Obstacle(new Point(1, 9));
            obstMap[new Point(4, 4)] = new Obstacle(new Point(4, 4));

            secMap[new Point(3, 4)] = new Enemy(new Point(3, 4), Color.Red, Facing.EAST);
            secMap[new Point(5, 7)] = new Enemy(new Point(5, 7), Color.Red, Facing.SOUTH);

            basketMap[new Point(9, 9)] = new Basket(new Point(9, 9), Color.Yellow);
            basketMap[new Point(5, 8)] = new Basket(new Point(5, 8), Color.Yellow);
            basketMap[new Point(1, 0)] = new Basket(new Point(5, 8), Color.Yellow);
            
            macilaci = new Player(Color.LightBlue);

            _mockedField.initField(ref secMap, ref obstMap, ref basketMap, ref macilaci);

            _fileReader = new FileReader("../../../maps/easyMap.txt", "../../../maps/mediumMap.txt", "../../../maps/hardMap.txt");
            _fileHandler = new FileHandler(_fileReader);
            //_mock = new Mock<IFileReader>();
            //_fileHandler = new FileHandler(_mock.Object);
        }
        
        

        [TestMethod]
        public void TestLoadEasy()
        {
            _fileHandler.Load(Difficulty.EASY,ref _mockedField,ref obstMap,ref secMap,ref basketMap);
            _mockedField.initField(ref secMap, ref obstMap, ref basketMap, ref macilaci);

            Assert.IsTrue(secMap.Count == 2);
            Assert.IsTrue(secMap.ContainsKey(new Point(3, 4)));
            Assert.IsTrue(secMap.ContainsKey(new Point(5, 7)));

            Assert.IsTrue(obstMap.Count == 5);
            Assert.IsTrue(obstMap.ContainsKey(new Point(3, 3)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(6, 6)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(4, 4)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(1, 9)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(4, 7)));

            Assert.IsTrue(basketMap.Count == 2);
            Assert.IsTrue(basketMap.ContainsKey(new Point(9, 9)));
            Assert.IsTrue(basketMap.ContainsKey(new Point(5, 8)));

        }

        [TestMethod]
        public void TestLoadMedium()
        {
            _fileHandler.Load(Difficulty.MEDIUM, ref _mockedField, ref obstMap, ref secMap, ref basketMap);
            _mockedField.initField(ref secMap, ref obstMap, ref basketMap, ref macilaci);

            Assert.IsTrue(secMap.Count == 3);
            Assert.IsTrue(secMap.ContainsKey(new Point(3, 4)));
            Assert.IsTrue(secMap.ContainsKey(new Point(5, 7)));
            Assert.IsTrue(secMap.ContainsKey(new Point(8, 7)));

            Assert.IsTrue(obstMap.Count == 5);
            Assert.IsTrue(obstMap.ContainsKey(new Point(3, 3)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(6, 6)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(4, 4)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(1, 9)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(4, 7)));

            Assert.IsTrue(basketMap.Count == 3);
            Assert.IsTrue(basketMap.ContainsKey(new Point(9, 9)));
            Assert.IsTrue(basketMap.ContainsKey(new Point(7, 8)));
            Assert.IsTrue(basketMap.ContainsKey(new Point(1, 10)));

        }

        [TestMethod]
        public void TestLoadHard()
        {
            _fileHandler.Load(Difficulty.HARD, ref _mockedField, ref obstMap, ref secMap, ref basketMap);
            _mockedField.initField(ref secMap, ref obstMap, ref basketMap, ref macilaci);

            Assert.IsTrue(secMap.Count == 2);
            Assert.IsTrue(secMap.ContainsKey(new Point(3, 4)));
            Assert.IsTrue(secMap.ContainsKey(new Point(5, 7)));

            Assert.IsTrue(obstMap.Count == 5);
            Assert.IsTrue(obstMap.ContainsKey(new Point(3, 3)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(6, 6)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(4, 4)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(1, 9)));
            Assert.IsTrue(obstMap.ContainsKey(new Point(4, 7)));

            Assert.IsTrue(basketMap.Count == 2);
            Assert.IsTrue(basketMap.ContainsKey(new Point(9, 9)));
            Assert.IsTrue(basketMap.ContainsKey(new Point(5, 8)));

        }

        [TestMethod]
        public void MaciLaciStartingPosition()
        {
            Assert.IsTrue(macilaci.Pos == new Point(0, 0));
            Assert.IsTrue(_mockedField.get(macilaci.Pos) == macilaci);
        }

        [TestMethod]
        public void MaciLaciMovePosition()
        {
            Assert.IsTrue(macilaci.Pos == new Point(0, 0));
            macilaci.move(ref _mockedField, new List<Point>(obstMap.Keys), 's');
            Assert.IsTrue(macilaci.Pos.X == 1);
        }

        [TestMethod]
        public void EnemyMove()
        {
            Assert.IsTrue(secMap[new Point(3,4)].Pos == new Point(3,4));
            secMap[new Point(3, 4)].move(ref _mockedField, new List<Point>(obstMap.Keys), macilaci.Pos);
            Assert.IsTrue(secMap[new Point(3, 4)].Pos == new Point(3,5));
        }

        [TestMethod]
        public void BasketsUnFound()
        {
            foreach(KeyValuePair<Point, Basket> basket in basketMap)
            {
                Assert.IsTrue(basket.Value.notFound());
            }
        }

        [TestMethod]
        public void BasketWhenFound()
        {
            macilaci.move(ref _mockedField, new List<Point>(obstMap.Keys), 's');
            Assert.IsTrue(basketMap[macilaci.Pos] != null);
            Basket temp;
            if (basketMap.TryGetValue(macilaci.Pos, out temp!) && temp.notFound())
            {
                temp.find();
                //points++;
            }
            Assert.IsTrue(!basketMap[macilaci.Pos].notFound());
        }


    }
}