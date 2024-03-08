using Dictionary.Commands;
using Dictionary.Models;
using Dictionary.MVVM.Models;
using Dictionary.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    public class WordViewModel : ViewModelBase
    {
        public static List<Word> _words;
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
            if(_words == null)
            {
                _words = new List<Word>();
                ReadWordJson();
            }
            NavigateToAdmin = new NavigateCommand(navigateCommand, createAdminViewModel);

        }

        public void ReadWordJson()
        {
            string filePath = @"Resources\words.json";
            if (File.Exists(filePath))
            {
                List<Word> deserializedWords = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText(filePath));
                _words.AddRange(deserializedWords);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }

    }
}
