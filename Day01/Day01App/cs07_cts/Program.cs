using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 C#의 모든 데이터 형식 체계
공용 형식 시스템이라는 .NET 프레임워크의 형식 체계의 표준 준수
🡪 .NET 언어들 간의 호환성
CTS를 따르는 C# vs. Visual Basic vs. C++
 */
namespace cs07_cts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Int32 a = 12345; // CTS
            int b = 12345;
            
            Console.WriteLine(a.GetType());
            Console.WriteLine(a);
            Console.WriteLine("========");
            Console.WriteLine(b.GetType());
            Console.WriteLine(b);
            Console.WriteLine("========");

            System.String d = "abcdef"; // CTS는 비추천
            string e = "abcedf";
            Console.WriteLine(d.GetType());
            Console.WriteLine(e.GetType());


        }
    }
}
