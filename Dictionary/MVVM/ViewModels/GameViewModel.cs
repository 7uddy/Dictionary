using Dictionary.Commands;
using Dictionary.MVVM.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dictionary.MVVM.ViewModels
{
    public class GameViewModel :ViewModelBase
    {
        public ICommand NavigateToAdmin { get; }
        public ICommand NavigateToDictionary { get; }

        public GameViewModel(Navigation navigation, Func<AdminViewModel> createAdminViewModel, Func<WordViewModel> createWordViewModel)
        {
        NavigateToAdmin = new NavigateCommand(navigation, createAdminViewModel);
        NavigateToDictionary = new NavigateCommand(navigation, createWordViewModel);
        _indexShown = 0;
        GetRandomWords();
        _guessedWords = new List<Word>();
        }

        private int _indexShown;
        public int IndexShown
        {
            get
            {
                return _indexShown;
            }
            set
            {
                _indexShown = value;
                OnPropertyChanged(nameof(IndexShown));
            }
        }
        private List<bool> randomImage;
        private void GetRandomWords()
        {
            Random random = new Random();
            List<int> randomNumbers = new List<int>();
            List<Word> randomWords = new List<Word>();
            randomImage=new List<bool>();
            for (int i = 0; i < 5; i++)
            {
                int randomNumber = random.Next(0, WordViewModel.Words.Count);
                if(randomNumber%2==0)
                {
                    randomImage.Add(true);
                }
                else
                {
                    randomImage.Add(false);
                }
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers.Add(randomNumber);
                    randomWords.Add(WordViewModel.Words[randomNumber]);
                }
                else
                {
                    i--;
                }
            }
            WordsToBeGuessed = randomWords;
        }

        private List<Word> _wordsToBeGuessed;
        public List<Word> WordsToBeGuessed
        {
            get
            {
                return _wordsToBeGuessed;
            }
            set
            {
                _wordsToBeGuessed = value;
                OnPropertyChanged(nameof(WordsToBeGuessed));
            }
        }
        public string Meaning
        {
            get { if (_indexShown != 5) return GetMeaning();
                else return "Game finished! You were right with "+ Score() + " words out of "+ _indexShown+".";
            }
        }

        public string GetMeaning()
        {
            return WordsToBeGuessed[IndexShown].WordMeaning;
        }
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
                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(FilteredWords));
            }
        }
        private List<Word> _guessedWords;
        public List<Word> GuessedWords
        {
            get
            {
                return _guessedWords;
            }
            set
            {
                _guessedWords = value;
                OnPropertyChanged(nameof(GuessedWords));
            }
        }

        private RelayCommand nextWord;

        public ICommand NextWord
        {
            get
            {
                if (nextWord == null)
                {
                    nextWord = new RelayCommand(PerformNextWord);
                }

                return nextWord;
            }
        }

        private void PerformNextWord()
        {
            if (_indexShown < 5)
            {
                _indexShown++;
                if (_guessedWords.Count == _indexShown - 1)
                {
                    _guessedWords.Add(_word);
                    _searchText = null;
                    _word = null;
                }
                else
                {
                    if (_indexShown < _guessedWords.Count && _guessedWords[_indexShown] != null)
                    {
                        _searchText = _guessedWords[_indexShown].WordName;
                        _word = _guessedWords[_indexShown];
                    }
                    else
                    {
                        _guessedWords.Add(_word);
                        _searchText = null;
                        _word = null;
                    }
                }
            }
            OnPropertyChanged(nameof(Meaning));
            OnPropertyChanged(nameof(Word));
            OnPropertyChanged(nameof(FilteredWords));
            OnPropertyChanged(nameof(GuessedWords));
            OnPropertyChanged(nameof(IsButtonEnabled));
            OnPropertyChanged(nameof(NextOrFinish));
            OnPropertyChanged(nameof(IsScoreViewOff));
            OnPropertyChanged(nameof(ImagePath));
            OnPropertyChanged(nameof(IsImageShown));
        }

        private RelayCommand previousWord;

        public ICommand PreviousWord
        {
            get
            {
                if (previousWord == null)
                {
                    previousWord = new RelayCommand(PerformPreviousWord);
                }
                return previousWord;
            }
        }

        private void PerformPreviousWord()
        {
            if (_indexShown > 0 && _guessedWords[--_indexShown]!=null)
            {
                _searchText = _guessedWords[_indexShown].WordName;
                _word = _guessedWords[_indexShown];
            }
            else
            {
                _searchText = null;
                _word = null;
            }
            OnPropertyChanged(nameof(Meaning));
            OnPropertyChanged(nameof(Word));
            OnPropertyChanged(nameof(FilteredWords));
            OnPropertyChanged(nameof(GuessedWords));
            OnPropertyChanged(nameof(IsButtonEnabled));
            OnPropertyChanged(nameof(NextOrFinish));
            OnPropertyChanged(nameof(ImagePath));
        }
        private string _nextOrFinish;

        public string NextOrFinish
        {
            get
            {
                if (_indexShown < 4)
                {
                    _nextOrFinish = "Next";
                }
                else
                {
                    _nextOrFinish = "Finish";
                }
                return _nextOrFinish;
            }
        }
        private bool _isButtonEnabled;

        public bool IsButtonEnabled
        {
            get { if (_indexShown != 0) _isButtonEnabled = true;
                  else _isButtonEnabled = false;
                  return _isButtonEnabled;
                }
        }

        private int Score()
        {
            int score = 0;
            for(int i= 0;i <=4;i ++)
            {
                if (_wordsToBeGuessed[i] == _guessedWords[i])
                {
                    score++;
                }
            }
            return score;

        }

        public System.Windows.Visibility IsScoreViewOff
        {
            get
            {
                if (_indexShown == 5)
                {
                    return System.Windows.Visibility.Hidden;
                }
                else
                {
                    return System.Windows.Visibility.Visible;
                }
            }
        }
        
        private string _imagePath;
        public string ImagePath
        {
            get
            {
                if (_indexShown != 5 && randomImage[_indexShown]==true)
                {
                    if (WordsToBeGuessed[IndexShown].ImagePath != null)
                    {
                        _imagePath = WordsToBeGuessed[IndexShown].ImagePath;
                    }
                    else
                    {
                        _imagePath = null;
                    }
                    OnPropertyChanged(nameof(IsImageShown));
                    return _imagePath;
                }
                else
                {
                    OnPropertyChanged(nameof(IsImageShown));
                    return _imagePath=null;
                }
            }
        }
        public System.Windows.Visibility IsImageShown
        {
            get
            {
                if(_imagePath!=null && _indexShown<5) return System.Windows.Visibility.Hidden;
                else return System.Windows.Visibility.Visible;
            }
        }
    }
}
