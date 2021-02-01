using System;
using System.Collections.Generic;
using System.ServiceModel;
using BankAppDataTier;

//Curtin ID: 20204646

namespace BankBizTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class BankAppBizImpl : IBankAppBiz
    {
        IBankApp bank_data;

        public BankAppBizImpl()
        {
            Console.WriteLine("User has connected!");

            try
            {

                ChannelFactory<IBankApp> BankChannelFactory;
                NetTcpBinding tcpBinding = new NetTcpBinding();

                BankChannelFactory = new ChannelFactory<IBankApp>(tcpBinding, "net.tcp://localhost:50001/BankAppData");
                bank_data = BankChannelFactory.CreateChannel();


            }
            catch (FaultException exception)
            {
                Console.WriteLine("Connection faiure! Cannot connect to DataTier" + exception);
            }
        }


        //Funtions are called from BankAppDataTier

        //User Access

        public uint CreateUser()
        {
            return bank_data.CreateUser();
        }

        public void GetUserName(out string fname, out string lname)
        {
            bank_data.GetUserName(out fname, out lname);
        }

        public List<uint> GetUsers()
        {
            return bank_data.GetUsers();
        }

        public void SelectUser(uint userID)
        {
            bank_data.SelectUser(userID);
        }

        public void SetUserName(string fname, string lname)
        {
            bank_data.SetUserName(fname, lname);
        }

        //Account Access

        public uint CreateAccount(uint userID)
        {
            return bank_data.CreateAccount(userID);
        }

        public void Deposit(uint amount)
        {
            bank_data.Deposit(amount);
        }

        public List<uint> GetAccountIDsByUser(uint userID)
        {
            return bank_data.GetAccountIDsByUser(userID);
        }

        public uint GetBalance()
        {
            return bank_data.GetBalance();
        }

        public uint GetOwner()
        {
            return bank_data.GetOwner();
        }

        public void SelectAccount(uint accountID)
        {
            bank_data.SelectAccount(accountID);
        }

        public void Withdraw(uint amount)
        {
            bank_data.Withdraw(amount);
        }

        //Transaction Access

        public uint CreateTransaction()
        {
            //Save Transation Details When Transaction created
            bank_data.SaveToDisk();
            return bank_data.CreateTransaction();
            
        }

        public uint GetAmount()
        {
            return bank_data.GetAmount();
        }

        public uint GetRecvrAcct()
        {
            return bank_data.GetRecvrAcct();
        }

        public uint GetSendrAcct()
        {
            return bank_data.GetSendrAcct();
        }

        public List<uint> GetTransactions()
        {
            //Process all pending transactions
            bank_data.ProcessAllTransactions();
            return bank_data.GetTransactions();
        }

        public void SelectTransaction(uint TransactionID)
        {
            bank_data.SelectTransaction(TransactionID);
        }

        public void SetAmount(uint amount)
        {
            bank_data.SetAmount(amount);
        }

        public void SetRecvr(uint acctID)
        {
            bank_data.SetRecvr(acctID);
        }

        public void SetSendr(uint acctID)
        {
            bank_data.SetSendr(acctID);
        }

        
        //Searching Transaction Per Account
        public List<uint> FilterTransactions(uint accountNo)
        {
            //Creating a new List for Transactions by Account ID
            List<uint> userTransactions = new List<uint>();

            //Going through all transactions
            foreach(uint i in bank_data.GetTransactions())
            {
                bank_data.SelectTransaction(i);

                //Getting Senders Account No.
                uint senderAccountNo = bank_data.GetSendrAcct();

                //Checking whether the account no equals to client selected Account No.
                if (accountNo.Equals(senderAccountNo))
                {
                    //If it eqauls Transation added to the list
                    userTransactions.Add(i);
                }
            }
            //Return the list
            return userTransactions;
        }
    }
}
