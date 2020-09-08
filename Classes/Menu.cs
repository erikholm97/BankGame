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
        public static void ShowATMMenu()
        {
            Console.WriteLine("1) Insert money");
            Console.WriteLine("2) Withdraw money");
            Console.WriteLine("3) Show balance");
            Console.WriteLine("4) Exit");
            Console.SetCursorPosition(0, 8);
            Console.Write("Please choose a number from above: ");
        }
        
        public static void CardAnimation(int amount, string animationType)
        {

            string atmInsert = "|ATM    Output|";
            string card =       "|100          | ";
            string cardSpaceing = "|             | ";
            int counter = 0;
            double amountOfBills = amount / 100;
            
            Console.WriteLine("Opening atm...");

            if (animationType == "Insert")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

                while (counter < amountOfBills)
                {
                    //Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(card);
                    Console.WriteLine(cardSpaceing);
                    Console.WriteLine(" _____________ ");
                    counter++;
                }
                Console.ResetColor();

                Thread.Sleep(1000);
                Console.Clear();
        }
        public static void LoadingText(string text)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(text);
                text += '.';

                Thread.Sleep(100);
            }
        }
    }
}