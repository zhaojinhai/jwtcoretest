using System;
using Jose;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace JWTConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            addString("adf", "-asdfaf");
            Console.WriteLine("Hello World!");
            Console.Read();
        }
        static void addString(string x, string y) => Console.WriteLine(x + y);
    }
}
