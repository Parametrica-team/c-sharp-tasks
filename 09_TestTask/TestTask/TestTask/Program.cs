using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var path = @"C:\Users\rik22\Downloads\Telegram Desktop\cities.csv";
            
            List<string> findCities = GetData(path);
            foreach (var e in findCities)
                Console.WriteLine(e);
            Console.WriteLine(findCities.Count);
            Console.ReadLine();
        }

        private static List <string> GetData(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            var list = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                
                var parts = lines[i].Split(',');
                var a = parts[5];
                var b = parts[1];
                var time = "XVII век";
                bool success = Int32.TryParse(a, out int result);
                if (success)
                {
                    if ((int.Parse(a) < 1700) && (int.Parse(a) > 1600))
                        list.Add(b);
                }
                else 
                    if (a == time)
                    list.Add(b);

            }
            //string output = string.Join(Environment.NewLine, list.ToArray());
            return list;
            
            
            //throw new NotImplementedException();
        }
    }
}
