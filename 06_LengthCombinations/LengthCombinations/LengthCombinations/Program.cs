using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LengthCombinations
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new int[] { 2, 3, 4 };
            var length = 7;

             GetAllCombinations(List<int> values, int length);

        }

        private static  List<List<int>> GetAllCombinations(List<int> values, int length)
        {
            foreach (var step in values)
            {
                stack.Push(step);
                //Print(step.ToString());
                int stackLength = GetStackLength();
                if (stackLength > fullLength)
                {
                    //добавить без последнего компонента
                    stack.Pop();
                    res.Add(stack.ToArray());
                    continue;
                }
                else if (stackLength == fullLength)
                {
                    res.Add(stack.ToArray());
                    stack.Pop();
                    break;
                }
                else
                {
                    AddStep();
                    stack.Pop();
                }

            }
            throw new NotImplementedException();
        }
    }
}
