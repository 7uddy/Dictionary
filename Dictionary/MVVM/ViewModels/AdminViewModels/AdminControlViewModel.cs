using Dictionary.Commands;
using Dictionary.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    public class AdminControlViewModel:ViewModelBase
    {
        public ICommand NavigateToAdmin { get; }
        public ICommand NavigateToDictionary { get; }
        public ICommand NavigateToGame { get; }
        public ICommand NavigateToAddWord { get; }
        public ICommand NavigateToModifyWord { get; }
        public ICommand NavigateToDeleteWord { get; }

        public AdminControlViewModel(Navigation navigation, Func<AdminViewModel> createAdminViewModel, Func<WordViewModel> createWordViewModel,
            Func<GameViewModel> createGameViewModel,Func<AddWordViewModel>createAddWordViewModel,Func<ModifyWordViewModel> createModifyWordViewModel,
            Func<DeleteWordViewModel> createDeleteWordViewModel)
        {
            NavigateToAdmin = new NavigateCommand(navigation, createAdminViewModel);
            NavigateToDictionary = new NavigateCommand(navigation, createWordViewModel);
            NavigateToGame = new NavigateCommand(navigation, createGameViewModel);
            NavigateToAddWord = new NavigateCommand(navigation, createAddWordViewModel);
            NavigateToModifyWord = new NavigateCommand(navigation, createModifyWordViewModel);
            NavigateToDeleteWord = new NavigateCommand(navigation, createDeleteWordViewModel);
        }

       

    }
}
