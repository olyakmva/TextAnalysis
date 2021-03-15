using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = text.ToLower().Split(new[] {'.', '!', '?', ';', ':', '(', ')'},
                StringSplitOptions.RemoveEmptyEntries);
            if (sentences.Length == 0 && text.Length != 0)
            {
                var words = GetWords(text);
                if(words!=null)
                    sentencesList.Add(words);
            }
            else
                sentencesList.AddRange(sentences.Select(sentence => GetWords(sentence)).Where(words => words != null));

            return sentencesList;
        }

        private static List<string> GetWords(string sentence)
        {
            var words = new List<string>();
            var sb = new StringBuilder();
            foreach (var c in sentence)
            {
                if (char.IsLetter(c) || c == '\'')
                {
                    sb.Append(c);
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        words.Add(sb.ToString());
                        sb.Clear();
                    }
                }
            }
            if(sb.Length > 0)
                words.Add(sb.ToString());
            return words.Count>0 ? words:null;
        }
    }
}