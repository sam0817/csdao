using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            DAO k = new DAO();
            k.QueryData();
            k.PrintTable();
            Console.ReadLine();
        }
    }
}
