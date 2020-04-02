using System;
using System.Collections.Generic;

namespace Task2
{
	public Program()
	{
		static void Main(string[] args)
        {
			var path = @"E:\Repos\ROBOT_BI\Modules\Facades\Facade Blocks.csv";
			var key = "kit_in_sec_0";
			
			string facadeNames = GetData(path, key);

			string facadeName = GetRandom(facadeNames);
        }


	}
}
