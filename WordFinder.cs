using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace CountdownWordFinderBlazor
{
    public class WordFinder
    {
        public static Dictionary<string, List<string>> LetterDictionary { get; set; }
        public string[] LetterArray { get; set; } = new string[9];
        public List<Word> WordList { get; set; }
        public bool SearchComplete { get; set; }
        public bool ArrayFilled { get; private set; } = true;
        private static readonly Random rand = new Random();

        private static IEnumerable<IEnumerable<T>> CombinationFinder<T>(IEnumerable<T> items, int count)
        {
            // Returns a list of all possible combinations of the items passed through.
            // The combinations will be 'count' long i.e if 2 is passed through, all 
            // the combinations returned will have two characters in them.

            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in CombinationFinder(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }

                i++;
            }
        }

        private void SaveRandomWord(string orderedLetters)
        {
            // A string of orderedLetters found in LetterDictionary is passed in and
            // used to select a random word from the List returned by the dictionary
            List<string> words = LetterDictionary[orderedLetters];

            int wordIndex = rand.Next(words.Count);
            string word = words[wordIndex];

            WordList.Add(new Word(word.ToUpper()));
            
        }


        public void FindWords()
        {
            SearchComplete = false;

            WordList = new List<Word>();


            // We're only interested in the 5-9 letter characters because anything else isn't really worth the time.
            for (int i = 5; i <= 9; i++)
            {
                //Console.WriteLine("{0} Letter Words: ", i);

                List<IEnumerable<string>> result = CombinationFinder(LetterArray, i).ToList();
                string orderedLetters = "";

                int randomCombinationIndex = rand.Next(result.Count());

                IEnumerable<string> combination = result.ElementAt(randomCombinationIndex);

                orderedLetters = string.Join("", combination.ToArray());
                orderedLetters = string.Concat(orderedLetters.OrderBy(c => c));

                while (!LetterDictionary.ContainsKey(orderedLetters))
                {
                    // It's possible that there aren't any possible combinations for a given
                    // length, so move on to the next length
                    if (result.Count == 1)
                    {
                        orderedLetters = "";
                        break;
                    }

                    result.RemoveAt(randomCombinationIndex);

                    randomCombinationIndex = rand.Next(result.Count());

                    combination = result.ElementAt(randomCombinationIndex);

                    orderedLetters = string.Join("", combination.ToArray());
                    orderedLetters = string.Concat(orderedLetters.OrderBy(c => c));
                }

                if(orderedLetters != "")
                    SaveRandomWord(orderedLetters);
            }

            SearchComplete = true;

        }

        public void Search()
        {
            if (LetterArrayFilled())
            {
                ArrayFilled = true;
                FindWords();
            }
            else
                ArrayFilled = false;
        }

        public void ResetState()
        {
            LetterArray = new string[9];
            WordList = null;
            ArrayFilled = true;
            SearchComplete = false;
        }

        
        public bool LetterArrayFilled()
        {
            foreach(var letter in LetterArray)
            {
                if (string.IsNullOrWhiteSpace(letter))
                    return false;
            }

            return true;
        }

    }
}
