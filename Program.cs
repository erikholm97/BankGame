
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

        public static void OptionMenu(BankAccount account) // meny ändrad 
        {
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
                        account.WriteAmount(account);
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
                input = CreateNewUser();
            }

            
            BankAccount bankAccount = Authenticate(ref input);
            
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("You are logged in!");

            Thread.Sleep(1000);

            Console.Clear();

            Console.ResetColor();

            OptionMenu(bankAccount);
            
        }


        //Todo Den här xml kan användas för att hålla koll på bank kontot. Finns på Bank\Bank\bin\Debug\netcoreapp3.1\BankAccount.xml. 
        private static string CreateNewUser()
        {
            Console.WriteLine(
                "Creating a bankaccount and serializing it.");

            string path = "BankAccount.xml";

            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine("Enter password");

            string password = Console.ReadLine();
            BankAccount account = new BankAccount(password);

            FileStream writer = new FileStream(path, FileMode.Create);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(BankAccount));
            ser.WriteObject(writer, account);
            writer.Close();

            return password;

        }

        //Todo spara data i minnet så att det finns ett konto, blir mer logisk 
        public static BankAccount Authenticate(ref string input)
        {
            string path = "BankAccount.xml";

            FileStream fs = new FileStream(path,
                FileMode.OpenOrCreate);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(BankAccount));
            // Deserialize the data and read it from the instance.
            BankAccount account = (BankAccount)ser.ReadObject(fs);
            fs.Close();

            bool passwordSuccess = false;

            passwordSuccess = (input == account.Password) ? true : false;

            while (!passwordSuccess)
            {
                Console.Clear();
                Console.WriteLine("Wrong password try again.");
                passwordSuccess = (input == Console.ReadLine()) ? true : false;
            }

            return account;
        }
    }
}