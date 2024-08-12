using MaciLaci.Persistence;
using MaciLaci.Model;

namespace macilaci_TEST
{
    [TestClass]
    public class macilaciTest
    {
        private FileHandler fh;
        private macilaciGameModel _model;
        private Fields _fields;


        [TestInitialize]
        public void Initialize()
        {
            FileReader fr = new FileReader("../../../maps/easyMap.txt", "../../../maps/mediumMap.txt", "../../../maps/hardMap.txt");
            fh = new FileHandler(fr);
            _model = new macilaciGameModel();
            _fields = new Fields();
            
        }


        [TestMethod]
        public void TestLoadEasy()
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.EASY);

            Assert.IsTrue(_model.Laszlo.X == 0 && _model.Laszlo.Y == 0);

            Assert.IsTrue(_model.Baskets.Count == 2);
            Assert.IsTrue(_fields.get(9,9) == fType.BASKET);
            Assert.IsTrue(_fields.get(3,7) == fType.BASKET);

            Assert.IsTrue(_model.Obstacles.Count == 5);
            Assert.IsTrue(_fields.get(3, 3) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(6, 6) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(4, 7) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(1, 9) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(4, 4) == fType.OBSTACLE);

            Assert.IsTrue(_model.Enemies.Count == 2);
            Assert.IsTrue(_fields.get(3, 4) == fType.ENEMY);
            Assert.IsTrue(_fields.get(5, 7) == fType.ENEMY);
            
        }
        

        [TestMethod]
        public void TestLoadMedium()
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.MEDIUM);

            Assert.IsTrue(_model.Laszlo.X == 0 && _model.Laszlo.Y == 0);

            Assert.IsTrue(_model.Baskets.Count == 3);
            Assert.IsTrue(_fields.get(9, 9) == fType.BASKET);
            Assert.IsTrue(_fields.get(7, 8) == fType.BASKET);
            Assert.IsTrue(_fields.get(1, 10) == fType.BASKET);

            Assert.IsTrue(_model.Obstacles.Count == 5);
            Assert.IsTrue(_fields.get(3, 3) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(6, 6) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(4, 7) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(1, 9) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(4, 4) == fType.OBSTACLE);

            Assert.IsTrue(_model.Enemies.Count == 3);
            Assert.IsTrue(_fields.get(3, 4) == fType.ENEMY);
            Assert.IsTrue(_fields.get(5, 7) == fType.ENEMY);
            Assert.IsTrue(_fields.get(8, 7) == fType.ENEMY);

        }

        [TestMethod]
        public void TestLoadHard()
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.HARD);

            Assert.IsTrue(_model.Laszlo.X == 0 && _model.Laszlo.Y == 0);

            Assert.IsTrue(_model.Baskets.Count == 2);
            Assert.IsTrue(_fields.get(9, 9) == fType.BASKET);
            Assert.IsTrue(_fields.get(4, 8) == fType.BASKET);

            Assert.IsTrue(_model.Obstacles.Count == 5);
            Assert.IsTrue(_fields.get(3, 3) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(6, 6) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(4, 7) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(1, 9) == fType.OBSTACLE);
            Assert.IsTrue(_fields.get(4, 4) == fType.OBSTACLE);

            Assert.IsTrue(_model.Enemies.Count == 2);
            Assert.IsTrue(_fields.get(3, 4) == fType.ENEMY);
            Assert.IsTrue(_fields.get(5, 7) == fType.ENEMY);

        }

        [TestMethod]
        public void MedveLaszloOnTheMove()
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.EASY);

            Assert.IsTrue(_model.Laszlo.X == 0 && _model.Laszlo.Y == 0);

            _model.move(ref _fields, 'd');

            Assert.IsFalse(_model.Laszlo.X == 0 && _model.Laszlo.Y == 0);
            Assert.IsTrue(_model.Laszlo.X == 1 && _model.Laszlo.Y == 0);
        }

        [TestMethod]
        public void EnemyOnTheMove()
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.EASY);

            Assert.IsTrue(_model.Enemies[0].Pos.X == 3 && _model.Enemies[0].Pos.Y == 4 
                && _model.Enemies[0].GetFacing == Facing.EAST);
            Assert.IsTrue(_model.Obstacles[4].Pos.X == 4 && _model.Obstacles[4].Pos.Y == 4);

            _model.moveEnemy(ref _fields);

            Assert.IsFalse(_model.Enemies[0].Pos.X == 3 && _model.Enemies[0].Pos.Y == 4
                && _model.Enemies[0].GetFacing == Facing.EAST);
            Assert.IsTrue(_model.Enemies[0].Pos.X == 3 && _model.Enemies[0].Pos.Y == 4
                && _model.Enemies[0].GetFacing == Facing.WEST);

            _model.moveEnemy(ref _fields);

            Assert.IsFalse(_model.Enemies[0].Pos.X == 3 && _model.Enemies[0].Pos.Y == 4
                && _model.Enemies[0].GetFacing == Facing.WEST);
            Assert.IsTrue(_model.Enemies[0].Pos.X == 2 && _model.Enemies[0].Pos.Y == 4
                && _model.Enemies[0].GetFacing == Facing.WEST);
        }

        [TestMethod]
        public void BasketsUnFound()
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.EASY);

            foreach (Basket basket in _model.Baskets)
            {
                Assert.IsTrue(basket.notFound());
            }
        }

        [TestMethod]
        public void BasketWhenFound()
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.EASY);

            foreach (Basket basket in _model.Baskets)
            {
                basket.find();
            }

            foreach (Basket basket in _model.Baskets)
            {
                Assert.IsFalse(basket.notFound());
            }
        }
    }
}