using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.MVVM.Models
{
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
        public Word(string wordName, string wordMeaning,string imagePath)
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
        }
        public override string ToString()
        {
            return _wordName;
        }

    }
}
