
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
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

        public static void OptionMenu() // meny ändrad 
        {
            BankAccount account = new BankAccount("1234"); //Todo fixa så att account amount ändras om man går tillbaka och gör en withdraw efter man lagt in pengar så att amount blir rätt.
            account.Amount = 4000;

            string dialoge = "Press [ENTER] to go back to the menu";

            bool isDone = false;
            while (!isDone)
            {

                string option;
                Menu.ShowATMMenu(); 

                option = Console.ReadLine();

                switch (option)
                {

                    case "1":
                        account.InsertMoney(account);
                        Console.WriteLine(dialoge);
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "2":
                        account.WithdrawMoney(account);
                        Console.WriteLine(dialoge);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine(dialoge);
                        account.WriteAmount();
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("You did not choose one of the options above.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quit program?");
                Console.ResetColor();
                Console.ReadLine();
            }
        }

        public static void StartCashMachine()
        {
            Menu.LoadingText("Starting bank");

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

            Menu.LoadingText("Loading card options.");
            Console.Clear();
            LogIn();
        }

        public static void LogIn()
        {
            Console.WriteLine("Type in your password.");
            Console.WriteLine("If you are new user press enter.");
            string input = Console.ReadLine();

            if (input == string.Empty)
            {
                CreateNewUser();
            }

            bool loginSucess;
            do
            {
                loginSucess = Authenticate(ref input);

            } while (!loginSucess);

            Console.WriteLine("You are logged in!");
            Thread.Sleep(1000);
            Console.Clear();
            OptionMenu();
        }


        //Todo Den här xml kan användas för att hålla koll på bank kontot. Finns på Bank\Bank\bin\Debug\netcoreapp3.1\BankAccount.xml. 
        private static void CreateNewUser()
        {
            Console.WriteLine(
                "Creating a bankaccount and serializing it.");

            string path = "BankAccount.xml";

            BankAccount account = new BankAccount("1234");

            FileStream writer = new FileStream(path, FileMode.Create);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(BankAccount));
            ser.WriteObject(writer, account);
            writer.Close();

        }

        //Todo spara data i minnet så att det finns ett konto, blir mer logisk 
        public static bool Authenticate(ref string input)
        {
            //Todo kolla i xml filen om användare existerar.
            //tips på hur man läser xml https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.datamemberattribute?view=netcore-3.1
            bool passwordSuccess = (input != string.Empty) ? true : false;

            if (!passwordSuccess)
            {
                Console.WriteLine("Wrong password.");
                input = Console.ReadLine();
            }
            return passwordSuccess;
        }
    }
}