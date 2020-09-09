using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

namespace Bank
{
    [DataContract(Name = "Customer", Namespace = "http://www.contoso.com")]
    class BankAccount 
    {
        [DataMember()]
        public string Password;

        [DataMember]
        public int BalanceAmount = 0;

        public BankAccount(string _password)
        {
            Password = _password;
        }

        public int Amount
        {
            get { return BalanceAmount;}
            set { BalanceAmount = value; }
        }

        public void WithdrawMoney(BankAccount account)
        {
            Console.WriteLine("How much money would you like to withdraw?");
            int amountWithdraw = int.Parse(Console.ReadLine());

            Console.WriteLine(amountWithdraw + "kr will soon be taken out from your account, please wait!");
            Thread.Sleep(4000);
            Menu.CardAnimation(amountWithdraw, "Withdraw");
            Console.WriteLine("Recently withdrawed: " + amountWithdraw + "kr");
            Console.WriteLine("Your account balance before yor newest withdraw: " + account.Amount);
            int total = (BalanceAmount -= amountWithdraw);
            WriteAmount(account);
        }
        public void InsertMoney(BankAccount account)
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
            int total = (BalanceAmount += amountInsert);
            
            Menu.CardAnimation(amountInsert, "Insert");

            Console.WriteLine("Recently added: " + amountInsert + "kr");
            Console.WriteLine("Your account balance before yor newest transaction: " + account.Amount);
            Thread.Sleep(3000);
            Console.Clear();
            WriteAmount(account);
            
           
        }
        public void WriteAmount(BankAccount account)
        {
            string path = "BankAccount.xml";

            FileStream writer = new FileStream(path, FileMode.Create);
            DataContractSerializer ser =
                new DataContractSerializer(typeof(BankAccount));
            ser.WriteObject(writer, account);
            writer.Close();

            Console.WriteLine($"Total balance is: {BalanceAmount} SEK");
        }

        //Todo Göra två konton, överför pengar mellan dessa
        public void TransferMoney(BankAccount account) 
        {
            Console.WriteLine("Enter the account number you would like to transfer money to: ");
            double accountNr = int.Parse(Console.ReadLine());

            Console.WriteLine("How much money would you like to transfer?");
            int transferredAmount = int.Parse(Console.ReadLine());

            Console.WriteLine("Are you sure that you would like to transfer " + transferredAmount + "kr to the following account number " + accountNr + "? (y/n)" );
            string answer = Console.ReadLine();
            int TotalBalanceAfterTrans =  BalanceAmount - transferredAmount;
            if (answer == "y")
            {
                Console.WriteLine(transferredAmount + "kr has been transfered to the following acount: " + accountNr);
                Console.WriteLine("Total balance: " + TotalBalanceAfterTrans);
            }

            else if (answer == "n")
            {
                Console.WriteLine("Transaction has been canceled.");
                Console.WriteLine("Total balance: " + BalanceAmount);
            }


        }
    }
}
