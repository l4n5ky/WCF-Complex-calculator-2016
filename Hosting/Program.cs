using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using UslugiSieciowe;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAdress = new Uri("http://localhost:51343/");
            string endpointAdress = "http://localhost:51343/Service1.svc";

            using (ServiceHost host = new ServiceHost(typeof(Service1), baseAdress))
            {
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), endpointAdress);


                host.Open();
                Console.WriteLine("Service1 hosted at address : " + baseAdress);
                Console.WriteLine("Endpoint adress : " + endpointAdress);
                Console.ReadKey(true);
                host.Close();  
            }
        }
    }
}
