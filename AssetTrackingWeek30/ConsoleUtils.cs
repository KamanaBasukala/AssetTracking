using System;
using System.Globalization;
using System.Text.Json;

namespace AssetTrackingWeek30
{   
    //This file contains common functions
    public static class ConsoleUtils
    {        
        public static void ClearScreen()
        {
            Console.Clear();
            PrintHeader();
        }

        public static void PrintHeader()
        {
            ConsoleStyler consoleStyler = new ConsoleStyler();
            consoleStyler.StyleText("Welcome to the weekly mini project, week30: Asset Tracking App.", TextType.PageHeader);
        }       

        public static int GetValidIntegerInput(string prompt)
        {
            int result;
            bool isValidInput = false;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                isValidInput = int.TryParse(input, out result);

                if (!isValidInput)
                {
                    ConsoleStyler consoleStyler = new ConsoleStyler();
                    consoleStyler.StyleText("Invalid input. Please enter a valid integer.",TextType.Error);
                }

            } while (!isValidInput);

            return result;
        }

        public static bool GetValidDecimal(string strDecimalValue,  out decimal decimalValue)
        {
            bool isValidDecimal = decimal.TryParse(strDecimalValue, out decimalValue);
            if (!isValidDecimal)
            {
                ConsoleStyler consoleStyler = new ConsoleStyler();
                consoleStyler.StyleText("Invalid input. Please enter a valid amount.", TextType.Error);
            }
            return isValidDecimal;
        }

        //Checks if entered value is empty or whitespace.
        public static bool IsNotEmpty(string text)
        {
            //Checking if input is empty
            if (string.IsNullOrEmpty(text.Trim() ))
            {
                ConsoleStyler consoleStyler = new ConsoleStyler();
                consoleStyler.StyleText("You may not enter an empty value.", TextType.Error);
                return false;
            }

            return true;
        }

        public static string CapitalizeFirstLetterOfWords1(string input)
        {
            string[] ignoreList = {"USA", "UK"};

            // Split the input string into words
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Capitalize the first letter of each word
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToUpper();//Converting to check for ignore cases.

                if (ignoreList != null && ignoreList.Contains(words[i].ToLower()))
                {
                    // If the word is in the ignore list, convert it to lowercase
                    words[i] = words[i].ToLower();
                }
                else
                {
                    // Capitalize the word if it's not in the ignore list
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }

            // Join the capitalized words back into a single string
            string result = string.Join(" ", words);

            // Handle special characters like apostrophes (optional)
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            result = textInfo.ToTitleCase(result);

            return result;
        }

        public static string CapitalizeFirstLetterOfWords(string input)
        {
            string[] ignoreList = { "USA", "UK" };

            // Split the input string into words
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Capitalize the first letter of each word
            for (int i = 0; i < words.Length; i++)
            {

                words[i] = words[i].ToUpper();//Converting to check for ignore cases.

                if (ignoreList != null && ignoreList.Contains(words[i].ToLower()))
                {

                    // Capitalize the word if it's not in the ignore list
                    words[i] = words[i].ToLower();
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }

            // Join the capitalized words back into a single string
            string result = string.Join(" ", words);

            // Handle special characters like apostrophes (optional)
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            result = textInfo.ToTitleCase(result);

            return result;
        }

        public static DateTime ConvertStringtoDate(string dateInString,string format = "MM/dd/yyyy")
        {
            if (DateTime.TryParseExact(dateInString, format, System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date; 
            }
            else
            {
                ConsoleStyler consoleStyler = new ConsoleStyler();
                consoleStyler.StyleText("Invalid date format.", TextType.Error);
            }

            return DateTime.Today;
        }

        public static bool IsDateValid(string input, string format, out DateTime formattedDate)
        {            
            if (DateTime.TryParse(input, out formattedDate))
            {
                // Parsing successful; the input matches the specified format
                return true;
            }

            return false;
        }

        public static int GetYearDifference(DateTime startDate, DateTime endDate)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            TimeSpan span = endDate - startDate;
            // We started at year 1 for the Gregorian
            // calendar, so we must subtract a year here.
            int years = (zeroTime + span).Year - 1;

            return years;
        }

        public static int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeDifference = endDate.Subtract(startDate);
            int monthsDifference = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;

            // If the end date's day is earlier than the start date's day,
            // and the end date's month is not the same as the start date's month,
            // we need to subtract 1 from the months difference to get the correct result.
            if (endDate.Day < startDate.Day && endDate.Month != startDate.Month)
            {
                monthsDifference--;
            }

            return monthsDifference;
        }
    }
}

