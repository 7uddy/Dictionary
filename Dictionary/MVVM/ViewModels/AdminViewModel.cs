using Dictionary.Commands;
using Dictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand LoginCommand { get; set; }
 //     public ICommand RegisterCommand { get; set; }
        
        public ICommand NavigateToDictionary { get; }
        public AdminViewModel(User user,Navigation navigateCommand,Func<WordViewModel> createWordViewModel)
        {
            LoginCommand = new LoginCommand(this);
            NavigateToDictionary = new NavigateCommand(navigateCommand,createWordViewModel);
        }
    }
}
