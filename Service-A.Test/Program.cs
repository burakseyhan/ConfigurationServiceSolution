using System;
using System.Configuration;
using System.Threading;

namespace Service_A.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This project configs read from database");

            while (true)
            {
                foreach (string item in ConfigurationManager.AppSettings.Keys)
                {
                    Console.WriteLine(item);
                }

                Thread.Sleep(10000);
            }
        }
    }
}
