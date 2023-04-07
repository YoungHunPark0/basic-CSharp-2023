using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
/* 클래스 종류만 가능
 is true false로 반환하여 형병환
 as 그 형식으로 변환 
 */

namespace cs17_inheritance
{
    // 상속해줄 부모클래스
    class Parent
    {
        public string Name; // 상속 할때는 private를 쓰면 안됨!

        public Parent(string Name) 
        {
            this.Name = Name;
            Console.WriteLine("{0} from Parent 생성자", Name); // Parent로 부터 시작되는 생성자
        }
        public void ParentMethod()
        {
            Console.WriteLine("{0} from Parent 메소드", Name);
        }
    }

    // 상속받을 자식 클래스
    class Child : Parent // Child는 Parent클래스를 상속받음
    {
        public Child(string Name) : base(Name) // :base() 부모생성자Parent를 먼저 실행하고 자신의 생성자를 실행함 
        {
            Console.WriteLine("{0} from Child 생성자", Name);
        }

        public void ChildMethod()
        {
            Console.WriteLine("{0} from Child 메소드", Name);
        }
    }

    // 클래스간 형변환 DB처리, DI(의존관계 주입)
    class Mammal // Mammal == 포유류
    {
        public void Nurse() // Nurse == 기르다
        {
            Console.WriteLine("포유류 기르다");
        }
    }

    class Dogs : Mammal
    {
        public void Bark() // 멍멍
        {
            Console.WriteLine("멍멍!!");
        }
    }

    class Cats : Mammal
    {
        public void Meow()
        {
            Console.WriteLine("야용!!");
        }
    }

    class Elephant : Mammal 
    {
        public void Poo()
        {
            Console.WriteLine("뿌!!");
        }
    }

    class ZooKeeper 
    {
        //public void Wash(Dogs dogs)
        //{

        //}

        //public void Wash(Cats cats)
        //{

        //} zookeeper가 각각 지정할 필요없이 하나로 통합 할 수 있음
        public void Wash(Mammal mammal)
        {
            if (mammal is Elephant)
            {
                var animal = mammal as Elephant;
                Console.WriteLine("코끼리를 씻깁니다.");
                animal.Poo();
            }
            else if (mammal is Dogs)
            {
                var animal = mammal as Dogs;
                Console.WriteLine("강아지를 씻깁니다.");
                animal.Bark();
            }
            else if (mammal is Cats)
            {
                var animal = mammal as Cats;
                Console.WriteLine("고양이를 씻깁니다.");
                animal.Meow();
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {   
            #region < 기본상속 개념 >
            Parent p = new Parent("p");
            p.ParentMethod();

            Console.WriteLine("자식클래스 생성");

            Child c = new Child("c");
            c.ParentMethod(); // 부모클래스를 상속받아서 ParentMethod를 쓸 수 있음.
            c.ChildMethod();  // 자식클래스는 부모클래스의 기능, 속성을 모두 쓸 수 있다.
                              // (단!, public, protected 일때만!)
            #endregion

            #region < 클래스간 형식변환 >
       
           //  Mammal mammal = new Mammal(); 기본
            Mammal mammal = new Dogs(); // 자식클래스는 부모클래스를 변환할 수 있음
           // Mammal.Bark(); // 바로안됨 부모클래스의 안에 있어서
            if (mammal is Dogs) 
            {
                Dogs midDog = mammal as Dogs; // 객체를 참조하여 처리
            //== Dogs midDog = (Dogs)mammal; 구식방법 값형식
                midDog.Bark();
            }
           // Dogs dogs = new Mammal(); // 부모클래스가 자식클래스로 변환 불가.
            Dogs dog2 = new Dogs();
            Cats cat2 = new Cats();
            Elephant el2 = new Elephant();

            ZooKeeper keeper = new ZooKeeper();
            keeper.Wash(dog2);
            keeper.Wash(cat2);
            keeper.Wash(el2);

            #endregion
        }
    }
}
