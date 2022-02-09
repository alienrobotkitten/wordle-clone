namespace WordleCloneConsole;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(".---------.");
        Console.WriteLine("| WORDLE! |");
        Console.WriteLine("| (clone) |");
        Console.WriteLine("|_________|");
        Console.WriteLine();
        Console.WriteLine("Guess what FIVE LETTER WORD I'm thinking about! You have six guesses.\nWhen you press enter, I will color the letters.");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Yellow");
        Console.ResetColor();
        Console.WriteLine(" means that the letter is in the word, but currently in the wrong place.");
        Console.Write("A ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("green");
        Console.ResetColor();
        Console.WriteLine(" letter is in the correct place.");
        Console.WriteLine("Black letters are not in the word at all.");
        Console.WriteLine("Let's begin! Write your guess below:\n");

        bool quit = false;
        while (!quit)
        {
            Run();
            Console.WriteLine("Press any key to play again or Ctrl+C to exit.\n");
            Console.ReadKey();
        }
    }

    private static void Run()
    {
        string[] words = GetWordList();
        string correctWord = GetRandomWord(words).Trim().ToUpper();
        bool win = false;

        for (int i = 1; i <= 6; i++)
        {
            Console.Write("\n> ");
            string answer = Console.ReadLine().Trim().ToUpper();

            if (answer.Length == 5 && words.Contains(answer))
            {
                Console.Write($"\t{i}: ");

                for (int j = 0; j < 5; j++)
                {
                    char letter = answer[j];

                    if (letter == correctWord[j])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (correctWord.Contains(answer[j]))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                     }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.Write(letter);
                    Console.ResetColor();
                }
            }
            else if (answer.Length != 5)
            {
                Console.Write($"\tNot a valid answer: {answer.Length} characters.");
                i--;
            }
            else if (!words.Contains<string>(answer))
            {
                Console.Write("\tNot in word list.");
                i--;
            }
            else
            {
                Console.Write("\tSomething went wrong. Try again.");
                i--;
            }

            if (answer == correctWord)
            {
                win = true;
                Console.WriteLine($"\nYou found the right word in {i} guesses!");
                break;
            }
        }
        Console.WriteLine(win ? "Congratulations!" : $"\nYou lost! The correct word was {correctWord}.");

    }

    private static string GetRandomWord(string[] words)
    {
        return words[Random.Shared.Next(0, words.Length - 1)];
    }

    /// <summary>
    /// Reads word list from file.
    /// </summary>
    /// <returns>string[] wordlist</returns>
    private static string[] GetWordList()
    {
        string[] wordlist = File.ReadAllLines("../../../10000commonestonlyfiveletters.txt");
        return wordlist;
    }

    /// <summary>
    /// Word list must be plain text and contain only one word per row
    /// </summary>
    /// <param name="filepath">The path to the file. Prefix filepath with "../../../" if your file is in the same dir as Program.cs</param>
    /// <returns>string[] wordlist</returns>
    /// 
    private static string[] GetOnlyFiveLetterWordsFromFile(string filepath)
    {
        List<string> result = new List<string>();

        string[] words = File.ReadAllLines(filepath);

        foreach (string word in words)
        {            
            if (word.Trim().Length == 5 && !word.Contains<char>(' '))
                result.Add(word);
        }

        /* 
        //Debug code
        Console.WriteLine(result.Count.ToString());
        for (int i = 0; i < 10; i++)
            Console.WriteLine(result.ElementAt<string>(Random.Shared.Next(0, result.Count)));

        File.WriteAllLines("../../../10000commonestonlyfiveletters.txt", result);
        */

        return result.ToArray();
    }
}