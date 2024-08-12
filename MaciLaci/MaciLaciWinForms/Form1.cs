using MaciLaci.Model;
using MaciLaci.Persistence;
using System;
using System.Numerics;

namespace MaciLaciWinForms
{
    public partial class Form1 : Form
    {
        GameStates GameState = GameStates.PRE_GAME;
        Difficulty GameDifficulty = Difficulty.EASY;

        Player macilaci;
        private Dictionary<Point, Enemy> secMap;
        private Dictionary<Point, Basket> basketMap;
        private Dictionary<Point, Obstacle> obstMap;

        private int points;
        private Field gameField;

        private DateTime startTime;
        private System.Windows.Forms.Timer timer;

        private DateTime pauseStartTime;
        private System.Windows.Forms.Timer pausedTimer;

        IFileReader fr;
        FileHandler fh;

        private Panel[,] _grid;

        public Form1()
        {
            InitializeComponent();

            fr = new FileReader("../../../maps/easyMap.txt", "../../../maps/mediumMap.txt", "../../../maps/hardMap.txt");
            fh = new FileHandler(fr);

            fh.Load(GameDifficulty, ref gameField!, ref obstMap!, ref secMap!, ref basketMap!);
            //gameField = new Field(10,10);
            macilaci = new Player(Color.LightBlue);

            timer = new System.Windows.Forms.Timer();
            pausedTimer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += UpdateStatusBar!;

            tableLayoutGrid.KeyPress += myKeyDown!;
            timer.Tick += secPassed!;
        }

        private void startButton_Clicked(object sender, EventArgs e)
        {
            //MessageBox.Show(macilaci.Pos.ToString()+", "+gameField.Size.ToString());
            if (GameState != GameStates.PRE_GAME || GameDifficulty != Difficulty.EASY)
            {
                gameField.setNull();
                obstMap.Clear();
                secMap.Clear();
                basketMap.Clear();
                fh.Load(GameDifficulty, ref gameField, ref obstMap, ref secMap, ref basketMap);
                macilaci.reset();
                timer.Stop();
                if (_grid != null)
                {
                    foreach (Panel panel in _grid)
                        tableLayoutGrid.Controls.Remove(panel);
                }
            }

            gameField.initField(ref secMap, ref obstMap, ref basketMap, ref macilaci);
            initGrid();

            points = 0;

            startTime = DateTime.Now;
            timer.Start();
            UpdateStatusBar(sender, e);
            tableLayoutGrid.Focus();

            GameState = GameStates.IN_GAME;
        }

        private void easyModButton_Click(object sender, EventArgs e)
        {
            GameDifficulty = Difficulty.EASY;
        }

        private void mediumModButton_Click(object sender, EventArgs e)
        {
            GameDifficulty = Difficulty.MEDIUM;

        }

        private void hardModButton_Click(object sender, EventArgs e)
        {
            GameDifficulty = Difficulty.HARD;
        }

        private void UpdateStatusBar(object sender, EventArgs e)
        {
            double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
            statusLabel.Text = $"Points: {points} | Elapsed time: {elapsedSeconds:F0} sec";
        }

        private void myKeyDown(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Space && GameState == GameStates.IN_GAME)
            {
                pausedTimer = new System.Windows.Forms.Timer();
                GameState = GameStates.PAUSED;
                timer.Stop();
                pauseStartTime = DateTime.Now;
                pausedTimer.Start();
                MessageBox.Show("Your game is now paused!\n\nHint: close this window and press space again to continue.");
                return;
            }

            if (e.KeyChar == (char)Keys.Space && GameState == GameStates.PAUSED)
            {
                GameState = GameStates.IN_GAME;
                pausedTimer.Stop();
                startTime += (DateTime.Now - pauseStartTime);
                timer.Start();
                return;
            }

            if (GameState != GameStates.IN_GAME)
                return;

            paint(macilaci.Pos,Color.LawnGreen);
            macilaci.move(ref gameField, new List<Point>(obstMap.Keys), e.KeyChar);
            paint(macilaci.Pos, macilaci.Color);

            bool end = false;
            foreach (KeyValuePair<Point, Enemy> entry in secMap)
            {
                end = entry.Value.gentleMenWeGotHim(macilaci.Pos);
                if (end) { gameEnded(false); }
            }

            Basket temp;
            if (basketMap.TryGetValue(macilaci.Pos, out temp!) && temp.notFound())
            {
                temp.find();
                points++;
            }

            if (points == basketMap.Count)
            {
                gameEnded(true);
            }
        }
        public void secPassed(object sender, EventArgs e)
        {
            if (GameState != GameStates.IN_GAME) { return; }
            bool end = false;
            foreach (KeyValuePair<Point, Enemy> entry in secMap)
            {
                paint(entry.Value.Pos, Color.LawnGreen);
                end = entry.Value.move(ref gameField, new List<Point>(obstMap.Keys), macilaci.Pos);
                paint(entry.Value.Pos, entry.Value.Color);
                if (end) { gameEnded(false); }
            }

        }

        void paint(Point pos, Color color) { _grid[pos.X, pos.Y].BackColor = color; }

        public void gameEnded(bool won)
        {
            if (won)
            {
                for (int i = 0; i < gameField.Size.X; i++)
                {
                    for (int j = 0; j < gameField.Size.Y; j++)
                    {
                        paint(new Point(i, j), Color.Green);
                    }
                }
                GameState = GameStates.WON;
                MessageBox.Show("BONFIRE LIT!");
            }
            else
            {
                for(int i = 0; i < gameField.Size.X; i++)
                {
                    for(int j = 0; j < gameField.Size.Y; j++)
                    {
                        paint(new Point(i,j), Color.Red);
                    }
                }
                GameState = GameStates.LOST;
                MessageBox.Show("YOU DIED!");
            }


        }

        private void initGrid()
        {
            _grid = new Panel[gameField.Size.X, gameField.Size.Y];
            tableLayoutGrid.RowCount = gameField.Size.X;
            tableLayoutGrid.ColumnCount = gameField.Size.Y;

            for (int i = 0; i < gameField.Size.X; i++)
            {
                for (int j = 0; j < gameField.Size.Y; j++)
                {
                    _grid[i, j] = new Panel(); //new Grid(i, j)
                    _grid[i, j].BackColor = Color.LawnGreen;
                    _grid[i, j].Dock = DockStyle.Fill;
                    tableLayoutGrid.Controls.Add(_grid[i, j], j, i);
                    
                    Point temp = new Point(i, j);
                    if (secMap.ContainsKey(temp))
                    {
                        _grid[i, j].BackColor = secMap[temp].Color;
                    }
                    else if (obstMap.ContainsKey(temp))
                    {
                        _grid[i, j].BackColor = obstMap[temp].Color;
                    }
                    else if (basketMap.ContainsKey(temp))
                    {
                        _grid[i, j].BackColor = basketMap[temp].Color;
                    }
                    
                }
            }

            tableLayoutGrid.RowStyles.Clear();
            tableLayoutGrid.ColumnStyles.Clear();

            for (int i = 0; i < gameField.Size.X; i++)
            {
                tableLayoutGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 1 / Convert.ToSingle(gameField.Size.X)));
            }
            for (int i = 0; i < gameField.Size.Y; i++)
            {
                tableLayoutGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1 / Convert.ToSingle(gameField.Size.Y)));
            }
            //MessageBox.Show(macilaci.Pos.ToString());
            _grid[0, 0].BackColor = macilaci.Color;
        }

    }
}