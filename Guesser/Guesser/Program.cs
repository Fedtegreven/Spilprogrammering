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
                //call the hint method 
                hint(tries, correctGuess);
                //mesages printed in the beginning
                Console.WriteLine("Guess a Name");
                Console.WriteLine("Write you guess and press enter");
                Console.WriteLine("You have: "+ (triesLeft - tries) + " tries left");
                Console.WriteLine("If you want to quit press Q\n");

                string guess = Console.ReadLine();
                //if the user wants to quit early, disregard upper and lower cases
                if (guess.Equals("q", StringComparison.OrdinalIgnoreCase)) Environment.Exit(0);
                //increment the tries variable with one for every loop
                tries++;
                //if the guess is correct print a message, disregard upper and lower cases in the guess
                if (correctGuess.Equals(guess, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Congratulations you guessed correctly on your " + (tries+1) + ". try!!!:D");  
                    running = false;
                }
                //not the right guess
                else
                {  
                    Console.WriteLine("Sorry wrong guess... :( \nPlease try again\n");
                }
                // after all tries is used up, exit the loop and print a message
                if (tries == triesLeft)
                {
                    Console.WriteLine("You have used up all your tries.... \nThe right name was: "+ correctGuess +", better luck next time :(");
                    running = false;
                }
            }
        }

        //set a random name to guess from an array
        static string setName()
        {
            Random random = new Random();

            string correctName;
            string[] names = {"Bob", "Henry", "Anne", "Anna", "Eric", "Marco", "Sofie", "Jakob", "John", "Sally"};
            return correctName = names[random.Next(0,names.Length)];
        }

        //give a hint (first letter in the name) after 5 guesses
        static void hint(int attempt, string correct)
        {
            if (attempt >= 5)
            {
                string hint = correct.Substring(0, 1);
                Console.WriteLine("Hint!!! The first letter is: ".ToUpper() + hint);
            }
        }
    }
}
