using System;
using System.Collections.Generic;
using System.Linq;

namespace Percentage
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static List<int> AdjustPercentage(List<double> numbers)
        {
            if (numbers == null)
                return null;

            if (numbers.Sum() != (int)numbers.Sum())
            {
                throw new ArgumentException("Сумма аргументов должна быть целым числом");
            }

            var result = numbers.Select(n => (int)Math.Round(n)).ToList();

            // если разница меньше нуля, то значит нужно уменьшить числа
            // если разница больше нуля, то увеличить
            // теоритически разница может быть больше 1 и нужно будет поправить несколько чисел!
            int diff = (int)(numbers.Sum() - result.Sum());

            if (diff < 0)
            {
                // нужно отсортировать индексы исходных значений в зависимости от дробной части
                var sortedIndexes = numbers.Select((num, index) => new { num, index })
                    .Where(pair => pair.num % 1 >= 0.5)
                    .OrderBy(pair => pair.num % 1)
                    .Select(pair => pair.index)
                    .ToList();

                var indexes = sortedIndexes.Take(-diff).ToList();

                // уменьшить значения по получившимся индексам
                indexes.ForEach(i => result[i]--);
            }
            else if (diff > 0)
            {
                // нужно отсортировать индексы исходных значений в зависимости от дробной части
                var sortedIndexes = numbers.Select((num, index) => new { num, index })
                    .Where(pair => pair.num % 1 > 0 && (pair.num % 1) < 0.5)
                    .OrderByDescending(pair => pair.num % 1)
                    .Select(pair => pair.index)
                    .ToList();

                var indexes = sortedIndexes.Take(diff).ToList();

                // увеличить значения по получившимся индексам
                indexes.ForEach(i => result[i]++);
            }

            return result;
        }
    }
}
