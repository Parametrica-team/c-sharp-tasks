using System;
using System.Collections.Generic;

namespace Task2
{
	public class Program
	{
        static void Main(string[] args)
        {
            var path = @"E:\Repos\ROBOT_BI\Modules\Facades\Facade Blocks.csv";
            var key = "kit_in_sec_1";

            string facadeNames = GetData(path, key);
            string facadeName = GetRandom(facadeNames);

            Console.WriteLine(facadeNames);
            Console.WriteLine(facadeName);
            Console.ReadLine();
        }

        private static string GetRandom(string text)
        {
        }

        private static string GetData(string path, string key)
        {
        }
	}
}
