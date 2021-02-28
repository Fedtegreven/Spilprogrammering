using System;

namespace PlayingWithDataStructures
{
    class Program
    {
        static int[] values = new int[3];

        public static void Main(string[] args)
        {
            InsertValues();
            DisplayValues();

        }

        static void InsertValues()
        {
            //insert the values
            Console.WriteLine("Input 3 numbers into the array: ");
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("Element - {0}",i);
   
                   values[i] = Convert.ToInt32(Console.ReadLine());
            }

        }

        static void DisplayValues()
        {
            //display original array values
            Console.WriteLine("Enter 3 numbers: ");
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("Element - {0} : {1}" , i, values[i]);
            }
            //display the values in revers order
            Console.WriteLine("Numbers displayed in reverse order:");
            for (int i = values.Length -1; i >=0; i--)
            {
                Console.WriteLine(values[i]);
            }
        }
    }
}
