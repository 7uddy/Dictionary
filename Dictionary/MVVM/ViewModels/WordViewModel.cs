using Dictionary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    internal class WordViewModel : ViewModelBase
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
        public ICommand Navigate { get; }

        public WordViewModel(Navigation navigateCommand)
        {
            Navigate = new NavigateCommand(navigateCommand);
        }

    }
}
