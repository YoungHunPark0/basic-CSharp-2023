using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs01_variable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte bdata = byte.MaxValue; // byte의 최대값
            Console.WriteLine(bdata);

            bdata = byte.MinValue;      // byte의 최소값
            Console.WriteLine(bdata);

            long ldata = long.MaxValue;
            Console.WriteLine(ldata);

            ldata = long.MinValue;
            Console.WriteLine(ldata);

            ldata--;
            Console.WriteLine(ldata);

            int binVal = 0b11111111;    // 2진수
            Console.WriteLine(binVal);

            int hexVal = 0xFF;
            Console.WriteLine(hexVal);

            float fdata = float.MaxValue;
            Console.WriteLine(fdata);

            fdata = float.MinValue;
            Console.WriteLine(fdata);

            double ddata = double.MaxValue;
            Console.WriteLine(ddata);


        }
    }
}