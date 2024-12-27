using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICD.Helpers
{
    public static class StringExtension
    {
        public static string ToAllFirstLetterInUpper(this string str)
        {
            var array = str.Split(" ");

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == "" || array[i] == " " || listOfArticles_Prepositions().Contains(array[i])) continue;
                array[i] = array[i].ToFirstLetterUpper();
            }
            return string.Join(" ", array);
        }

        private static string ToFirstLetterUpper(this string str)
        {
            return str?.First().ToString().ToUpper() + str?.Substring(1).ToLower();
        }

        private static string[] listOfArticles_Prepositions()
        {
            return new[]
            {
                "in","on","to","of","and","or","for","a","an","is"
            };
        }
    }
}
