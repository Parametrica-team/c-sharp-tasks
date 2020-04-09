using System;

namespace ConsoleApp1
{
    class GrasshopperTask
    {
        private static string GetData(string path, string key)
        {
            // проверка на существование файла
            if (!System.IO.File.Exists(path))
            {
                return null;
            }


            // чтение, разбивка, возврат "значения" при соответствии "ключа"
            string[] lines = System.IO.File.ReadAllLines(path);           

            foreach (string line in lines)
            {
                var lineParts = line.Split(';');
                var keys = lineParts[0];
                var values = lineParts[1];

                if (keys == key)
                    return values;
            }

            return null;
        }


        // сгенерирует рандомный индекс (не более длины массива) и выдаст соответствующий элемент
        private static string GetRandom(string text)
        {
            // если пришла пустая строка
            if (string.IsNullOrEmpty(text))
                return null;

            var textPart = text.Split(',');
            var length = textPart.Length;

            Random ind = new Random();
            var index = ind.Next(length);

            var trimmedTextPart = textPart[index].Trim();

            return trimmedTextPart;
        }



        static void Main()
        {
            var path = @"E:\Repos\ROBOT_BI\Modules\Facades\Facade Blocks.csv";
            var key = "kit_in_sec_0";

            Console.WriteLine(GetData(path, key));
            Console.WriteLine(GetRandom(GetData(path, key)));
        }
    }
}

