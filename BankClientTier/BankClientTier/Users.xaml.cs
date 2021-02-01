using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ServiceModel;
using BankBizTier;

//Curtin ID: 20204646

namespace BankClientTier
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        IBankAppBiz bank_biz;
        private uint userID;

        public Users()
        {
            InitializeComponent();

            ChannelFactory<IBankAppBiz> channelFactory;
            NetTcpBinding tcpBind = new NetTcpBinding();

            channelFactory = new ChannelFactory<IBankAppBiz>(tcpBind, "net.tcp://localhost:50002/BankAppBiz");
            bank_biz = channelFactory.CreateChannel();

            //View User List at the startup
            List<uint> users = bank_biz.GetUsers();
            userList.ItemsSource = users;

        }

        private void accounts_Click(object sender, RoutedEventArgs e)
        {
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {

        }

        private void transactions_Click(object sender, RoutedEventArgs e)
        {
        }


        private void txtuserID_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bank_biz.CreateUser();
            MessageBox.Show("New User Created Successfully");

            //Refresh the User List with new Added users
            List<uint> users = bank_biz.GetUsers();
            userList.ItemsSource = users;
        }

        private void btnSelectUsers_Click(object sender, RoutedEventArgs e)
        {
            if (txtuserID.Text == null)
            {
                MessageBox.Show("Please, Select a User");
            }
            else
            {
                //Passing UserID to Accounts Wiindow
                string selectedUserID = txtuserID.Text;
                Accounts newAccount = new Accounts(selectedUserID);
                newAccount.Show();
                this.Close();
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //Clearing Text Feilds
            txtuserID.Text = "";
            txtfname.Text = "";
            txtlname.Text = "";
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fname, lname;
            userID = Convert.ToUInt32(userList.SelectedItem);
            bank_biz.SelectUser(userID);
            bank_biz.GetUserName(out fname, out lname);

            //Assigning Selected User Details to text feilds
            txtuserID.Text = userID.ToString();
            txtfname.Text = fname;
            txtlname.Text = lname;
        }

        private void btnSetName_Click(object sender, RoutedEventArgs e)
        {
            string fname = txtfname.Text;
            string lname = txtlname.Text;
            if(userList.SelectedItem == null)
            {
                MessageBox.Show("Please, Select a User ID");
            }
            else
            {
                userID = Convert.ToUInt32(userList.SelectedItem);
                bank_biz.SelectUser(userID);
                bank_biz.SetUserName(fname, lname);
                MessageBox.Show("Name Set Successfully");
            }
            
        }
    }
}
