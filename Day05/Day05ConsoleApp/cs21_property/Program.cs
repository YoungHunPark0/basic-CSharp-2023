using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
// 멤버변수, 프로퍼티
namespace cs21_property
{
    class Boiler
    {// 보일러 온도. private로 바꾸는 순간 main에서 temp를 못씀
        private int temp; // 멤버변수.
                          // public 안쓰는 이유는 보일러가 30만 등 int정수제한이
                          // 없으니 데이터오염방지 제한걸려고-> get,set 메소드 만듬
        
        public int Temp // 프로퍼티(속성)
        {
            get { return temp; } // 밑에 public int GetTemp() { return this.temp; } 와 같음
            set
            {
                if (value <= 10 || value >= 70)
                {
                    temp = 10; // 제일 낮은 온도로 변경설정
                }
                else
                {
                    temp = value;
                }
            }    
        }


        // 위의 get; set;과 비교 // 아래의 Get메서드와 Set메서드는 Java에서만 사용, C#은 거의 안씀(프로퍼티씀)
        public void SetTemp(int temp) // 
        {
            if (temp <= 10 || temp > 70)
            {
                //Console.WriteLine("수온설정값이 너무 낮거나 높습니다. 10~70도 사이로 지정해주세요");
                //return; 
                this.temp = 10;
            }
            else
            { 
                this.temp = temp;
            }
        }

        public int GetTemp() { return this.temp; }
    }

    class Car
    {
        int year;
        string fuelType;
        string tireType;
        string company;
        int price;
        string carIdNumber;
        string carPlateNumber; // 10개항목 드래그해서 alt+enter 캡슐화 하면 밑에 나옴
        // -> 프로퍼티. 자동으로 나온것들 중 잘못된 값 있으면 수정
        // 프로퍼티 : get, set 작업함

        
        public string Name { get; set; } // 필터링이 필요없으면 멤버변수 없이 프로퍼티로 대체
        
        public string Color { get; set; }
        // string color만 쓰면 밑에서 못씀

        // 들어오는 데이터를 필터링할 때는 private멤버변수와 public 프로퍼티를 둘다 사용
        public int Year {
            get { return year; } // get => year; 람다식
            set
            {
                if (value <= 1990 || value >= 2023)
                {
                    value = 2023;
                }
                year = value;
            }
        }
        public string FuelType { get => fuelType;
            set
            {
                if (value != "휘발유" || value != "경유")
                {
                    value = "휘발유";
                }
                fuelType = value;
            }
        }

        private int door;

        public int Door { 
            get { return door; }
            set
            {
                if (value != 2 || value != 4)
                {
                    { value = 4; }
                }
                door = value;
            }
        }
        public string TireType { get; set; }
        public string Company { get; set; }
        public int Price { get => price; set => price = value; }
        public string CarIdNumber { get; set; }
        public string CarPlateNumber { get; set; }
    }

    interface IProduct
    {
        string ProductName { get; set; }

        void Produce();
    }

    class MyProduct : IProduct // 인터페이스를 상환하기론 약속을 지켜라
    {
        private string productName;
        public string ProductName { 
            get { return productName; }
            set { productName = value; } 
        }

        public void Produce() 
        {
            Console.WriteLine("{0}을 생산합니다.", ProductName);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Boiler kitturami = new Boiler();
            //kitturami.temp = 60; // 60도

            ////
            //kitturami.temp = 300000; // 귀뚜라미 보일러 물수온이 30만도? 증발->말이안됨 -> 데이터오염
            //kitturami.temp = -120; // -120도 -> 말안됨->데이터오염->get, set을 만듬
            kitturami.SetTemp(50);
            Console.WriteLine(kitturami.GetTemp()); // 옛날방식

            Boiler navien = new Boiler();
            navien.Temp = 5000;
            Console.WriteLine(navien.Temp); 

            Car ionic = new Car();
            ionic.Name = "아이오닉"; // set을 지우면 setting이 안돼서 값입력 불가
            Console.WriteLine(ionic.Name);

            // 생성할 때 프로퍼티로 초기화
            Car genesis = new Car() { // 뭐넣을지 모르겠다 == ctrl+space  프로퍼티는 벤찌모양
                Name = "제네시스",
                Company = "현대자동차",
                FuelType = "휘발류",
                Color = "흰색",
                Door = 4,
                TireType = "광폭타이어",
                Year = 0, // 0넣으면 2023 나오게 위에서 설정해놈
            };

            Console.WriteLine("자동차 제조회사는 {0}", genesis.Company);
            Console.WriteLine("자동차 제조년도는 {0}년", genesis.Year);
        }
    }
}
