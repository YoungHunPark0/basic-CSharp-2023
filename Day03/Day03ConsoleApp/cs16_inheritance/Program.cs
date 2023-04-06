using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 <상속(inheritance)>
 물려받는 클래스가 물려줄 클래스 지정
파생 클래스 = 자신만의 고유 멤버 + 기반 클래스 멤버
파생 클래스의 수명 주기
기반 생성자 → 파생 생성자 → 파생 종료자 → 기반 종료자
기반 클래스의 멤버 호출 🡪 base
 파생 클래스의 생성자에서 기반 클래스 생성자에 매개변수 전달

 <기반클래스와 파생클래스 사이의 형식변환>
기반 클래스와 파생 클래스 사이에 족보를 오르내리는 형식 변환이 가능  
 파생 클래스의 인스턴스를 기반 클래스의 인스턴스로 사용 가능
 */
namespace cs16_inheritance
{
    class Base // 기반 또는 부모클래스 
        // 자식클래스에서 상속받으려면 private는 안써야 함
        #region < 부모클래스 선언 >
    {
        protected string Name;
        private string Color; // 만약에 상속을 할꺼면 private를 protected로 변경!!
                              // private여서 접근 설정 따로 해야됨
        public int Age;

        public Base(string Name, string Color, int Age)
        {
            this.Name = Name;
            this.Color = Color; 
            this.Age = Age;
            Console.WriteLine("{0}.Base()", Name);
        }

        public void BaseMethod()
        {
            Console.WriteLine("{0}.BaseMethod()", Name);
        }

        public void GetColor()
        {
            Console.WriteLine("{0}.Base() {1}", Name, Color);
        }
        #endregion

    }

    class Child : Base // 상속받은 이후 Base의 Name, Color, Age를 새로 만들거나 하지않음
    {
        public Child(string Name, string Color,int Age): base(Name, Color, Age)
        {
            Console.WriteLine("{0}.Child()", Name);
        }

        public void ChildMethod() 
        {
            Console.WriteLine("{0}.ChildMethod", Name);
        }

        //public string GetColor()
        //{
        //    Console.WriteLine("{0}.Base() {1}", Name, Color); 
        //    // color는 private 설정이라 접근 못함, public설정한 age는 가능(string이라 안될뿐), protected인 name 가능
        //    // public으로 변경 하면 가능함
        //}
        public void GetChildColor()
        {
           // GetColor(color); 
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Base b = new Base("NameB", "White", 1);
            b.BaseMethod();
            b.GetColor();

            Child c = new Child("NameC", "Black", 2);
            c.BaseMethod();
            c.ChildMethod();
            c.GetColor(); // Base.GetColor Black .. c에서 Color(무조건 부모)에 접근불가!
        }
    }
}
