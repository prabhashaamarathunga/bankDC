using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BankBizTier;
using System.ServiceModel;

//Curtin ID: 20204646

namespace BankClientTier
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Window
    {
        IBankAppBiz bank_biz;
        private string selectedUserID;
        private uint accountNo;

        //Getting UserID from Users Window
        public Accounts(string selectedUserID)
        {
            InitializeComponent();

            this.selectedUserID = selectedUserID;
            txtuserID.Text = selectedUserID;

            ChannelFactory<IBankAppBiz> channelFactory;
            NetTcpBinding tcpBind = new NetTcpBinding();

            channelFactory = new ChannelFactory<IBankAppBiz>(tcpBind, "net.tcp://localhost:50002/BankAppBiz");
            bank_biz = channelFactory.CreateChannel();

            //View Account List at the startup
            List<uint> accounts = bank_biz.GetAccountIDsByUser(Convert.ToUInt32(selectedUserID));
            listAccounts.ItemsSource = accounts;

        }

        private void users_Click(object sender, RoutedEventArgs e)
        {

        }

        private void transactions_Click(object sender, RoutedEventArgs e)
        {
        }

        private void accounts_Click(object sender, RoutedEventArgs e)
        {
        }

        private void txtuserID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            uint userID = Convert.ToUInt32(selectedUserID);
            bank_biz.CreateAccount(userID);
            MessageBox.Show("New Account Created Successfully");
            //Refresh the Account List with new Added Accounts
            List<uint> accounts = bank_biz.GetAccountIDsByUser(Convert.ToUInt32(userID));
            listAccounts.ItemsSource = accounts;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Users newUser = new Users();
            newUser.Show();
            this.Close();
        }

        private void listAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            accountNo = Convert.ToUInt32(listAccounts.SelectedItem);
            bank_biz.SelectAccount(accountNo);
            txtaccountNo.Text = accountNo.ToString();
            uint balance = bank_biz.GetBalance();

            if (balance == 0)
            {
                txtBalance.Text = "0";
            }
            else
            {
                txtBalance.Text = balance.ToString();
            }
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (listAccounts.SelectedItem == null || txtamount.Text == null)
            {
                MessageBox.Show("Please Select a Account and Enter Amount");
            }
            else
            {
                accountNo = Convert.ToUInt32(listAccounts.SelectedItem);
                bank_biz.SelectAccount(accountNo);
                uint amount = Convert.ToUInt32(txtamount.Text);
                bank_biz.Deposit(amount);
                uint balance = bank_biz.GetBalance();
                txtBalance.Text = balance.ToString();
                MessageBox.Show("Account No. " + accountNo + " has been credited");
            }

        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            if (listAccounts.SelectedItem == null || txtamount.Text == null)
            {
                MessageBox.Show("Please Select a Account and Enter Amount");
            }

            else
            {


                accountNo = Convert.ToUInt32(listAccounts.SelectedItem);
                bank_biz.SelectAccount(accountNo);
                uint amount = Convert.ToUInt32(txtamount.Text);
                uint current_balance = bank_biz.GetBalance();

                if (amount > current_balance)
                {
                    MessageBox.Show("Withdrawal Failed! Not Enough Funds.");
                }
                else
                {
                    bank_biz.Withdraw(amount);
                    MessageBox.Show("Account No. " + accountNo + " has been debited");
                }

                uint newbalance = bank_biz.GetBalance();
                txtBalance.Text = newbalance.ToString();
            }

        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            //Passing UserID and AccountNo to Transaction Window
            string selectedAccountNo = txtaccountNo.Text;
            string selectedUserID = txtuserID.Text;
            Transactions newTransaction = new Transactions(selectedAccountNo, selectedUserID);
            newTransaction.Show();
            this.Close();
        }
    }
}
