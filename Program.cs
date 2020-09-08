
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

            Console.WriteLine("Would you like to continue? Press enter or ESC to exit"); // text ändrad
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                StartCashMachine();
            }
            Console.WriteLine("_Good bye.");

        }

        public static void DrawOptionMenu() // meny ändrad 
        {
            Menu ShowMenu = new Menu();
            BankAccount account = new BankAccount(); //Todo fixa så att account amount ändras om man går tillbaka och gör en withdraw efter man lagt in pengar så att amount blir rätt.
            account.Amount = 4000;

            bool listRunning = true;
            while (listRunning)
            {

                string option;
                ShowMenu.ShowMenu();

                option = Console.ReadLine();

                switch (option)
                {

                    case "1":
                        InsertMoney(account);
                        Console.WriteLine("Press [ENTER] to go back to the menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "2":
                        WithdrawMoney(account);
                        Console.WriteLine("Press [ENTER] to go back to the menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "3":
                    default:
                        Console.WriteLine("You did not choose one of the options above.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
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
            Thread.Sleep(1000);
            Console.Clear();
            DrawOptionMenu();
        }

        //Todo spara data i minnet så att det finns ett konto, blir mer logisk 
        private static void InsertMoney(BankAccount account)
        {
            Console.WriteLine("How much money would you like to insert?");
            int amountInsert = int.Parse(Console.ReadLine());

            bool isMaximum = (amountInsert >= 5000) ? true : false;

            if (isMaximum)
            {
                Console.WriteLine("The amount is to large please insert an lesser amount!");
                amountInsert = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(amountInsert + "kr will soon be added to your account, please wait!");

            Menu.InsertCardAnimation(amountInsert);

            Console.WriteLine("Recently added: " + amountInsert + "kr");
            Console.WriteLine("Your account balance before yor newest transaction: " + account.Amount);
            int total = (amountInsert + account.Amount); // enkel matte
            Console.WriteLine("Total balance: " + total);
        }
        private static void WithdrawMoney(BankAccount account)
        {
            
            Console.WriteLine("How much money would you like to withdraw?");
            int amountWithdraw = int.Parse(Console.ReadLine());

            Console.WriteLine(amountWithdraw + "kr will soon be taken out from your account, please wait!");
            Thread.Sleep(3000);
            Console.WriteLine("Recently withdrawed: " + amountWithdraw + "kr");
            Console.WriteLine("Your account balance before yor newest withdraw: " + account.Amount);
            int total = (account.Amount - amountWithdraw); // enkel matte

            Console.WriteLine("Total balance: " + total);
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
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(text);
                text += '.';

                Thread.Sleep(100);
            }
        }
    }
}