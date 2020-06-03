using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowRecess
{
    class Program
    {
        static void Main(string[] args)
        {

            double GetAngle(double height, double depth)
            {
                var angle = Math.PI/2-(Math.Atan((depth / height) + Math.PI*0));
                return angle;
            }

            Console.WriteLine("Введите высоту окна:");
            string hieghtA = Console.ReadLine();
            Console.WriteLine("Введите глубину окна:");
            string depthB = Console.ReadLine();
            var resultRadians = GetAngle(double.Parse(hieghtA), double.Parse(depthB));
            var resultAngle = resultRadians*(180/ Math.PI);
            Console.WriteLine("Угол в градусах");
            Console.WriteLine(resultAngle);
            Console.WriteLine("Угол в радианах");
            Console.WriteLine(resultRadians);
            Console.ReadKey();
        }
    }
}
