using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DailyProgrammer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thue-Morse Sequence
            //Console.WriteLine(ThueMorseSequence(6));

            // Shakespeares
            RememberYourLines("give thee");
        }

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
    }
}
