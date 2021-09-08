using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TextbasedRPG.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Clone all members from <paramref name="obj"/>
        /// into destination type of <typeparamref name="T"/>
        /// </summary>
        /// <remarks>
        /// This is designed for 2 different types containing the same name/types.
        /// A design model commonly seen on web apps where database models are passed to the frontend via intermediate objects
        /// This is essentially a copy / paste exercise
        /// </remarks>
        /// <param name="obj">Object containing data we want to extract</param>
        /// <typeparam name="T">Type of object we want to copy data to</typeparam>
        /// <returns>Hydrated instance of <typeparamref name="T"/></returns>
        public static T Clone<T>(this object obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();
            FieldInfo[] fields = obj.GetType().GetFields();
            
            T instance = Activator.CreateInstance<T>();

            foreach (PropertyInfo prop in props)
            {
                if (prop.SetMethod == null)
                    continue;
                
                object value = prop.GetValue(obj);
                prop.SetValue(instance, value);
            }

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(obj);
                field.SetValue(instance, value);
            }

            return instance;
        }
        
        /// <summary>
        /// Generates a text-based table that displays information from <paramref name="items"/>
        /// </summary>
        /// <param name="items">Will be utilized to generate data for each row</param>
        /// <param name="columnHeaders">Text that appears in the header</param>
        /// <param name="selectors">Methods used to extract / format data that will appear in each column</param>
        /// <typeparam name="T">Type of entity being used as data for our table</typeparam>
        /// <returns>Entire table as a string</returns>
        /// <exception cref="Exception">When length of <paramref name="columnHeaders"/> do not match length of <paramref name="selectors"/></exception>
        public static string ToTable<T>(this IEnumerable<T> items,
            string[] columnHeaders,
            params Func<T, object>[] selectors)
        {
            if (columnHeaders.Length != selectors.Length)
                throw new Exception("ToTable requires that column headers and selectors be the same length");

            var values = new string[items.Count() + 1, selectors.Length];
            
            // Create the headers
            for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
                values[0, colIndex] = columnHeaders[colIndex];
            
            // Fill the table with data
            for (int rowIndex = 1; rowIndex < values.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
                {
                    values[rowIndex, colIndex] = selectors[colIndex]
                        .Invoke(items.ElementAt(rowIndex-1))
                        .ToString();
                }
            }

            return ToStringTable(values);
        }

        /// <summary>
        /// Helper method for populating string table
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        static string ToStringTable(this string[,] values)
        {
            int[] maxColumnsWidth = GetMaxColumnWidth(values);
            var headerSplitter = new string('-', maxColumnsWidth
                .Sum(i => i + 3) - 1);

            var builder = new StringBuilder();

            for (int rowIndex = 0; rowIndex < values.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
                {
                    // Print Cell
                    string cell = values[rowIndex, colIndex];

                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    builder.Append(" | ");
                    builder.Append(cell);
                }

                builder.Append(" | ");
                builder.AppendLine();

                if (rowIndex == 0)
                {
                    builder.AppendFormat(" |{0}| ", headerSplitter);
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
        
        /// <summary>
        /// Determine that maximum length of text in each column. This will enable uniformity between rows
        /// </summary>
        /// <param name="values"></param>
        /// <returns>Array of column lengths</returns>
        static int[] GetMaxColumnWidth(string[,] values)
        {
            var maxColumnWidth = new int[values.GetLength(1)];

            for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < values.GetLength(0); rowIndex++)
                {
                    int newLength = values[rowIndex, colIndex].Length;
                    int oldLength = maxColumnWidth[colIndex];

                    if (newLength > oldLength)
                        maxColumnWidth[colIndex] = newLength;
                }
            }
            
            return maxColumnWidth;
        }
    }
}