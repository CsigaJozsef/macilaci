using MaciLaci.Model;
using MaciLaci.Persistence;
using MedveLaszlo_MAUI.ViewModel;
using MedveLaszlo_MAUI.Persistence;


namespace MedveLaszlo_MAUI;

public partial class AppShell : Shell
{
	private FileHandler _fileHandler;
	private macilaciGameModel _macilaciGameModel;
	private Fields _fields;
	private MainViewModel _mainViewModel;

	private readonly IDispatcherTimer _timer;

    public AppShell(FileHandler fileHandler, macilaciGameModel GameModel, Fields Fields, MainViewModel ViewModel)
	{
		InitializeComponent();

		_fileHandler = fileHandler;
		_macilaciGameModel = GameModel;
		_fields = Fields;
		_mainViewModel = ViewModel;

        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += new EventHandler(_mainViewModel.enemyMove);
		_timer.Tick += new EventHandler(_mainViewModel.updateStatus);
		_timer.Start();
    }
}
