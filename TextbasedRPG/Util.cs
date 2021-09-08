using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TextbasedRPG
{
    /// <summary>
    /// Provides basic utility functions utilized by our game
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Load a collection of <typeparam name="T"/> from json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> LoadFromFile<T>()
        {
            string filename = $"{typeof(T).Name}s.json";

            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filename));
        }

        /// <summary>
        /// Save collection of <typeparamref name="T"/> as JSON
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        public static void SaveFile<T>(IEnumerable<T> collection)
        {
            string filename = $"{typeof(T).Name}s.json";

            File.WriteAllText(filename, JsonConvert.SerializeObject(collection, Formatting.Indented));
        }
        
        /// <summary>
        /// Will continuously ask user for valid input until it is given
        /// </summary>
        /// <param name="message">Prompt the user must answer</param>
        /// <param name="errorMessage">Custom message to display if invalid input is given</param>
        /// <typeparam name="T">Desired type of data wanted from user</typeparam>
        /// <returns>Value from user as <typeparamref name="T"/></returns>
        public static T GetUserInput<T>(string message, string errorMessage = null)
        {
            do
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                
                try
                {
                    return (T) Convert.ChangeType(input, typeof(T));
                }
                catch
                {
                    Console.WriteLine(errorMessage ?? $"Expected a value of type '{typeof(T).Name}'");
                }
            } while (true);
        }

        /// <summary>
        /// Extends functionality of <see cref="GetUserInput{T}(string,string)"/> <br/>
        /// Except the user's value must pass the test given by <paramref name="criteria"/>.<br/>
        /// If user fails expected input type, or fails <paramref name="criteria"/> -- they'll continuously be prompted
        /// </summary>
        /// <param name="message">Prompt to give user</param>
        /// <param name="criteria">Method containing condition(s) that the user's input must meet</param>
        /// <param name="errorMessage">Custom error message to display (optional) when user fails</param>
        /// <typeparam name="T">Desired type of data wanted from user</typeparam>
        /// <returns>Value from user as <typeparamref name="T"/></returns>
        public static T GetUserInput<T>(string message, Predicate<T> criteria, string errorMessage = null)
        {
            T value = GetUserInput<T>(message, errorMessage);

            while (!criteria(value))
            {
                if(errorMessage != null)
                    Console.WriteLine(errorMessage);
                
                value = GetUserInput<T>(message, errorMessage);
            }

            return value;
        }

        /// <summary>
        /// Extends functionality of <see cref="GetUserInput{T}(string,string)"/> <br/>
        /// Numeric Based <br/>
        /// User must provide a value within <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        /// <param name="message">Prompt for user to answer</param>
        /// <param name="min">Minimum value allowed (inclusive)</param>
        /// <param name="max">Maximum value allowed (inclusive)</param>
        /// <param name="errorMessage">Custom error message to display when user fails</param>
        /// <typeparam name="T">Desired type of data wanted from user</typeparam>
        /// <returns>Value from user as <typeparamref name="T"/></returns>
        public static T GetUserInput<T>(string message, T min, T max, string errorMessage = null)
            where T : IComparable, IConvertible, IFormattable
        {
            T value = GetUserInput<T>(message, errorMessage);

            while (Comparer.Default.Compare(value, min) < 0 || Comparer.Default.Compare(value, max) > 0)
            {
                Console.WriteLine($"Invalid input. You chose: '{value}'. Must be in range of {min} - {max}");
                return GetUserInput(message, min, max, errorMessage);
            }
            
            return value;
        }
    }
}