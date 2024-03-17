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
                OnPropertyChanged(nameof(Word));
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
                _word = null;
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
            NavigateToAdminControl.Execute(null);
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
    }

}
