using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 조건 연산자의 형식
조건식 ? 참일_때의_값 : 거짓일_때의_값
조건식: 결과가 논리 값
 */
namespace cs08_conditions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 30;
            string result = "";
            if (a == 30) 
            {
                result = "삼십";
            }
            else
            {
                result = "삼십아님";
            }

            // 이렇게 쓰면 너무 김

            // 조건 연산자 = 삼항 연산자
            int b = 40;
            string result2 = b == 40 ? "사십" : "사십아님";
        }
    }
}
