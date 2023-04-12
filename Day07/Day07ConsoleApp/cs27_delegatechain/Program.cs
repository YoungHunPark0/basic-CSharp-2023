using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace cs27_delegatechain
{
    delegate void ThereIsAFire(string location); // 불났을때 대신해주는 대리자

    delegate int Calc(int a, int b); // 대리자

    delegate string Concatenate(string[] args);
    #region < 클래스 Sample 람다식 프로퍼티 >

    class Sample
    {
        private int valueA; // 이렇게만 쓰면 멤버변수. 프로퍼티 아님!, 외부에서 접근불가

        //public int ValueA // 프로퍼티
        //{
        //    //get { return valueA; } 
        //    //set { valueA = value; }

        //    // 람다식
        //    //get => valueA; 
        //    //set => valueA = value;         
        //}
        public int ValueA
        {
            get => valueA; // get은 람다식 
            set  // set은 일반식
            { 
                valueA = value; 
            } 
        }
    }
    #endregion
    internal class Program
    {
        #region < 설명예시안 >
        static void Call119(string location) 
        {
            Console.WriteLine("소방서죠? {0}에 불났어요!!", location); 
        }

        static void ShoutOut(string location) 
        {
            Console.WriteLine("{0}에 불났어요!", location);
        }

        static void Escape(string location) // 탈출하다
        {
            Console.WriteLine("{0}에서 탈출합니다.", location);
        }
        #endregion

        static string ProcConcate(string[] args)
        {
            string result = string.Empty; // == "";
            foreach (string s in args)
            {
                result += s + "/";
            }

            return result;
        }

        static void Main(string[] args)
        {
            #region < 대리자체인 영역 >
            // 하나하나 따로해야됨
            //var loc = "우리집";
            //Call119(loc);
            //ShoutOut(loc);
            //Escape(loc);

            //Console.WriteLine("======대리자 체인사용");
            //// 불이 날수도 있으니까 미리 준비
            //var otherloc = "경찰서";
            //// 대리자 체인 thereisafire 대리자에 전부 넣어서 한번에 실행가능
            //ThereIsAFire fire = new ThereIsAFire(Call119);
            //fire += new ThereIsAFire(ShoutOut);
            //fire += new ThereIsAFire(Escape); // 대리자에 메서드 추가

            //fire(otherloc);

            //Console.WriteLine("===ShoutOut 대리자 체인사용 안하겠다.");
            //fire -= new ThereIsAFire(ShoutOut); // 대리자에서 메서드를 삭제

            //fire("다른집");

            //// 익명함수
            //Console.WriteLine("=====익명함수");
            //Calc plus = delegate (int a, int b)
            //{
            //    return a + b;
            //};

            //Console.WriteLine(plus(6, 7));

            //Calc minus = delegate (int a, int b)
            //{
            //    return a - b;
            //};
            //// == 람다식표현 Calc minus = (a, b) => { return a - b; }; 더줄일 수 있음
            //Console.WriteLine(minus(67, 9));

            //Calc simpleMinus = (a,b) => a - b; // 람다식 : 익명함수를 아주 간결하고 심플하게 쓰겠다
            #endregion
            Console.WriteLine("===메서드를 만들어 대리자를 사용(디버깅에 등록하거나)");
            Concatenate concat = new Concatenate(ProcConcate);
            var result = concat(args);

            Console.WriteLine(result);
            // cs27_delegate속성 디버그에 Hello World C# 저장함

            Console.WriteLine("===람다식으로 익명함수를 만듬");
            Concatenate concat2 = (arr) =>
            {
                string res = string.Empty; // == ""; // result랑 겹치니 이름 변경
                foreach (string s in args)
                {
                    res += s + "/";
                }

                return res;
            };

            Console.WriteLine(concat2(args));
        }
    }
}
