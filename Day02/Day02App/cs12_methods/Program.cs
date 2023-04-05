using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace cs12_methods
{
    class Calc
    { // static 정적 - static은 프로그램 끝날 때 까지 살아있음->언제든지 접근가능.
        // 프로그램 실행되자말자 접근가능한것 main이라는 이름의 static은 제일먼저 접근한다 => 동적
        public static int Plus(int a, int b)
        //public int Plus(int a, int b)
        { 
            return a + b;
        }
        
        //public static int Minus(int a, int b)
        public int Minus(int a, int b)
        { 
            return a - b;
        }

    }
    internal class Program
    {
        /// <summary>
        /// 실행시 메모리에 최초 올라가야 하기때문에 static이 되어야 하고
        /// 메서드 이름이 Main이면 실행될때 알아서 제일 처음에 시작된다.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) // static int main 쓸 경우 마지막에 return 0; 해야됨
        {
            #region < static 메서드 >
            int result = Calc.Plus(1, 2); // static은 최초실행 때 메모리에 바로 올라가기때문에
                                          // 클래스의 객체를 만들 필요가 없음. like new Calc();
                                          // int result = new Calc().Plus(1, 2); // static 없을 때=> 무조건 New사용 위와동일
                                          // Calc.Minus(3, 2); Minus는 static이 아니기때문에 접근불가(객체생성해야 접근가능)
            result = new Calc().Minus(3, 2); // static 없을 때=> 무조건 New사용
            Console.WriteLine(result);

            #endregion

            #region < Call by reference vs Call by value 비교 >
            Console.WriteLine("======Swap");
            int x = 10; int y = 3;
            //Swap(x, y); // public void Swap하면 쓸 수 없음! static 써야함
            Swap(ref x, ref y); // x, y가 가지고 있는 주소를 전달하라 Call by reference == pointer

            Console.WriteLine("x = {0}, y = {1}", x, y); // 원본값은 안바뀜

            Console.WriteLine(GetNumber());
            #endregion

            #region < out 매개변수 >
            Console.WriteLine("======out 매개변수로 2개의 값을 가짐");
            int divid = 10;
            int divor = 3;

            int rem = 0;
            //Divide(divid, divor, ref result, ref rem);
            Divide(divid, divor, out result, out rem); // ref를 쓰든 out을 쓰든 결과 차이 없음!
            Console.WriteLine("나누기 값 {0}, 나머지 {1}", result, rem);

            Console.WriteLine("======튜플");
            (result, rem) = Divide(20, 6);
            Console.WriteLine("나누기 값 {0}, 나머지 {1}", result, rem);

            #endregion

            #region < 가변길이 매개변수 >
            Console.WriteLine("======가변길이 매개변수");
            //int resSum = Sum(1, 3, 5, 7, 9);
            //Console.WriteLine(resSum)
            Console.WriteLine(Sum(1, 3, 5, 7, 9)); // 위와 동일

            #endregion
        }
        //static int Divide(int x, int y)
        //{
        //    return x / y;
        //}
        //static int Reminder(int x, int y)
        //{
        //    return x % y;
        //} 위 2개식 합치기!
        // static void Divide(int x, int y, ref int val, ref int rem)
        static void Divide(int x, int y, out int val, out int rem)
        {
            val = x/y;
            rem = x%y;
        }

        static (int result, int rem) Divide(int x, int y)
        {
            return (x / y, x % y);   // 튜플. C# 7.0
        }

        static (float result, float rem) Divide(float x, float y) // 오버로딩
        {
            return (x / y, (int)(x % y)); 
        }
        /*
         오버로딩:
         하나의 메소드 이름에 여러 개의 구현을 올리는 것
         매개 변수의 수와 형식을 분석해 호출할 메소드 결정
        */

        // Main 메서드와 같은레벨에 있는 메서드들은 전부 static이 되어야함(무조건!)
        // public static void Swap(int a, int b) // Swap(x=5,y=4) 값을 보내줌 ->> call by value,
        public static void Swap(ref int a, ref int b)
        { //  그냥 int a, b하면 원본값은 안바뀜

            int temp = 0;
            temp = a; // 5 : temp = 5
            a = b; // a = 4
            b = temp; // 5
        }

        static int val = 100;
        public static ref int GetNumber() 
        {
            // 메소드의 결과를 참조로 반환하기
            return ref val; // static 메소드에 접근가능한 변수는 static뿐
        }

        public static int Sum(params int[] args) // Python 가변길이 매개변수랑 비교. (*args)랑 같음
        {
            int sum = 0;

            foreach (var item in args)
            {
                sum += item;
            }
            return sum;
        }
    }
}
