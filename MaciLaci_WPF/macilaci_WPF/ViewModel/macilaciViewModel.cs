using MaciLaci.Model;
using MaciLaci.Persistence;
using macilaci_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace macilaci_WPF.ViewModel
{
    public class macilaciViewModel : ViewModelBase
    {
        #region Fields

        FileHandler fh;

        private macilaciGameModel _model;
        private Fields _fields;

        public event EventHandler<bool> gameOver;

        public ObservableCollection<macilaciFieldVM> GameField { get; set; }

        private int _fieldWidth = 32;
        private int _fieldHeight = 32;
        private int _columns;
        private int _rows;

        string _progress;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        DateTime startTime;

        #endregion

        #region Properties

        public int FieldWidth
        {
            get => _fieldWidth;
            set
            {
                _fieldWidth = value;
                OnPropertyChanged();
            }
        }

        public int FieldHeight
        {
            get => _fieldHeight;
            set
            {
                _fieldHeight = value;
                OnPropertyChanged();
            }
        }

        public int Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                OnPropertyChanged();
            }
        }

        public int Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged();
            }
        }

        public int FieldMatrixWidth
        {
            get => FieldWidth * Columns;
            set
            {
                OnPropertyChanged();
            }
        }

        public int FieldMatrixHeight
        {
            get => FieldHeight * Rows;
            set
            {
                OnPropertyChanged();
            }
        }

        public string Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public macilaciViewModel(macilaciGameModel model, Fields fields, FileHandler fileHandler)
        {
            //FileReader fr = new FileReader("../../../maps/easyMap.txt", "../../../maps/mediumMap.txt", "../../../maps/hardMap.txt");
            //fh = new FileHandler(fr);
            fh = fileHandler;
            _model = model;
            _fields = fields;
            fh.Load(ref _model,ref _fields,Difficulty.EASY);
            
            _columns = _fields.ColumnCount;
            _rows = _fields.RowCount;

            GameField = new ObservableCollection<macilaciFieldVM>();
            setupFields();
            Progress = "Nyomd meg a Játok-ot, hogy elinduljon a játék";

            _model.gameOver += drawEndScreen;

            dispatcherTimer.Tick += new EventHandler(enemyMove);
            dispatcherTimer.Tick += new EventHandler(updateStatus);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        #endregion

        #region field_setup

        private void setupFields()
        {
            GameField.Clear();
            
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    macilaciFieldVM field = new macilaciFieldVM();

                    OnFieldCreated(j,i,ref field);

                    field.Col = j;
                    field.Row = i;
                    field.FieldChangeCommand = new DelegateCommand(field =>
                    {
                        if (field is macilaciFieldVM viewModel)
                            FieldChange(viewModel);
                    });

                    GameField.Add(field);
                }
            }
        }

        private void FieldChange(macilaciFieldVM selectedField)
        {
            foreach(macilaciFieldVM field in GameField)
            {
                if (field.Col == selectedField.Col || field.Row == selectedField.Row)
                    field.Type = selectedField.Type;
            }

        }

        private void OnFieldCreated(int col, int row, ref macilaciFieldVM field)
        {
            if (_fields.get(col, row) != fType.GRASS)
            {
                field.Type = _fields.get(col, row);
            }
        }

        private void updateStatus(object sender, EventArgs e)
        {
            if(_model.gameState == GameStates.IN_GAME)
            {
                double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
                Progress = $"Points: {_model.currPoints}/{_model.maxPoints} | Elapsed time: {elapsedSeconds:F0} sec";
            }
            
        }

        private macilaciFieldVM? GetFieldAt(int col, int row)
        {
            foreach (macilaciFieldVM field in GameField)
            {
                if (field.Col == col && field.Row == row)
                {
                    return field;
                }
            }
            return null;
        }

        #endregion

        #region enemy_move
        private void enemyMove(object sender, EventArgs e)
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                bool l = _model.moveEnemy(ref _fields);
                foreach (Enemy enemy in _model.Enemies)
                {
                    switch (enemy.GetFacing)
                    {
                        case Facing.NORTH:
                            if (enemy.Pos.Y < Rows - 1)
                            {
                                GetFieldAt(enemy.Pos.X, enemy.Pos.Y).Type = fType.ENEMY;
                                GetFieldAt(enemy.Pos.X, enemy.Pos.Y + 1).Type = _fields.get(enemy.Pos.X, enemy.Pos.Y + 1);
                            }
                            break;
                        case Facing.SOUTH:
                            if (enemy.Pos.Y > 0)
                            {
                                GetFieldAt(enemy.Pos.X, enemy.Pos.Y).Type = fType.ENEMY;
                                GetFieldAt(enemy.Pos.X, enemy.Pos.Y - 1).Type = _fields.get(enemy.Pos.X, enemy.Pos.Y - 1);
                            }
                            break;
                        case Facing.WEST:
                            if (enemy.Pos.X < Columns - 1)
                            {
                                GetFieldAt(enemy.Pos.X, enemy.Pos.Y).Type = fType.ENEMY;
                                GetFieldAt(enemy.Pos.X + 1, enemy.Pos.Y).Type = _fields.get(enemy.Pos.X + 1, enemy.Pos.Y);
                            }
                            break;
                        case Facing.EAST:
                            if (enemy.Pos.X > 0)
                            {
                                GetFieldAt(enemy.Pos.X, enemy.Pos.Y).Type = fType.ENEMY;
                                GetFieldAt(enemy.Pos.X - 1, enemy.Pos.Y).Type = _fields.get(enemy.Pos.X - 1, enemy.Pos.Y);
                            }
                            break;
                        default: break;
                    }
                }
                //if (_model.gameState == GameStates.LOST) drawEndScreen(false); 
                
            }
        }

        #endregion

        #region end_screen
        private void drawEndScreen(object? sender, bool l)
        {
            if (l)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        GetFieldAt(i, j).BackgroundBrush = new SolidColorBrush(Colors.Green);
                    }
                }
                gameOver.Invoke(this, l);
            }
            else
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        GetFieldAt(i, j).BackgroundBrush = new SolidColorBrush(Colors.Red);
                    }
                }
                gameOver.Invoke(this, l);
            }
        }
        #endregion

        #region player_movement

        private DelegateCommand moveNorth;
        public ICommand MoveNorth => moveNorth ??= new DelegateCommand(PerformMoveNorth);

        private void PerformMoveNorth(object commandParameter)
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                bool l = _model.move(ref _fields, 'w');
                if (l && _model.gameState == GameStates.IN_GAME)
                {
                    GetFieldAt(_model.Laszlo.X, _model.Laszlo.Y).Type = fType.PLAYER;
                    GetFieldAt(_model.Laszlo.X, _model.Laszlo.Y + 1).Type = _fields.get(_model.Laszlo.X, _model.Laszlo.Y + 1);
                }

                if (_model.gameState == GameStates.WON)
                {
                    //drawEndScreen(true);
                }
                else if (_model.gameState == GameStates.LOST)
                {
                    //drawEndScreen(false);
                }
            }
        }

        private DelegateCommand moveSouth;
        public ICommand MoveSouth => moveSouth ??= new DelegateCommand(PerformMoveSouth);

        private void PerformMoveSouth(object commandParameter)
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                bool l = _model.move(ref _fields, 's');
                if (l && _model.gameState == GameStates.IN_GAME)
                {
                    GetFieldAt(_model.Laszlo.X, _model.Laszlo.Y).Type = fType.PLAYER;
                    GetFieldAt(_model.Laszlo.X, _model.Laszlo.Y - 1).Type = _fields.get(_model.Laszlo.X, _model.Laszlo.Y - 1);

                }

                if (_model.gameState == GameStates.WON)
                {
                    //drawEndScreen(true);
                }
                else if (_model.gameState == GameStates.LOST)
                {
                    //drawEndScreen(false);
                }
            }
        }

        private DelegateCommand moveEast;
        public ICommand MoveEast => moveEast ??= new DelegateCommand(PerformMoveEast);

        private void PerformMoveEast(object commandParameter)
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                bool l = _model.move(ref _fields, 'd');
                if (l && _model.gameState == GameStates.IN_GAME)
                {
                    GetFieldAt(_model.Laszlo.X, _model.Laszlo.Y).Type = fType.PLAYER;
                    GetFieldAt(_model.Laszlo.X - 1, _model.Laszlo.Y).Type = _fields.get(_model.Laszlo.X - 1, _model.Laszlo.Y);

                }

                if (_model.gameState == GameStates.WON)
                {
                    //drawEndScreen(true);
                }
                else if (_model.gameState == GameStates.LOST)
                {
                    //drawEndScreen(false);
                }
            }
        }

        private DelegateCommand moveWest;
        public ICommand MoveWest => moveWest ??= new DelegateCommand(PerformMoveWest);

        private void PerformMoveWest(object commandParameter)
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                bool l = _model.move(ref _fields, 'a');
                if (l && _model.gameState == GameStates.IN_GAME)
                {
                    GetFieldAt(_model.Laszlo.X, _model.Laszlo.Y).Type = fType.PLAYER;
                    GetFieldAt(_model.Laszlo.X + 1, _model.Laszlo.Y).Type = _fields.get(_model.Laszlo.X + 1, _model.Laszlo.Y);

                }

                if (_model.gameState == GameStates.WON)
                {
                    //drawEndScreen(true);
                }
                else if (_model.gameState == GameStates.LOST)
                {
                    //drawEndScreen(false);
                }
            }
        }

        #endregion

        #region game_pause_start
        private DelegateCommand pauseGame;
        public ICommand PauseGame => pauseGame ??= new DelegateCommand(PerformPauseGame);

        DateTime pauseStart;
        DateTime pauseEnd;
        private void PerformPauseGame(object commandParameter)
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                _model.gameState = GameStates.PAUSED;
                pauseStart = DateTime.Now;

            }else if (_model.gameState == GameStates.PAUSED)
            {
                _model.gameState = GameStates.IN_GAME;
                pauseEnd = DateTime.Now;
                startTime += pauseEnd - pauseStart;
            }

        }

        private DelegateCommand startGame;
        public ICommand StartGame => startGame ??= new DelegateCommand(PerformStartGame);

        private void PerformStartGame(object commandParameter)
        {
            if (_model.gameState == GameStates.PRE_GAME)
            {
                startTime = DateTime.Now;
                _model.gameState = GameStates.IN_GAME;
            }
        }

        #endregion

        #region diff_change

        private DelegateCommand easyGame;
        public ICommand EasyGame => easyGame ??= new DelegateCommand(PerformEasyGame);

        private void PerformEasyGame(object commandParameter)
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.EASY);

            Columns = _fields.ColumnCount;
            Rows = _fields.RowCount;

            setupFields();
            _model.gameOver += drawEndScreen;
            Progress = "Nyomd meg a Játok-ot, hogy elinduljon a játék";
        }

        private DelegateCommand mediumGame;
        public ICommand MediumGame => mediumGame ??= new DelegateCommand(PerformMediumGame);

        private void PerformMediumGame(object commandParameter)
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.MEDIUM);

            Columns = _fields.ColumnCount;
            Rows = _fields.RowCount;

            setupFields();
            _model.gameOver += drawEndScreen;
            Progress = "Nyomd meg a Játok-ot, hogy elinduljon a játék";
        }

        private DelegateCommand hardGame;
        public ICommand HardGame => hardGame ??= new DelegateCommand(PerformHardGame);

        private void PerformHardGame(object commandParameter)
        {
            _model = new macilaciGameModel();
            _fields = new Fields();
            fh.Load(ref _model, ref _fields, Difficulty.HARD);
            

            Columns = _fields.ColumnCount;
            Rows = _fields.RowCount;

            setupFields();
            _model.gameOver += drawEndScreen;
            Progress = "Nyomd meg a Játok-ot, hogy elinduljon a játék";            
        }
    }
    #endregion

}