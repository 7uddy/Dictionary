using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.MVVM.Models
{
    internal class Word
    {
        private string WordName { get; set; }
        private string WordMeaning { get; set; }

        public Word(string wordName, string wordMeaning)
        {
            if (string.IsNullOrEmpty(wordName))
            {
                throw new ArgumentException($"'{nameof(wordName)}' cannot be null or empty.", nameof(wordName));
            }

            if (string.IsNullOrEmpty(wordMeaning))
            {
                throw new ArgumentException($"'{nameof(wordMeaning)}' cannot be null or empty.", nameof(wordMeaning));
            }

            WordName = wordName;
            WordMeaning = wordMeaning;
        }
    }
}
