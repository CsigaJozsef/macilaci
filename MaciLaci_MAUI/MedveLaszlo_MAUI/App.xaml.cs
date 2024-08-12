using MedveLaszlo_MAUI.ViewModel;
using MedveLaszlo_MAUI.Persistence;
using MaciLaci.Persistence;
using MaciLaci.Model;

namespace MedveLaszlo_MAUI;

public partial class App : Application
{
	private readonly AppShell _appShell;

	//private readonly FileReader _fr;
	private readonly FileHandler _fh;
	private readonly macilaciGameModel GameModel;
	private readonly Fields Fields;

	private MainViewModel ViewModel;
	public App()
	{
		InitializeComponent();

        GameModel = new macilaciGameModel();
        Fields = new Fields();
        ViewModel = new MainViewModel(GameModel, Fields, _fh);

        _appShell = new AppShell(_fh, GameModel, Fields, ViewModel)
        {
            BindingContext = ViewModel
		};
        MainPage = _appShell;
	}

    /*
    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);

        window.Created += (s, e) =>
        {
            // új játékot indítunk
            _sudokuGameModel.NewGame();
            _appShell.StartTimer();
        };

        window.Activated += (s, e) =>
        {
            if (!File.Exists(Path.Combine(FileSystem.AppDataDirectory, SuspendedGameSavePath)))
                return;

            Task.Run(async () =>
            {
                // betöltjük a felfüggesztett játékot, amennyiben van
                try
                {
                    await _sudokuGameModel.LoadGameAsync(SuspendedGameSavePath);

                    // csak akkor indul az időzítő, ha sikerült betölteni a játékot
                    _appShell.StartTimer();
                }
                catch
                {
                }
            });
        };

        window.Stopped += (s, e) =>
        {
            Task.Run(async () =>
            {
                try
                {
                    // elmentjük a jelenleg folyó játékot
                    _appShell.StopTimer();
                    await _sudokuGameModel.SaveGameAsync(SuspendedGameSavePath);
                }
                catch
                {
                }
            });
        };

        return window;
    }
    */
}
