using System;

namespace Guesser
{
    class Program
    {
        string[] names = new string[10];
        static void Main(string[] args)
        {
            bool running = true;
            int tries = 0;
            int triesLeft = 10;
            string correctGuess = setName();


            while (running)
            {
                Console.WriteLine("Guess a Name");
                Console.WriteLine("Write you guess and press enter");
                Console.WriteLine("You have: "+ (triesLeft - tries) + " tries left \n");

                string guess = Console.ReadLine();

                if (correctGuess.Equals(guess, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Congratulations you guessed correctly on your " + (tries+1) + ". try!!!:D");  
                    running = false;
                }
             
                else
                {
                    tries++;
                    Console.WriteLine("Sorry wrong guess... :( \nPlease try again\n");
                }

                if (tries == triesLeft)
                {
                    Console.WriteLine("You have used up all your tries.... \nThe right name was: "+ correctGuess +" better luck next time :(");
                    running = false;
                }
                
                hint(tries,correctGuess);
            }
        }

        static string setName()
        {
            Random random = new Random();

            string correctName;
            string[] names = {"Bob", "Henry", "Anne", "Anna", "Eric", "Marco", "Sofie", "Jakob", "John", "Sally"};
            return correctName = names[random.Next(0,names.Length)];
        }

        static void hint(int attempt, string correct)
        {
            if (attempt == 5)
            {
                string hint = correct.Substring(0, 1);
                Console.WriteLine("Here is a hint, the first letter is: " + hint + "\n");
            }
        }
    }
}
