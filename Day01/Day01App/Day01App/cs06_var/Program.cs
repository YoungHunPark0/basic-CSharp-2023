using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
C#은 강력한 형식 언어
의도치 않은 형식의 데이터를 읽거나 할당하는 일 차단
약한 형식 검사
코드 작성 단계에서 편리
컴파일러에서 변수에 담긴 데이터에 따라 자동으로 형식 지정
C#의 약한 형식 검사 방법 🡪 Var
선언과 동시 초기화 필수
지역 변수로만 사용
*/
namespace cs06_var
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var a = 4000000;
            Console.WriteLine("type : {0}, value : {1}", a.GetType(), a);

            var b = 3.141592; // f여부 따라서 double / float 변경구분
            Console.WriteLine("type : {0}, value : {1}", b.GetType(), b);

            var c = "Basic C#";
            Console.WriteLine("type : {0}, value : {1}", c.GetType(), c);

        }
    }
}
