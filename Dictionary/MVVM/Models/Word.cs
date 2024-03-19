using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dictionary.MVVM.Models
{
    public enum Category
    {
        All,
        Noun,
        Verb,
        Adjective,
        Adverb,
        Pronoun,
        Interjection,
    }
    public class Word
    {
        private string _wordName { get; set; }

        public string WordName
        {
            get => _wordName;
            set
            {
                _wordName = value;
            }
        }
        private string _wordMeaning { get; set; }
        public string WordMeaning
        {
            get => _wordMeaning;
            set
            {
                _wordMeaning = value;
            }
        }
        private Category _category { get; set; }
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
            }
        }
        private string _imagePath { get; set; }
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
            }
        }

        [JsonConstructor]
        public Word(string wordName, string wordMeaning, string imagePath, string category)
        {
            if (string.IsNullOrEmpty(wordName))
            {
                throw new ArgumentException($"'{nameof(wordName)}' cannot be null or empty.", nameof(wordName));
            }

            if (string.IsNullOrEmpty(wordMeaning))
            {
                throw new ArgumentException($"'{nameof(wordMeaning)}' cannot be null or empty.", nameof(wordMeaning));
            }

            _wordName = wordName;
            _wordMeaning = wordMeaning;
            _imagePath = imagePath;
            _category = (Category)Enum.Parse(typeof(Category), category);
        }
        public override string ToString()
        {
            return _wordName;
        }
        static public bool ContainsOnlyLetters(string input)
        {
            // Regular expression pattern to match only letters
            Regex regex = new Regex("^[a-zA-Z]+$");

            // Match the pattern against the input string
            return regex.IsMatch(input);
        }
    }
}
