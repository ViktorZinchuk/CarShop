using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            VendorManager vendor = new VendorManager();

            Console.WriteLine("Press R to sign up on Site, Press T to sign out");
            ConsoleKeyInfo key;
            key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.X)
            {
                if (key.Key == ConsoleKey.R)
                {
                    vendor.Registration();
                    Console.Clear();
                    Console.WriteLine("Vendor sign up!!!");
                    Console.WriteLine(vendor.WcfServiceUri);
                }

                if (key.Key == ConsoleKey.T)
                {
                    vendor.UnRegistration();
                    Console.Clear();
                    Console.WriteLine("Vendor sign out!!!");
                }

                Console.WriteLine("Press R to sign up on Site, Press T to sign out");
                key = Console.ReadKey(true);
            }
        }
    }
}
