using System;
using System.ServiceModel;

//Curtin ID: 20204646

namespace BankBizTier
{
    class BankAppBizServer
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Biz Server Started");

                ServiceHost hostBiz;
                NetTcpBinding tcpBinding = new NetTcpBinding();

                hostBiz = new ServiceHost(typeof(BankAppBizImpl));
                hostBiz.AddServiceEndpoint(typeof(IBankAppBiz), tcpBinding, "net.tcp://localhost:50002/BankAppBiz");

                hostBiz.Open();
                Console.WriteLine("Press Enter To Exit");
                Console.ReadLine();
                hostBiz.Close();
            }
            catch (FaultException exception)
            {
                Console.WriteLine("Connection faiure! BizServer couldn't started " + exception);
            }

        }
    }
}
