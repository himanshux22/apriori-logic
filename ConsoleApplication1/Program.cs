using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string s = "m,o,n,k,e,y";
            string item1 = "e";
            if (s.Contains(item1))
            {
                Console.WriteLine("yes");
            }
            Console.Read();
        }
    }
}
