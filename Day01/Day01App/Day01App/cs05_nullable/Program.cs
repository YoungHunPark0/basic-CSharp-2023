using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs05_nullable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Nullable : 어떤 값도 가지지 않는 변수가 필요할 때
            // 0이 아닌 비어 있는 변수, 즉 null 상태

            //int a= 0; // int a null값을 담을 수 없음.
            //C# 6.0 Nullable 추가
            int? a = null; // ? null값 담을 수 있음
            Console.WriteLine(a==null);
            //Console.WriteLine(a.GetType());  예외발생
            Console.WriteLine("============");
            int b = 0;
            Console.WriteLine(b==null);
            Console.WriteLine(b.GetType());

            // 값 형식 byte, short, int, long, float, double, char 등은 NULL값을 할당X
            // NULL을 할당할 수 있도록 만드는 방식 => type? ex) int? a= null;
            // bool val = null; 
            // string strval = null; string은 참조형식이라 null 가능
            Console.WriteLine("============");
            float? c=null;
            Console.WriteLine(c.HasValue); // c에 값이 있는지 알아봄->HasValue
            Console.WriteLine(c); // NULL값은 값이 비어서 나옴
            Console.WriteLine("============");

            c = 3.14f;
            Console.WriteLine(c.HasValue);
            Console.WriteLine(c.Value);
            Console.WriteLine(c);
        }
    }
}
