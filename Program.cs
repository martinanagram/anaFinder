using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace anaFinder {
    class Program {

        /// <summary>
        /// Console application that searches anagrams for a word from a specified dictionary
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {

            // Specify start time to calculate the execution time
            long startTime = (System.DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

            if (args.Length != 2) {
                Console.WriteLine("Usage: anaFinder [fullPathToDictionaryFile] [wordToFindAnagramsFor]");
                return;
            }

            string path = args[0];
            string word = args[1].ToLower();
            string[] anagrams;
            string result;

            // Check if dictionary file really exists
            if (!System.IO.File.Exists(path)) {
                Console.WriteLine("Dictionary file does not exist");
                return;
            }

            anagrams = getAnagramsFromFile(path, word);
            result = String.Join(",", anagrams);

            // Specify end time to calculate the execution time
            long stopTime = (System.DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startTime;

            // Show result
            Console.WriteLine(stopTime.ToString() + (String.IsNullOrEmpty(result) ? "" : ",") + result);
        }

        /// <summary>
        /// Method to find anagrams from specific dictionary file for a given word
        /// </summary>
        /// <param name="path">Path to dictionary file</param>
        /// <param name="word">Word to search anagrams for</param>
        /// <returns>Array of found anagrams</returns>
        static string[] getAnagramsFromFile(string path, string word) {
            List<string> foundWords = new List<string>();

            // Sort all characters in a word
            string sortedWord = String.Concat(word.OrderBy(c => c).ToArray());

            string s;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var reader = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("iso-8859-4"))) {
                while (!reader.EndOfStream) {
                    s = reader.ReadLine().ToLower();
                    // Let's first check if lengths match
                    if (s.Length == word.Length) {
                        // Sort characters in a found word
                        string sortedLine = String.Concat(s.OrderBy(c => c).ToArray());
                        // If it matches the source word, add it to found words list
                        if (sortedLine == sortedWord) {
                            foundWords.Add(s);
                        }
                    }
                }
            }

            // Remove the source word
            foundWords.Remove(word);

            // Return all unique found words
            return foundWords.Distinct().ToArray();
        }

    }
}
