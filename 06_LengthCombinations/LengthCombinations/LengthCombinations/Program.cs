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
            List<int> values = new List<int>() { 2, 3, 4};
            var length = 7;

             GetAllCombinations(values, length);

        }

        private static  List<List<int>> GetAllCombinations(List<int> values, int length)
        {
            //List<int> allSteps;
            Stack<int> stack;
            List<int[]> res;
            int fullLength; 

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
