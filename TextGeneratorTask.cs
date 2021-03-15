using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var list = new List<string>();
            var begins = phraseBeginning.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (begins.Length == 0)
            {
                list.Add(phraseBeginning);
            }
            else list.AddRange(begins);

            while (wordsCount > 0)
            {
                if (list.Count>1)
                {
                    var key = list[list.Count - 2] + " " + list[list.Count-1];
                    if (nextWords.ContainsKey(key))
                    {
                        list.Add(nextWords[key]);
                        wordsCount--;
                    }
                    else
                    {
                        key = list[list.Count - 1];
                        if (nextWords.ContainsKey(key))
                        {
                            list.Add(nextWords[key]);
                            wordsCount--;
                        }
                        else break;
                    }
                }
                else
                {
                    var key2 = list[list.Count - 1];
                    if (nextWords.ContainsKey(key2))
                    {
                        list.Add(nextWords[key2]);
                        wordsCount--;
                    }
                    else break;
                }
            }

            var sb = new StringBuilder();
            foreach (var word in list)
            {
                sb.Append(word + " ");
            }
            return sb.ToString().TrimEnd();
        }
    }
}