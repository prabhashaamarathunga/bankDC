using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ServiceModel;
using BankBizTier;

namespace BankClientTier
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Window
    {
        IBankAppBiz bank_biz;
        private string selectedAccountNo;
        private string selectedUserID;
        private uint transactionID;

        //Getting UserID and AccountNo from Accounts Window
        public Transactions(string selectedAccountNo, string selectedUserID)
        {
            InitializeComponent();
            this.selectedAccountNo = selectedAccountNo;
            this.selectedUserID = selectedUserID;
            txtSender.Text = selectedAccountNo;

            ChannelFactory<IBankAppBiz> channelFactory;
            NetTcpBinding tcpBind = new NetTcpBinding();

            channelFactory = new ChannelFactory<IBankAppBiz>(tcpBind, "net.tcp://localhost:50002/BankAppBiz");
            bank_biz = channelFactory.CreateChannel();

            //View Accounts of Selected User
            List<uint> userAccounts = bank_biz.GetAccountIDsByUser(Convert.ToUInt32(selectedUserID));
            listUserAccounts.ItemsSource = userAccounts;

            //View Transactions of Selected User
            uint accountNo = Convert.ToUInt32(selectedAccountNo);
            List<uint> accountTransactions = bank_biz.FilterTransactions(accountNo);
            listTransactions.ItemsSource = accountTransactions;
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {
            Users newUser = new Users();
            newUser.Show();
            this.Close();
        }

        private void accounts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void transactions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnconnect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateTrans_Click(object sender, RoutedEventArgs e)
        {
            uint accountNo = Convert.ToUInt32(selectedAccountNo);

            if (txtReceiver.Text == "" || txtamount.Text == "")
            {
                MessageBox.Show("Enter Transaction Details");
            }
            else
            {
                uint destaccountNo = Convert.ToUInt32(txtReceiver.Text);
                uint amount = Convert.ToUInt32(txtamount.Text);

                bank_biz.SelectAccount(accountNo);
                uint current_balance = bank_biz.GetBalance();

                if (amount > current_balance)
                {
                    MessageBox.Show("Transaction Failed! Not Enough Funds.");
                }
                else
                {
                    bank_biz.Withdraw(amount);
                    bank_biz.SelectAccount(destaccountNo);
                    bank_biz.Deposit(amount);
                    bank_biz.CreateTransaction();
                    bank_biz.SetSendr(accountNo);
                    bank_biz.SetRecvr(destaccountNo);
                    bank_biz.SetAmount(amount);
                }
            }

            List<uint> accountTransactions = bank_biz.FilterTransactions(accountNo);
            listTransactions.ItemsSource = accountTransactions;

        }


        private void listTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            transactionID = Convert.ToUInt32(listTransactions.SelectedItem);
            bank_biz.SelectTransaction(transactionID);
            uint senderAccount = bank_biz.GetSendrAcct();
            uint receiverAccount = bank_biz.GetRecvrAcct();
            uint amount = bank_biz.GetAmount();

            txtSender.Text = senderAccount.ToString();
            txtReceiver.Text = receiverAccount.ToString();
            txtamount.Text = amount.ToString();
            txtTransactionID.Text = transactionID.ToString();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //Going Back to Accounts Window
            string backUserID = selectedUserID;
            Accounts newAccount = new Accounts(backUserID);
            newAccount.Show();
            this.Close();
        }

        private void listUserAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            uint accountNo = Convert.ToUInt32(listUserAccounts.SelectedItem);
            txtReceiver.Text = accountNo.ToString();
        }

    }
}