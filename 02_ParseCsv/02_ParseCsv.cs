using System;
using System.Collections.Generic;

namespace Task2
{
	public class Program
	{
        static void Main(string[] args)
        {
            var path = @"C:\Users\Pm\Desktop\ROBOT_BI\Modules\Facades\Facade Blocks.csv";
            var key = "kit_in_sec_1";

            string facadeNames = GetData(path, key);
            string facadeName = GetRandom(facadeNames);

            Console.WriteLine(facadeNames);
            Console.WriteLine(facadeName);
            Console.ReadLine();
        }
        private static string GetData(string path, string key)
        {
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                var lngth = key.Length;

                foreach (string line in lines)
                {
                    var a = line.Split(';');
                    var x = a[0];

                    if (string.IsNullOrEmpty(x))
                        Console.WriteLine("false");

                    else

                    {
                        var input = line[0];
                        var result = line[1];
                    }
                }
                
            }
            return [1];
        }

        private static string GetRandom(string text)
        {
            Random rnd = new Random();
            var str = text.Split(',');
            int i = rnd.Next(str.Length);

            return str[i].TrimStart();

        }

    }
}
