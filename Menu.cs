using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bank
{
    /// <summary>
    /// Help library to draw animations and display menu. 
    /// </summary>
    public class Menu
    {
        public void ShowMenu()
        {
            Console.WriteLine("1) Insert money");
            Console.WriteLine("2) Withdraw money");
            Console.WriteLine("3) Exit");
            Console.SetCursorPosition(0, 8);
            Console.Write("Please choose a number from above: ");
        }

        public static void InsertCardAnimation(int amount)
        {
            string atmInsert = "|             |";
            string card =       "|100          | ";
            string cardSpaceing = "|             | ";
            int counter = 0;
            double amountOfBills = amount / 100;
            
            Console.WriteLine("Opening atm...");
            
                Console.WriteLine(atmInsert);
                Console.WriteLine(" _____________ ");

                while (counter < amountOfBills)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(card);
                    Console.WriteLine(cardSpaceing);
                    Console.WriteLine(" _____________ ");
                    counter++;
                }
                Console.ResetColor();

                Thread.Sleep(1000);
                Console.Clear();
        }
    }
}