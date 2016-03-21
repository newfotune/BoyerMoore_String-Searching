using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyer_Moore
{
    class Program
    {
        static Dictionary<Char, int> theTable = new Dictionary<Char, int>();
        static string word;
        static int counter;

        static void Main(string[] args)
        {
            Console.Write("Enter your search Word: ");
            word = Console.ReadLine();
            buildTable();
            //read my text file.
            System.IO.StreamReader reader = new System.IO.StreamReader("./testText.txt");
            string line;
              while ((line = reader.ReadLine()) != null)
              {
                 SearchLine(line);
              }
            Console.WriteLine(word + " appears " + counter + " times");
            Console.ReadKey();
        }

        /*
            
         */
        static void buildTable()
        {
            int length = word.Length;
           
            for (int i = 0; i < 256; i++) {
                theTable[(char)i] = length;
            }

            for (int i = 0; i < length - 1; i++)
            {
                theTable[word[i]] = length - (i + 1);
            }
        }
        /*
            Searches the line of text passed in.
        */
        static void SearchLine(string text)
        {
            //initialize variables
            int index = 0, wordIndex = word.Length-1, textIndex = wordIndex;
            
            while (textIndex < text.Length) {
                wordIndex = word.Length - 1;
                while (wordIndex >= 0 && textIndex < text.Length)
                {
                    if (word[wordIndex] == text[textIndex])
                    {
                        if (wordIndex == 0)
                        {
                            counter++;
                            textIndex = index + word.Length;
                        } else
                        {
                            wordIndex--; textIndex--;
                        }
                        index = textIndex;   
                    }
                    else
                    {
                        int value;
                        theTable.TryGetValue(text[textIndex], out value);

                        textIndex += value;

                        index = textIndex;
                    }
                }
            }
        }  
    }
}
