using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Models;
using Dictionary.MVVM.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dictionary.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly AdminViewModel _userViewModel;

        private List<User> users = new List<User>();
        public override void Execute(object parameter)
        {
            if (users.Capacity == 0) ReadJsonFile();
            User user = new User(_userViewModel.Username, _userViewModel.Password);
            if (users.Contains(user))
            {
                App._user = user;
                Console.WriteLine("Login successful");
                _userViewModel.NavigateToAdminControl.Execute(null);
            }
            else
            {
                Console.WriteLine("Login failed");
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_userViewModel.Username) &&
                   !string.IsNullOrEmpty(_userViewModel.Password) &&
                   base.CanExecute(parameter);
        }

        public LoginCommand(AdminViewModel userViewModel)
        {
            _userViewModel = userViewModel;
            _userViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }


        public void ReadJsonFile()
        {
            string filePath = @"Resources\users.json";
            if (File.Exists(filePath))
            {
                users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(filePath));
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AdminViewModel.Username) ||
                              e.PropertyName == nameof(AdminViewModel.Password))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
