using Dictionary.Models;
using Dictionary.MVVM.ViewModels;
using Dictionary.MVVM.ViewModels.AdminViewModels;
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
        public static User _user;
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = CreateWordViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();
            base.OnStartup(e);
        
        }
        private AdminViewModel CreateAdminViewModel()
        {
            return new AdminViewModel(_user, _navigation, CreateWordViewModel,CreateGameViewModel,CreateAdminControlViewModel);
        }

        private WordViewModel CreateWordViewModel()
        {
            return new WordViewModel(_navigation,CreateAdminViewModel,CreateGameViewModel);
        }
        private GameViewModel CreateGameViewModel()
        {
            return new GameViewModel(_navigation,CreateAdminViewModel, CreateWordViewModel);
        }
        private AdminControlViewModel CreateAdminControlViewModel()
        {
            return new AdminControlViewModel(_navigation, CreateAdminViewModel, CreateWordViewModel, CreateGameViewModel,
                CreateAddWordViewModel, CreateModifyWordViewModel,CreateDeleteWordViewModel);
        }
        private AddWordViewModel CreateAddWordViewModel()
        {
            return new AddWordViewModel(_navigation, CreateAdminControlViewModel, CreateWordViewModel, CreateGameViewModel);
        }
        private ModifyWordViewModel CreateModifyWordViewModel()
        {
            return new ModifyWordViewModel();
        }
        private DeleteWordViewModel CreateDeleteWordViewModel()
        {
            return new DeleteWordViewModel(_navigation, CreateAdminControlViewModel, CreateWordViewModel, CreateGameViewModel);
        }
    }
}
