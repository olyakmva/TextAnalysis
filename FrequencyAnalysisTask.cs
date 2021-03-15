using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var workDictionary = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (var i = 0; i < sentence.Count - 1; i ++)
                {
                    if (workDictionary.ContainsKey(sentence[i]))
                    {
                        if (workDictionary[sentence[i]].ContainsKey(sentence[i + 1]))
                        {
                            workDictionary[sentence[i]][sentence[i + 1]]++;
                        }
                        else
                        {
                            workDictionary[sentence[i]].Add(sentence[i+1],1);
                        }
                    }
                    else
                    {
                        var dict = new Dictionary<string, int> {{sentence[i + 1], 1}};
                        workDictionary.Add(sentence[i], dict); 
                    }
                }

                for (var i = 0; i < sentence.Count-2; i++)
                {
                    var key = sentence[i] + " " + sentence[i + 1];
                    if (workDictionary.ContainsKey(key))
                    {
                        if (workDictionary[key].ContainsKey(sentence[i + 2]))
                        {
                            workDictionary[key][sentence[i + 2]]++;
                        }
                        else
                        {
                            workDictionary[key].Add(sentence[i + 2], 1);
                        }
                    }
                    else
                    {
                        var dict = new Dictionary<string, int> { { sentence[i + 2], 1 } };
                        workDictionary.Add(key, dict);
                    }
                }
            }

            foreach (var pair in workDictionary)
            {
                result.Add(pair.Key, pair.Value.Count == 1 ? pair.Value.Keys.First() : GetValue(pair.Value));
            }
            return result;
        }

        static string GetValue(Dictionary<string, int> dict)
        {
            var word = dict.Keys.First();
            var frec = dict[word];
            foreach (var pair in dict)
            {
                if (pair.Value > frec)
                {
                    word = pair.Key;
                    frec = pair.Value;
                }
                else if (frec == pair.Value && string.CompareOrdinal(pair.Key, word) < 0)
                {
                    word = pair.Key;
                }
            }
            return word;
        }
   }
}