using Dictionary.Commands;
using Dictionary.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace Dictionary.MVVM.ViewModels.AdminViewModels
{
    public class AddWordViewModel : ViewModelBase
    {
        private Word _newWord;
        public Word NewWord
        {
            get
            {
                return _newWord;
            }
        }

        private string _addWordName;
        public string AddWordName
        {
            get { return _addWordName; }
            set
            {
                _addWordName = value;
                OnPropertyChanged(nameof(AddWordName));
            }
        }

        private string _addWordMeaning;
        public string AddWordMeaning
        {
            get { return _addWordMeaning; }
            set
            {
                _addWordMeaning = value;
                OnPropertyChanged(nameof(AddWordMeaning));
            }
        }

        public ICommand AddImage { get; }
        public ICommand CreateWord { get; }
        public ICommand NavigateToAdminControl { get; }
        public ICommand NavigateToDictionary { get; }
        public ICommand NavigateToGame { get; }
        public AddWordViewModel(Navigation navigation, Func<AdminControlViewModel> createAdminControlViewModel, Func<WordViewModel> createWordViewModel, Func<GameViewModel> createGameViewModel)
        {
            AddImage = new RelayCommand(ExecuteAddImage);
            CreateWord = new RelayCommand(SubmitWord);
            NavigateToAdminControl = new NavigateCommand(navigation, createAdminControlViewModel);
            NavigateToDictionary = new NavigateCommand(navigation, createWordViewModel);
            NavigateToGame = new NavigateCommand(navigation, createGameViewModel);
        }

        private void SubmitWord()
        {
           _newWord = new Word(_addWordName,_addWordMeaning,_selectedImagePath,_category);
            WordViewModel.Words.Add(_newWord);
            string json= Newtonsoft.Json.JsonConvert.SerializeObject(_newWord);
            File.AppendAllText("words.json",json);
            NavigateToAdminControl.Execute(null);
        }

        private string _selectedImagePath;
        public string SelectedImagePath
        {
            get { return _selectedImagePath; }
        }

        private void ExecuteAddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                string fileName = Path.GetFileName(selectedFileName);

                // Define the target path to the Resources folder of your project
                string targetPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources\");

                // If the Resources directory doesn't exist, create it
                if (!Directory.Exists(targetPath))
                {
                    throw new DirectoryNotFoundException("The Resources directory doesn't exist");
                }

                string destFile = Path.Combine(targetPath, fileName);

                // Copy the selected file to the target path
                File.Copy(selectedFileName, destFile, true);

                string relativeFilePath = $"/Dictionary;component/Resources/{fileName}";
                // Now, you can use relativeFilePath as needed
                _selectedImagePath=relativeFilePath;
            }
        }
        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        private ObservableCollection<Category> _allCategories;
        public ObservableCollection<Category> AllCategories
        {
            get
            {
                _allCategories = new ObservableCollection<Category> { 
                    Models.Category.All,
                    Models.Category.Noun,
                    Models.Category.Verb,
                    Models.Category.Adjective,
                    Models.Category.Adverb,
                    Models.Category.Pronoun,
                    Models.Category.Interjection
                };

                return _allCategories;
            }
        }
    }
}
