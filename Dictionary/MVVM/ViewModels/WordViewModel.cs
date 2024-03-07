using Dictionary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    public class WordViewModel : ViewModelBase
    {
        private string _wordName;
        private string _wordMeaning;


        public string WordName
        {
            get => _wordName;
            set
            {
                _wordName = value;
                OnPropertyChanged(nameof(WordName));
            }
        }

        public string WordMeaning
        {
            get => _wordMeaning;
            set
            {
                _wordMeaning = value;
                OnPropertyChanged(nameof(WordMeaning));
            }
        }
        public ICommand NavigateToAdmin { get; }

        public WordViewModel(Navigation navigateCommand,Func<AdminViewModel> createAdminViewModel)
        {
            NavigateToAdmin = new NavigateCommand(navigateCommand, createAdminViewModel);

        }

    }
}
