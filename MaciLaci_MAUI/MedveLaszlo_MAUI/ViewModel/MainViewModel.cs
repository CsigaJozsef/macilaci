using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MaciLaci.Model;
using MaciLaci.Persistence;
using MedveLaszlo_MAUI.Persistence;

namespace MedveLaszlo_MAUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        FileHandler fh;

        private macilaciGameModel _model;
        private Fields _fields;

        public event EventHandler<bool> gameOver;

        public ObservableCollection<FieldViewModel> GameField { get; set; }

        //private int _fieldWidth = 32;
        //private int _fieldHeight = 32;
        private int _columns;
        private int _rows;
        string _progress;

        //private readonly IDispatcherTimer _timer;


        DateTime startTime;

        #endregion

        async Task log(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Tetris", message, "OK");
        }

        #region Properties

        public DelegateCommand StartButtonCommand { get; private set; }
        public DelegateCommand PauseButtonCommand { get; private set; }


        public DelegateCommand EasyButtonCommand { get; private set; }
        public DelegateCommand MediumButtonCommand { get; private set; }
        public DelegateCommand HardButtonCommand { get; private set; }

        public DelegateCommand UpButtonCommand { get; private set; }
        public DelegateCommand LeftButtonCommand { get; private set; }
        public DelegateCommand RightButtonCommand { get; private set; }
        public DelegateCommand DownButtonCommand { get; private set; }

        public int Columns
        {
            get {return _columns; }
            set
            {
                if (value != _columns)
                {
                    _columns = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ColumnDefinitions));
                }
            }
        }

        public ColumnDefinitionCollection ColumnDefinitions
        {
            get => new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition(GridLength.Star), Columns).ToArray());
        }

        public int Rows
        {
            get { return _rows; }
            set
            {
                if (_rows != value)
                {
                    _rows = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RowDefinitions));
                    
                }
            }
        }

        public RowDefinitionCollection RowDefinitions
        {
            get => new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition(GridLength.Star), Rows).ToArray());
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

        public MainViewModel(macilaciGameModel model, Fields fields, FileHandler fileHandler)
        {
            fh = new FileHandler();
            //log(fh.EasyFileContent);
            
            _model = model;
            _fields = fields;

            _columns = _fields.ColumnCount;
            _rows = _fields.RowCount;

            GameField = new ObservableCollection<FieldViewModel>();
            setupFields();
            Progress = "Nyomd meg a Játok-ot, hogy elinduljon a játék";

            _model.gameOver += drawEndScreen;

            StartButtonCommand = new DelegateCommand(param => onStart());
            PauseButtonCommand = new DelegateCommand(param => onPause());
            EasyButtonCommand = new DelegateCommand(param => PerformEasyGame());
            MediumButtonCommand = new DelegateCommand(param => PerformMediumGame());
            HardButtonCommand = new DelegateCommand(param => PerformHardGame());

            UpButtonCommand = new DelegateCommand(param => PerformMoveNorth());
            DownButtonCommand = new DelegateCommand(param => PerformMoveSouth());
            LeftButtonCommand = new DelegateCommand(param => PerformMoveWest());
            RightButtonCommand = new DelegateCommand(param => PerformMoveEast());

            //dispatcherTimer.Tick += new EventHandler(enemyMove);
            //dispatcherTimer.Tick += new EventHandler(updateStatus);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();
        }

        #endregion

        #region event_methods

        #endregion

        #region field_setup

        private void setupFields()
        {
            GameField.Clear();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    FieldViewModel field = new FieldViewModel();

                    OnFieldCreated(j, i, ref field);

                    field.Col = j;
                    field.Row = i;
                    field.FieldChangeCommand = new DelegateCommand(field =>
                    {
                        if (field is FieldViewModel viewModel)
                            FieldChange(viewModel);
                    });

                    GameField.Add(field);
                }
            }
        }

        private void FieldChange(FieldViewModel selectedField)
        {
            foreach (FieldViewModel field in GameField)
            {
                if (field.Col == selectedField.Col || field.Row == selectedField.Row)
                    field.Type = selectedField.Type;
            }

        }

        private void OnFieldCreated(int col, int row, ref FieldViewModel field)
        {
            if (_fields.get(col, row) != fType.GRASS)
            {
                field.Type = _fields.get(col, row);
            }
        }

        public void updateStatus(object sender, EventArgs e)
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
                Progress = $"Points: {_model.currPoints}/{_model.maxPoints} | Elapsed time: {elapsedSeconds:F0} sec";
            }

        }

        private FieldViewModel? GetFieldAt(int col, int row)
        {
            foreach (FieldViewModel field in GameField)
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

        /*
        public void timerTick()
        {
            if(_model.gameState == GameStates.IN_GAME)
            {
                enemyMove();
                updateStatus();
            }
        }*/

        public void enemyMove(object sender, EventArgs e)
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
                Progress = "BONFIRE LIT!";
                //gameOver.Invoke(this, l);
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
                Progress = "YOU DIED!";
                //gameOver.Invoke(this, l);
            }
        }
        #endregion

        #region player_movement

        private void PerformMoveNorth()
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

        private void PerformMoveSouth()
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

        private void PerformMoveEast()
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

        private void PerformMoveWest()
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

        DateTime pauseStart;
        DateTime pauseEnd;
        private void onPause()
        {
            if (_model.gameState == GameStates.IN_GAME)
            {
                _model.gameState = GameStates.PAUSED;
                pauseStart = DateTime.Now;

            }
            else if (_model.gameState == GameStates.PAUSED)
            {
                _model.gameState = GameStates.IN_GAME;
                pauseEnd = DateTime.Now;
                startTime += pauseEnd - pauseStart;
            }

        }

        private void onStart()
        {
            if (_model.gameState == GameStates.PRE_GAME)
            {
                startTime = DateTime.Now;
                _model.gameState = GameStates.IN_GAME;
            }
        }

        #endregion

        #region diff_change

        private void PerformEasyGame()
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

        private void PerformMediumGame()
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

        private void PerformHardGame()
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
    
        
        #endregion
    }

}
