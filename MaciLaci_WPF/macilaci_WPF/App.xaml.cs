using MaciLaci.Model;
using MaciLaci.Persistence;
using macilaci_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace macilaci_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        macilaciViewModel _macilaciViewModel;
        macilaciGameModel mlModel;
        Fields gameFields;
        FileHandler fh;
        public App()
        {
            Startup += OnStartup;
        }

        private void OnStartup(object? sender, StartupEventArgs args)
        {
            FileReader fr = new FileReader("../../../maps/easyMap.txt", "../../../maps/mediumMap.txt", "../../../maps/hardMap.txt");
            fh = new FileHandler(fr);
            mlModel = new macilaciGameModel();
            gameFields = new Fields();
            fh.Load(ref mlModel,ref gameFields, Difficulty.EASY);
            _macilaciViewModel = new macilaciViewModel(mlModel, gameFields, fh);
            MainWindow window = new()
            {
                DataContext = _macilaciViewModel      //new macilaciViewModel(mlModel, gameFields, fh)
            };
            _macilaciViewModel.gameOver += onGameOver;
            window.Show();
        }


        private void onGameOver(object? sender, bool l)
        {
            if (l)
            {
                MessageBox.Show("BONFIRE LIT");
            }
            else
            {
                MessageBox.Show("YOU DIED");
            }
        }
    }
}

