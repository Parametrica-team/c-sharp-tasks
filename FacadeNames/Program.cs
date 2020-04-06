using System;
using System.Collections.Generic;

namespace calculation
{
    class Program
    {
        static void Main(string[] args)
        {
            string testOne = "dor_out_1st";
            string[] testTwo = new string[2];
            testTwo[0] = "win_in_sec_0s";
            testTwo[1] = "kit_out_sec_1";

            string path = @"E:\ROBOT_BI\Modules\Facades\Facade Blocks.csv";

            Console.WriteLine(GetRandom(GetData(path, testTwo[0])));

        }

        public static string GetRandom(string options)
        {

            string[] eachOption = options.Split(',');
            Random rnd = new Random();
            int optionIndex = rnd.Next(eachOption.Length);
            return eachOption[optionIndex];
        }

        public static string GetData(string path, string key)
        {
            string[] allLines = System.IO.File.ReadAllLines(path);

            Dictionary<string, string> users = new Dictionary<string, string>();

            foreach (string oneLine in allLines)
            {
                try
                {
                    users.Add(oneLine.Split(';')[0], oneLine.Split(';')[1]);
                }
                catch (ArgumentException)
                {

                }
            }
            try
            {
                return users[key];
            }
            catch (KeyNotFoundException)
            {
                return ($"null");
            }
        }
        public static string[] GetData(string path, string[] keys)
        {
            string[] res = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                res[i] = GetData((path), (keys[i]));
            }
            return res;
        }

    }
}
