using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Web
{
    public class Print
    {
        public int MyProperty  =2;
        public Print()
        {
            Console.WriteLine("create");
        }
        public void WriteLine(string a){
            MyProperty++;
            Console.WriteLine($"message: {a})");

        }
    }
}