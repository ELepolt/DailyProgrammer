using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DailyProgrammer
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // Thue-Morse Sequence
            //Console.WriteLine(ThueMorseSequence(6));

            // Shakespeares
            //RememberYourLines("give thee");

            // Friendly Dates
            var friendlyDate = FriendlyDates(new DateTime(2022, 09, 05), new DateTime(2023, 09, 04));
            Console.WriteLine(friendlyDate);
        }

        #region ThueMorseSequence
        /// <summary>
        /// http://www.reddit.com/r/dailyprogrammer/comments/2cld8m/8042014_challenge_174_easy_thuemorse_sequences/
        /// Takes the degree and outputs the Thue-Morse Sequence to that degree
        /// </summary>
        /// <param name="degree">
        /// The degree of Thue-Morse Sequence to end at
        /// </param>
        /// <returns></returns>
        static string ThueMorseSequence(int degree)
        {
            string sequence = "0";
            string tempSequence = "";
            int sequenceInt = 0;

            for (int i = 0; i < degree; i++)
            {
                foreach (char letter in sequence)
                {
                    Int32.TryParse(letter.ToString(), out sequenceInt);
                    tempSequence += sequenceInt == 1 ? "0" : "1";
                }

                Console.WriteLine(sequence);
                Console.WriteLine(tempSequence);

                sequence += tempSequence;
            }

            return sequence;
        }
        #endregion

        #region RememberYourLines
        /// <summary>
        /// http://www.reddit.com/r/dailyprogrammer/comments/2xoxum/20150302_challenge_204_easy_remembering_your_lines/
        /// Given a line from Shakespeare's Macbeth, print out the entirety of the passage that it belongs to
        /// </summary>
        /// <param name="line">
        /// The line from the play
        /// </param>
        /// <returns></returns>
        static void RememberYourLines(string forgottenLine)
        {
            // passage will hold each line as it comes in, and will refresh when
            // it switches to a different character
            var passages = new Dictionary<string,List<string>>();
            var passage = new List<string>();
            // Just for fun to know who said what
            var speakingCharacter = "";
            // flag to know when to break and stop reading through the play
            var passageContainsForgottenLine = false;
            
            // Get the file from the interwebz
            string line;
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://gist.githubusercontent.com/Quackmatic/f8deb2b64dd07ea0985d/raw/macbeth.txt");

            using (var reader = new StreamReader(stream))
            {
                // Loop through the file.
                while ((line = reader.ReadLine()) != null)
                {
                    // If the first four characters are blank than it is part of a dialogue
                    // Therefore add the line to the variable passage
                    if (line.Length > 3 && line.Substring(0, 4) == "    ")
                    {
                        // Add the line to the passage.
                        passage.Add(line);
                        if (line.ToLower().Contains(forgottenLine.ToLower()))
                        {
                            passageContainsForgottenLine = true;
                        }
                    }
                    // If the first two characters are blank, then it is the start of a new
                    // character speaking. Clear the passage, and make note of the new character.
                    else if (line.Length > 1 && line.Substring(0, 2) == "  ")
                    {
                        speakingCharacter = line.Replace(".", "");
                        passage = new List<string>();
                    }
                    // If the line is empty it's a new character speaking, or a new act.
                    // If the passage contains the line, break out of the loop
                    else if (line == "")
                    {
                        if (passageContainsForgottenLine)
                        {
                            passages.Add(speakingCharacter, passage);
                            passageContainsForgottenLine = false;
                        }
                        else
                        {
                            speakingCharacter = "";
                        }
                    }
                }
            }

            if (passages.Count() > 0)
            {
                // Print the passage
                foreach (var passageSection in passages)
                {
                    // passageSection.Key is the speaking character
                    // passageSection.Value is the list of lines in the passage
                    Console.WriteLine(passageSection.Key);
                    foreach (var passageLine in passageSection.Value)
                    {
                        Console.WriteLine(passageLine);
                    }
                    Console.WriteLine("---------------------------");
                }
            }
            else
            {
                Console.WriteLine("The line you have searched for was not found in the given text");
            }
            // Suspend the screen.
            Console.ReadLine();
        }
        #endregion

        /// <summary>
        /// http://www.reddit.com/r/dailyprogrammer/comments/2ygsxs/20150309_challenge_205_easy_friendly_date_ranges/
        /// Given two dates, print a friendly version of the range
        /// </summary>
        /// <example>
        /// 2015-07-01 2015-07-04 returns July 1st - 4th
        /// </example>
        public static string FriendlyDates(DateTime startDate, DateTime endDate)
        {
            string returnString = string.Concat(GetMonth(startDate), " ", OrdinalSuffix(startDate));
            
            // difference == number of days of difference
            var difference = endDate - startDate;
            
            // begin a slew of multi else if chain to compare the difference in dates
            // if the dates are the same, return the date as a readable format with year
            if (startDate == endDate)
            {
                returnString += string.Concat(", ", startDate.Year);
            }
            // if the difference in days is <= 31 and the start/end months match, just concat with the same month
            else if (difference.Days <= 30 && startDate.Month == endDate.Month)
            {
                returnString += string.Concat(" - ", OrdinalSuffix(endDate));
            }
            // if the difference is <= 364
            else if (difference.Days <= 364)
            {
                // If the years match, just return the months with the year attached at the end
                if (startDate.Year == endDate.Year)
                {
                    returnString += string.Concat(" - ", GetMonth(endDate), " ", OrdinalSuffix(endDate), ", ", endDate.Year);
                }
                // else if the months match but it has reached this line than it's had a change of year, so show the year on both
                else if (startDate.Month == endDate.Month)
                {
                    returnString += string.Concat(", ", startDate.Year, " - ",GetMonth(endDate), " ", OrdinalSuffix(endDate), ", ", endDate.Year);
                }
                // else it's been less than a year
                // I don't necessarily agree with this one: December 1st - February 3rd
                else
                {
                    returnString += string.Concat(" - ", GetMonth(endDate), " ", OrdinalSuffix(endDate));
                }
            }
            // else it's been more than a year, so show both months and years for each date 
            else
            {
                returnString += string.Concat(", ", startDate.Year, " - ", GetMonth(endDate), " ", OrdinalSuffix(endDate), ", ", endDate.Year);
            }

            return returnString;
        }

        static string GetMonth(DateTime dateTime)
        {
            return dateTime.ToString("MMMM");
        }
        /// <summary>
        /// Returns the ordinal suffix for the day of the month represented by this instance
        /// </summary>
        /// <returns></returns>
        static string OrdinalSuffix(DateTime datetime)
        {
            int day = datetime.Day;

            if (day % 100 >= 11 && day % 100 <= 13)
                return String.Concat(day, "th");

            switch (day % 10)
            {
                case 1:
                    return String.Concat(day, "st");
                case 2:
                    return String.Concat(day, "nd");
                case 3:
                    return String.Concat(day, "rd");
                default:
                    return String.Concat(day, "th");
            }
        }
    }
}
