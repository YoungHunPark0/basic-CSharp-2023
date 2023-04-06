using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
얕은 복사
객체를 복사할 때 참조만 살짝 복사
깊은 복사
별도의 힙 공간에 객체 자체 복사
 */
namespace cs14_deepcopy
{
    class SomeClass
    {
        public int SomeField1;
        public int SomeField2;

        public SomeClass DeepCopy()
        {
            SomeClass newCopy = new SomeClass();
            newCopy.SomeField1 = this.SomeField1; // Call by Value
            newCopy.SomeField2 = this.SomeField2; // newCopy.SomeField2와 SomeField2는 달라서 this 꼭 안붙여도됨

            return newCopy;
        }
    }
    // this 사용법
    /*
    객체가 자신을 지칭할 때 사용하는 키워드 this
    객체 내부에서 자신의 필드나 메소드에 접근할 때 사용
     */
    class Employee
    {
        private string Name;

        public void SetName(string Name)
        {
            this.Name = Name; // 멤버변수(속성)과 메서드의 매게변수이름이 대소문자까지 완전히 똑같을때, 꼭 써야함!
        }
    }

    class ThisClass
    {
        int a, b, c;
        
        public ThisClass() 
        {
            this.a = 1;
        }

        public ThisClass(int b) : this()
        {
            this.b = 2;
        }

        public ThisClass(int b, int c) : this(b)
        {
            this.c = 3;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("얕은 복사 시작"); // source와 target이 (주소를 복사했기 때문에) 값이 쉐어!

            SomeClass source = new SomeClass();
            source.SomeField1 = 100;
            source.SomeField2 = 200;

            SomeClass target = source;
            target.SomeField2 = 300;

            Console.WriteLine("s.SomeField1 => {0}, s.SomeField2 => {1}",
                               source.SomeField1, source.SomeField2);
            Console.WriteLine("t.SomeField1 => {0}, t.SomeField2 => {1}",
                               target.SomeField1, target.SomeField2); // 두개 값이 똑같이 나오는 문제생김-> 깊은복사해야됨

            Console.WriteLine("깊은 복사 시작!!");

            SomeClass s = new SomeClass();
            s.SomeField1 = 100;
            s.SomeField2 = 200;

            SomeClass t = s.DeepCopy(); // 깊은 복사
            t.SomeField2 = 300;

            Console.WriteLine("s.SomeField1 => {0}, s.SomeField2 => {1}",
                               s.SomeField1, s.SomeField2);
            Console.WriteLine("t.SomeField1 => {0}, t.SomeField2 => {1}",
                               t.SomeField1, t.SomeField2); // 깊은복사 해서 값이 분리됨

        }
    }
}
