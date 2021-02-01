using System;
using System.Collections.Generic;
using System.ServiceModel;

//Curtin ID: 20204646



namespace BankAppDataTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class BankAppImpl : IBankApp
    {
        //Creating the static object from BankDB.dll
        public static BankDB.BankDB mybank = new BankDB.BankDB();

        //Accessing UserAccessInterface on BankDB.dll
        BankDB.UserAccessInterface IUserAccess = mybank.GetUserAccess();

        //Accessing AccountAccessInterface on BankDB.dll
        BankDB.AccountAccessInterface IAccountAccess = mybank.GetAccountInterface();

        //Accessing TransactionAccessInterface on BankDB.dll
        BankDB.TransactionAccessInterface ITransactionAccess = mybank.GetTransactionInterface();

        public BankAppImpl()
        {
        }

        //Funtions are called from BankDB.dll

        //BankDB
        public void SaveToDisk()
        {
            mybank.SaveToDisk();
        }

        public void ProcessAllTransactions()
        {
            mybank.ProcessAllTransactions();
        }

        //User Access

        public uint CreateUser()
        {
            Console.WriteLine("New Bank User Created");
            return IUserAccess.CreateUser();
        }

        public void GetUserName(out string fname, out string lname)
        {
            IUserAccess.GetUserName(out fname, out lname);
        }

        public List<uint> GetUsers()
        {
            return IUserAccess.GetUsers();
        }

        public void SelectUser(uint userID)
        {
            IUserAccess.SelectUser(userID);
        }

        public void SetUserName(string fname, string lname)
        {
            IUserAccess.SetUserName(fname, lname);
        }

        //Account Access

        public uint CreateAccount(uint userID)
        {
            Console.WriteLine("New Bank Account Created");
            return IAccountAccess.CreateAccount(userID);
        }

        public void Deposit(uint amount)
        {
            IAccountAccess.Deposit(amount);
        }

        public List<uint> GetAccountIDsByUser(uint userID)
        {
            return IAccountAccess.GetAccountIDsByUser(userID);
        }

        public uint GetBalance()
        {
            return IAccountAccess.GetBalance();
        }

        public uint GetOwner()
        {
            return IAccountAccess.GetOwner();
        }

        public void SelectAccount(uint accountID)
        {
            IAccountAccess.SelectAccount(accountID);
        }

        public void Withdraw(uint amount)
        {
            IAccountAccess.Withdraw(amount);
        }

        //Transaction Access

        public uint CreateTransaction()
        {
            Console.WriteLine("New Transaction Done");
            return ITransactionAccess.CreateTransaction();
        }

        public uint GetAmount()
        {
            return ITransactionAccess.GetAmount();
        }

        public uint GetRecvrAcct()
        {
            return ITransactionAccess.GetRecvrAcct();
        }

        public uint GetSendrAcct()
        {
            return ITransactionAccess.GetSendrAcct();
        }

        public List<uint> GetTransactions()
        {
            return ITransactionAccess.GetTransactions();
        }

        public void SelectTransaction(uint TransactionID)
        {
            ITransactionAccess.SelectTransaction(TransactionID);
        }

        public void SetAmount(uint amount)
        {
            ITransactionAccess.SetAmount(amount);
        }

        public void SetRecvr(uint acctID)
        {
            ITransactionAccess.SetRecvr(acctID);
        }

        public void SetSendr(uint acctID)
        {
            ITransactionAccess.SetSendr(acctID);
        }




    }
}
