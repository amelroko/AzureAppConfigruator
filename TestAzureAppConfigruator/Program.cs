using System;
using System.Threading;

namespace TestAzureAppConfigruator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message = System.Configuration.ConfigurationManager.AppSettings["dev-key"];
            string message2 = System.Configuration.ConfigurationManager.AppSettings["base-key"];
            Console.WriteLine(message);
            Console.WriteLine(message2);

            Thread.Sleep(3000);

            //string message3 = System.Configuration.ConfigurationManager.AppSettings["base-amel"];

            //Console.WriteLine(message3);

            //Thread.Sleep(1000);
        }
    }
}
