using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 class ~~~
{
    데이터(명사) string name;
    메소드(동사) void add();
}
 */
namespace cs13_class
{   
    class Cat // 기본적) private이라도 같은 cs13_class 안에 있기 때문에 접근가능!
    {
        #region<생성자>

        /// <summary>
        /// 기본생성자
        /// </summary>
        public Cat()
        {
            Name = string.Empty; // name="무며잉"처럼 지정하면 디폴트값 지정됨
            Color = string.Empty;
            Age = 0;
        } // 있을때나 없을때나 별로 차이가 없음
        
        /// <summary>
        /// 사용자 지정생성자
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="age"></param>
        public Cat(string name, string color = "흰색", sbyte age = 1)
        {
            Name=name;
            Color = color;
            Age=age;  
        }
        #endregion

        #region < 멤버변수 - 속성>
        public string Name;  // 고양이 이름
        public string Color; // 고양이 색상
        public sbyte Age;    // 고양이 나이 sbyte:0~255. 나이는 숫자가 적으니 int 굳이 안써도됨
        #endregion

        #region < 멤버메서드 - 기능>
        public void Meow()
        {
            Console.WriteLine("{0} - 야용!!", Name);
        }

        public void Run()
        {
            Console.WriteLine("{0} 달린다.", Name);
        }
        #endregion
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 위에 쓴 클래스 부름
            Cat helloKitty = new Cat(); // 클래스는 클래스의 실체인, 인스턴스 객체 new 클래스명()=>생성자 씀
            // Cat클래스에 helloKitty라는 이름의 객체를 생성할게. 
            helloKitty.Name = "헬로키티"; // Cat클래스 안의 Meow(), Run()이 안뜸->기본적으로 private라서 못씀, Public 설정을 해줘야됨
            helloKitty.Color = "흰색";
            helloKitty.Age = 50;
            helloKitty.Meow();
            helloKitty.Run();

            // 객체를 생성하면서 속성 초기화
            Cat nero = new Cat() { // {}중괄호 안에는 속성 초기화
                Name = "검은고양이 네로",
                Color = "검은색",
                Age = 27,
            };
            nero.Meow();
            nero.Run();

            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세입니다.", 
                              helloKitty.Name, helloKitty.Color, helloKitty.Age);
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세입니다.",
                              nero.Name, nero.Color, nero.Age);
           
            // 기본생성자
            Console.WriteLine("=======기본생성자");
            Cat yaongi = new Cat();
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세입니다.",
                             yaongi.Name, yaongi.Color, yaongi.Age);

            Console.WriteLine("=======사용자생성자");
            Cat norangi = new Cat("노랑이", "노란색"); // 색 생략하면 흰색으로 지정해놈
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}세입니다.",
                             norangi.Name, norangi.Color, norangi.Age);
        }
    }
}
