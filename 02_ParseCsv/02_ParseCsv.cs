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
            Random rnd = new Random();
            var randomParts = text.Split(',');
            int pIndex = rnd.Next(randomParts.Length);

            return randomParts[pIndex].TrimStart();
        }

        private static string GetData(string path, string key)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {

                var parts = lines[i].Split(';');
                var a = parts[0];
                if (a == key)
                    return parts[1];
            }
            return null;
        }
	}
}
