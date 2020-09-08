using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace Bank
{
    class BankAccount
    {
        private int BalanceAmount = 0;

        public int Amount
        {
            get { return BalanceAmount;}
            set { BalanceAmount = value; }
        }

        enum CardAction
        {
            Withdraw,
            Insert
        }
        public void WithdrawMoney(BankAccount account)
        {
            Console.WriteLine("How much money would you like to withdraw?");
            int amountWithdraw = int.Parse(Console.ReadLine());

            Console.WriteLine(amountWithdraw + "kr will soon be taken out from your account, please wait!");
            Thread.Sleep(3000);
            Menu.CardAnimation(amountWithdraw, "Withdraw");
            Console.WriteLine("Recently withdrawed: " + amountWithdraw + "kr");
            Console.WriteLine("Your account balance before yor newest withdraw: " + account.Amount);
            int total = (BalanceAmount -= amountWithdraw); 

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
            Thread.Sleep(2000);
            Console.Clear();
            WriteAmount();
            
           
        }

        public void WriteAmount()
        {
            Console.WriteLine($"Total balance is: {BalanceAmount} SEK");
        }
    }
}
