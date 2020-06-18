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
                //нужно проверить вдруг пути не существует и тогда программа выдаст ошибку на следующем шаге
                if (!System.IO.File.Exists(path))  // восклицательный знак вначале - это NOT (меняет true на false и наоборот)
                    return ""; //выйти из метода и вернуть пустую строку


                string[] lines = System.IO.File.ReadAllLines(path);


                var lngth = key.Length; //Это для чего?

                foreach (string line in lines)
                {
                    var a = line.Split(';');
                    var x = a[0];

                    //это все не нужно
                    /* 
                    if (string.IsNullOrEmpty(x))
                        Console.WriteLine("false"); //он будет каждый раз выводить false если пустая строка. Зачем?
                    else
                    {
                        var input = line[0]; 
                        var result = line[1];
                    }*/


                    // нужно сравнить x c ключом key
                    // и если ключ совпадает, то выдать значение из правого столбца a[1]
                    if (x == key)
                    {
                        return a[1].Trim();
                        //Trim нужен чтобы удалить лишние пробелы вначале и в конце
                        //после return метод прекращает выполняться
                    }
                }
                
            }
            //return [1]; так не соберется, потому что непонятно что такое [1]

            //до этого места программа доберется только в том случае, если не сработает return внутри цикла foreach
            //а это значит, что совпадений не было найдено и нужно вернуть пустую строку
            return "";
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
