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
                OnPropertyChanged(nameof(SelectedWordName)); 
                OnPropertyChanged(nameof(SelectedWordMeaning)); 
                OnPropertyChanged(nameof(SelectedWordImagePath)); 
                OnPropertyChanged(nameof(SelectedCategory)); 
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


        public static ObservableCollection<Word> Words
        {
            get { return _words; }
            set
            {
                _words = value;
            }
        }
        private ObservableCollection<Word> _filteredWords;
        public ObservableCollection<Word> FilteredWords
        {

            get
            {
                if (string.IsNullOrEmpty(_searchText))
                {
                    if(_selectedCategory==Category.All)_filteredWords= _words;
                    else
                    {
                        _filteredWords = new ObservableCollection<Word>(_words.Where(x => x.GetCategory == _selectedCategory));
                    }
                }
                else
                {
                    if (_selectedCategory == Category.All)
                    {
                        _filteredWords = new ObservableCollection<Word>(_words.Where(x => x.WordName.ToLower().Contains(_searchText.ToLower())));
                    }
                    else
                    {
                        _filteredWords = new ObservableCollection<Word>(_words.Where(x => x.WordName.ToLower().Contains(_searchText.ToLower()) && x.GetCategory == _selectedCategory));
                    }
                }
                return _filteredWords;
            }
        }

        public ICommand NavigateToAdmin { get; }
        public ICommand NavigateToGame { get; }

        public WordViewModel(Navigation navigateCommand, Func<AdminViewModel> createAdminViewModel,Func<GameViewModel> createGameViewModel)
        {
            if (_words == null)
            {
                _words = new ObservableCollection<Word>();
                ReadWordJson();
            }
            NavigateToAdmin = new NavigateCommand(navigateCommand, createAdminViewModel);
            NavigateToGame = new NavigateCommand(navigateCommand, createGameViewModel);

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
                _word = null;
                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(FilteredWords)); 
            }
        }
        private ObservableCollection<Category> _allCategories;
        public ObservableCollection<Category> AllCategories
        {
            get
            {
                _allCategories = new ObservableCollection<Category> { 
                Category.All,
                Category.Noun,
                Category.Verb,
                Category.Adjective,
                Category.Adverb,
                Category.Pronoun,
                Category.Interjection
                };
                
                return _allCategories;
            }
        }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get
            {
                if(_word==null)
                {
                    return _selectedCategory;
                }
                else
                {
                    return _word.GetCategory;
                }
            }
            set
            {
                _selectedCategory = value;
                _word = null;
                OnPropertyChanged(nameof(SelectedCategory));
                OnPropertyChanged(nameof(FilteredWords));  
            }
        }
    }
}


