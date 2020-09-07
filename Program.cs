using System;
using System.Collections.Generic;
using System.Threading;

namespace Bank
{
    class Program
    {
        private static string bankName = "ZoidCoin Bank";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Hello welcome to {bankName}.");
            Console.ResetColor();

            Console.WriteLine("Would you like to continue? Press enter or ESC to discontinue");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                StartCashMachine();
            }
            Console.WriteLine("_Good bye.");

        }

        public static void StartCashMachine()
        {
            LoadingText("Starting bank");

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Insert card: (press space)");

            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                InsertCard();
            }
        }

        public static void InsertCard()
        {
            Console.WriteLine("Card inserted.");
            Console.ResetColor();

            LoadingText("Loading card options.");

            LogIn();
        }

        public static void LogIn()
        {
            Console.WriteLine("Type in your password.");
            string input = Console.ReadLine();

            bool loginSucess;
            do
            {
                loginSucess = Authenticate(ref input);

            } while (!loginSucess);

            Console.WriteLine("You are logged in!");
            Thread.Sleep(2000);
            Console.Clear();
            DrawOptionMenu();
        }
        public static void DrawOptionMenu()
        {
            var options = new Dictionary<string, int>()
            {
                {"Insert", 1},
                {"Withdraw", 2},
                {"Exit", 3}
                
            };

            Console.WriteLine("Option:");
            foreach (KeyValuePair<string, int> pair in options)
            {
                Console.WriteLine($"{pair.Value}: {pair.Key}");
            }
        }
        public static bool Authenticate(ref string input)
        {
            //Todo check if user exist.
            bool passwordSuccess = (input != string.Empty) ? true : false;

            if (!passwordSuccess)
            {
                Console.WriteLine("Wrong password.");
                input = Console.ReadLine();
            }
            return passwordSuccess;
        }

        public static void LoadingText(string text)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(text);
                text += '.';

                Thread.Sleep(200);
            }
        }
    }
}
