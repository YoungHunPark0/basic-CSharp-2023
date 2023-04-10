using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs20_abstractCalss
{// virtual은 위에서 만들걸 밑에서 다시 재정의
    abstract class AbstractParent // 추상메서드를 쓸려면 추상클래스를 써야함
    {
        protected void MethodA() // 자신과 하위클래스에서 상속받으면 쓸 수 있음
        {
            Console.WriteLine("AbstractParent.MethodA()");
        }

        public void MethodB()  // 클래스랑 동일!
        {
            Console.WriteLine("AbstractParent.MethodB()");
        }

        public abstract void MethodC();// 인터페이스랑 기능은 동일! 추상메서드
    }

    class Child : AbstractParent //  public abstract void MethodC()을 인터페이스처럼 구현해야함
    {
        public override void MethodC() // 추상메서드에서는 override는 재정의(사실은 구현!)
            // 인터페이스와 차이를 위해서 override를 씀(virtual에서 재정의할때씀)
        {
            Console.WriteLine("Child.MethodC() - 추상클래스 구현!");
            MethodA();
        }
    }

    abstract class Mammal // 포유류 최상위클래스 - 부모와 자식중간에 쓰는 클래스
    {
        public void Nurse() // 인터페이스와의 차이점
        {
            Console.WriteLine("포유한다");
        }
        public abstract void Sound();
    }

    class Dogs : Mammal 
    {
        public override void Sound()
        {
            Console.WriteLine("멍멍!!"); 
        }
    }
    
    class Cats:Mammal
    {   
        public override void Sound()  // 동일한 sound라는 메서드지만 객체에 따라 구체화해서 다른일을 할 수 있음
        {
            Console.WriteLine("야옹!!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AbstractParent parent = new Child(); // 부모클래스로 형변환해서 자식클래스를 부모클래스에 할당
            parent.MethodC();
            parent.MethodB();
            //parent.MethodA();  // methodA는 안됨->protected는 자기자신과 자식클래스내에서만 사용가능

        }
    }
}
