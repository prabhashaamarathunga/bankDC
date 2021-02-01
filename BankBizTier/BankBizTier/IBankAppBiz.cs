using System.Collections.Generic;
using System.ServiceModel;

//Curtin ID: 20204646

namespace BankBizTier
{
    [ServiceContract]

    public interface IBankAppBiz
    {

        //User Access

        [OperationContract]
        void SelectUser(uint userID);

        [OperationContract]
        List<uint> GetUsers();

        [OperationContract]
        uint CreateUser();

        [OperationContract]
        void GetUserName(out string fname, out string lname);

        [OperationContract]
        void SetUserName(string fname, string lname);

        //Account Access

        [OperationContract]
        void SelectAccount(uint accountID);

        [OperationContract]
        List<uint> GetAccountIDsByUser(uint userID);

        [OperationContract]
        uint CreateAccount(uint userID);

        [OperationContract]
        void Deposit(uint amount);

        [OperationContract]
        void Withdraw(uint amount);

        [OperationContract]
        uint GetBalance();

        [OperationContract]
        uint GetOwner();

        //Transaction Access

        [OperationContract]
        void SelectTransaction(uint TransactionID);

        [OperationContract]
        List<uint> GetTransactions();

        [OperationContract]
        uint CreateTransaction();

        [OperationContract]
        uint GetAmount();

        [OperationContract]
        uint GetSendrAcct();

        [OperationContract]
        uint GetRecvrAcct();

        [OperationContract]
        void SetAmount(uint amount);

        [OperationContract]
        void SetSendr(uint acctID);

        [OperationContract]
        void SetRecvr(uint acctID);

        [OperationContract]
        List<uint> FilterTransactions(uint accountNo);
    }
}
