using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            var w = 1800;
            var d = 500;
            double width = w;
            double depth = d;

            double height = 1800;


            Console.WriteLine(GetAngle(w,d));
            Console.ReadLine();
        }
        
        static double GetAngle(double width, double depth)

        {

            double sunAngle = ((((width / 2) / depth) * Math.PI / 180));

            
            return sunAngle;

           
        }

    }
           
}
