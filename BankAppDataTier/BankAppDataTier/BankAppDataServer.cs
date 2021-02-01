using System;
using System.ServiceModel;

//Curtin ID: 20204646

namespace BankAppDataTier
{
    class BankAppDataServer
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Bank Data Server Started");

                ServiceHost hostData;
                NetTcpBinding tcpBinding = new NetTcpBinding();

                hostData = new ServiceHost(typeof(BankAppImpl));
                hostData.AddServiceEndpoint(typeof(IBankApp), tcpBinding, "net.tcp://localhost:50001/BankAppData");

                hostData.Open();
                Console.WriteLine("Press Enter To Exit");
                Console.ReadLine();
                hostData.Close();
            }
            catch (FaultException exception)
            {
                Console.WriteLine("Connection faiure! DataServer couldn't started" + exception);
            }
        }
    }
}
