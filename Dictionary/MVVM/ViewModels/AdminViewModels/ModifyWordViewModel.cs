using Dictionary.Commands;
using Dictionary.MVVM.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels.AdminViewModels
{
    public class ModifyWordViewModel:ViewModelBase
    {
        private Word _word;

        public Word Word
        {
            get
            {
                if (_searchText == null)
                {
                    return WordViewModel.Words.FirstOrDefault(w => w.WordName == _searchText);
                }
                return _word;
            }

            set
            {
                _word = value;
                _newWordName = _word.WordName;
                _newWordMeaning = _word.WordMeaning;
                _selectedCategory = _word.Category;
                OnPropertyChanged(nameof(Word));
                OnPropertyChanged(nameof(NewWordName));
                OnPropertyChanged(nameof(NewWordMeaning));
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        private ObservableCollection<Word> _filteredWords;
        public ObservableCollection<Word> FilteredWords
        {

            get
            {
                if (string.IsNullOrEmpty(_searchText))
                {
                    _filteredWords = WordViewModel.Words;
                }
                else
                {
                    _filteredWords = new ObservableCollection<Word>(WordViewModel.Words.Where(w => w.WordName.ToLower().Contains(_searchText.ToLower())));
                }
                return _filteredWords;
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
                OnPropertyChanged(nameof(FilteredWords));
            }
        }
        public ICommand ModifyWordCommand { get; set; }
        public ICommand NavigateToAdminControl { get; }
        public ICommand NavigateToDictionary { get; }
        public ICommand NavigateToGame { get; }
        public ModifyWordViewModel(Navigation navigation, Func<AdminControlViewModel> createAdminControlViewModel, Func<WordViewModel> createWordViewModel, Func<GameViewModel> createGameViewModel)
        {
            ModifyWordCommand = new RelayCommand(ModifyWord);
            NavigateToAdminControl = new NavigateCommand(navigation, createAdminControlViewModel);
            NavigateToDictionary = new NavigateCommand(navigation, createWordViewModel);
            NavigateToGame = new NavigateCommand(navigation, createGameViewModel);
        }
        private void ModifyWord()
        {
            if (string.IsNullOrEmpty(_newWordName) || string.IsNullOrEmpty(_newWordMeaning) || _selectedCategory == null)
            {
                System.Windows.MessageBox.Show("Please fill all the fields");
                return;
            }
            Word.WordName = _newWordName;
            Word.WordMeaning = _newWordMeaning;
            Word.Category = _selectedCategory;
            NavigateToAdminControl.Execute(null);
        }
        private ObservableCollection<Category> _allCategories;
        public ObservableCollection<Category> AllCategories
        {
            get
            {
                _allCategories = new ObservableCollection<Category> {
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
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        private string _newWordName;
        public string NewWordName
        {
            get => _newWordName;
            set
            {
                _newWordName = value;
                OnPropertyChanged(nameof(NewWordName));
            }
        }
        private string _newWordMeaning;
        public string NewWordMeaning
        {
            get => _newWordMeaning;
            set
            {
                _newWordMeaning = value;
                OnPropertyChanged(nameof(NewWordMeaning));
            }
        }
    }

}
