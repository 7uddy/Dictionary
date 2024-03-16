using Dictionary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    public class GameViewModel :ViewModelBase
    {
    public ICommand NavigateToAdmin { get; }
    public ICommand NavigateToDictionary { get; }

        public GameViewModel(Navigation navigation, Func<AdminViewModel> createAdminViewModel, Func<WordViewModel> createWordViewModel)
        {
        NavigateToAdmin = new NavigateCommand(navigation, createAdminViewModel);
        NavigateToDictionary = new NavigateCommand(navigation, createWordViewModel);
        }
    }
}
