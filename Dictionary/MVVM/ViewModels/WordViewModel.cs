using Dictionary.Commands;
using Dictionary.Models;
using Dictionary.MVVM.Models;
using Dictionary.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    public class WordViewModel : ViewModelBase
    {
        private static ObservableCollection<Word> _words;

        private Word _word;

        public Word Word
        {
            get
            {
                if (_searchText == null)
                {
                    return Words.FirstOrDefault(w => w.WordName == _searchText);
                }
                return _word;
            }

            set
            {
                _word = value;
                OnPropertyChanged(nameof(Word));
                OnPropertyChanged(nameof(SelectedWordName)); // Update the SelectedWordName when Word changes
                OnPropertyChanged(nameof(SelectedWordMeaning)); // Update the SelectedWordName when Word changes
                OnPropertyChanged(nameof(SelectedWordImagePath)); // Update the SelectedWordName when Word changes
            }
        }

        public string SelectedWordName
        {
            get
            {
                if (_word != null)
                {
                    return _word.WordName;
                }
                return null;
            }
        }

        public string SelectedWordMeaning
        {
            get
            {
                if (_word != null)
                {
                    return _word.WordMeaning;
                }
                return null;
            }
        }
        public string SelectedWordImagePath
        {
            get
            {
                if (_word != null)
                {
                    if (_word.ImagePath != null)
                    {
                        return Word.ImagePath;
                    }
                    else return "/Dictionary;component/Resources/NotAImage.png";
                }
                return null;
            }
        }


        public ObservableCollection<Word> Words
        {
            get { return _words; }
            set
            {
                _words = value;
                OnPropertyChanged(nameof(Words));
            }
        }

        public ObservableCollection<Word> FilteredWords
        {

            get
            {
                if (string.IsNullOrEmpty(_searchText))
                {
                    return _words;
                }
                else
                {
                    return new ObservableCollection<Word>(_words.Where(x => x.WordName.ToLower().Contains(_searchText.ToLower())));
                }
            }
        }

        public ICommand NavigateToAdmin { get; }

        public WordViewModel(Navigation navigateCommand, Func<AdminViewModel> createAdminViewModel)
        {
            if (_words == null)
            {
                _words = new ObservableCollection<Word>();
                ReadWordJson();
            }
            NavigateToAdmin = new NavigateCommand(navigateCommand, createAdminViewModel);

        }

        public void ReadWordJson()
        {
            string filePath = @"Resources\words.json";
            if (File.Exists(filePath))
            {
                _words = JsonConvert.DeserializeObject<ObservableCollection<Word>>(File.ReadAllText(filePath));
                Console.WriteLine("!!!!!!!!!!!!CITESC");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        public string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(FilteredWords)); // Update the FilteredWords when SearchText changes
                OnPropertyChanged(nameof(AllCategories)); // Update the FilteredWords when SearchText changes
            }
        }
        private ObservableCollection<Category> _allCategories;
        public ObservableCollection<Category> AllCategories
        {
            get
            {
                _allCategories = new ObservableCollection<Category>();
                if(_searchText==null || _searchText=="")
                {
                    _allCategories.Add(Category.All);
                }
                foreach (Word word in FilteredWords)
                {
                    if (!_allCategories.Contains(word.GetCategory))
                        _allCategories.Add(word.GetCategory);
                }
                return _allCategories;
            }




        }
    }
}


