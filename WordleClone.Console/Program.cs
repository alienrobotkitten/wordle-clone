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
        string[] words = { "WRUNG", "QUOTE", "FLING", "CHAIN", "BADGE",
            "BARGE", "QUITE", "BLAST","KNIFE", "CREPE",
            "KNACK","HORSE","APPLE","FRUIT","DRUID",
            "KNOCK","CRAMP","NUDGE","ABORT","CLAMP",
            "KNOLL","CROPS","FLANK","CRASH","CRASS" };
        string realAnswer = GetRandomWord(words);
        bool win = false;

        for (int i = 0; i < 6; i++)
        {
            Console.Write("\n> ");
            string answer = Console.ReadLine();
            answer = answer.Trim().ToUpper();

            if (answer.Length == 5)
            {
                Console.Write($"\t{i + 1}: ");

                for (int j = 0; j < 5; j++)
                {
                    char letter = answer[j];

                    if (letter == realAnswer[j])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (realAnswer.Contains(answer[j]))
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

            if (answer == realAnswer)
            {
                win = true;
                Console.WriteLine($"\nYou found the right word in {i + 1} guesses!");
                break;
            }
        }
        Console.WriteLine(win ? "Congratulations!" : $"\nYou lost! The correct word was {realAnswer}.");

    }

    private static string GetRandomWord(string[] words)
    {
        return words[Random.Shared.Next(0, words.Length - 1)];
    }
}