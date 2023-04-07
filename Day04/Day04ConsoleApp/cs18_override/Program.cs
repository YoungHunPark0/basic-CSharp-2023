using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs18_override
{
    class ArmorSuite // 토니스타크 만든 아머슈트 
    {
        public virtual void Init() // 오버라이드 할꺼면 public virtual
        {
            Console.WriteLine("기본 아머슈트");
        }
    }
    // 부모가 가진걸 상속받으면서 다른것들 추가
    class IronMan : ArmorSuite
    {
        public override void Init()
        {
            base.Init();// base 부모클래스 실행
            Console.WriteLine("리펄서 빔!");
        }
    }

    class WarMachine : ArmorSuite
    {
        public override void Init()
        {
            // base.Init(); // 부모클래스의 Init()실행안함
            Console.WriteLine("미니건");
            Console.WriteLine("돌격소총");
        }
    }

    class Parent
    {
        public void CurrentMethod()
        {
            Console.WriteLine("부모클래스 메서드");
        }
    }

    class Child : Parent 
    {
        public new void CurrentMethod() // 부모클래스 cuurentmethod 숨기고 싶으면 new 사용
        {
            Console.WriteLine("자식클래스 메서드");
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("아머슈트 생산");
            ArmorSuite suite = new ArmorSuite();
            suite.Init();

            Console.WriteLine("워머신 생산");
            WarMachine machine = new WarMachine();
            machine.Init(); // 재정의 오버라이드

            Console.WriteLine("아이언맨 생산");
            IronMan ironMan = new IronMan();
            ironMan.Init();

            Parent parent = new Parent();
            parent.CurrentMethod();

            Child child = new Child();
            child.CurrentMethod();
        }
    }
}
