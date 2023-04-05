using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs10_operator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 비트연산자 중 시프트 연산자는 실무에서 종종씀
            // << == *2 ,   >> == /2  
            int firstval = 15; // 15 = 0x1111 = 10진수 1111 
            int secondval = firstval << 1; // 11110  16+8+4+2=30
            Console.WriteLine(secondval);

            // 여기서부터 비트연산은 게임에서 많이씀
            Console.WriteLine("============");
            // 1111 & 1101 => 1101    15 & 13 => 13
            // 1010 | 0101 => 1111
            firstval = 15;
            secondval = 13;
            Console.WriteLine(firstval&secondval);
            Console.WriteLine("============");
            firstval = 10; // 1010
            secondval = 5; //  101
            Console.WriteLine(firstval|secondval);
            Console.WriteLine(firstval ^ secondval); // XOR 둘다 다르면 1
            Console.WriteLine(~secondval); // 보수
            // 실무에서는 많이 않씀
            Console.WriteLine("============");
            // Null 병합 연산자
            //필요한 변수/ 객체의 null 검사 를 간결하게 만들어주는 역할
            //형식: OP1 ?? OP2
            //OP1이 Null인지 여부에 따라.

            int? checkval = null;
            Console.WriteLine(checkval == null? 0:checkval); // 3항연산자.  값 0
            Console.WriteLine(checkval ?? 0); // null 병합연산자는 3항연산자를 더 축소 시킴. 값 0

            checkval = 25;
            Console.WriteLine(checkval == null ? 0 : checkval); // 3항연산자. 값 25
            Console.WriteLine(checkval.HasValue ? checkval.Value:0); // 값 25. 위와 같음 이전시간 배운 HasValue 활용
            // Hasvalue가 참이면 
            Console.WriteLine(checkval ?? 0); // null 병합연산자는 3항연산자를 더 축소 시킴. 값 25
        }
    }
}
