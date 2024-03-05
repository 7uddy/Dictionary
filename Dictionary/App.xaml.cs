using Dictionary.Models;
using Dictionary.MVVM.ViewModels;
using Dictionary.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Navigation _navigation = new Navigation();
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = new WordViewModel(_navigation);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();
            base.OnStartup(e);
        
        }
    }
}
