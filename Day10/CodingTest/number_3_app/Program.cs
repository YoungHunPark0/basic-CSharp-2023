using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_3_app
{
    class Car
    {
        public string Name;
        public int YearModel;
        public int MaxSpeed;
        public string Color;
        public string UniqueNumber;
        public string Maker;


        internal void Start()
        {
            Console.WriteLine($"{Name}이 시동을 겁니다.");
        }

        internal void Accelerate()
        {
            Console.WriteLine($"최대시속 {MaxSpeed}km/h로 가속합니다.");    
        }

        internal void TurnRight()
        {
            Console.WriteLine($"{Name}를 우회전합니다.");
        }

        internal void Break()
        {
            Console.WriteLine($"{Name}를 브레이크를 밟습니다.");
        }
    }

    class ElectricCar:Car 
    {
        internal void Recharge()
        {
            
        }
    }
    class HybridCar : ElectricCar
    {
        internal void Recharge()
        {
            Console.WriteLine("달리면서 배터리를 충전합니다.");
        }

        
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            HybridCar ioniq = new HybridCar();
            ioniq.Name = "아이오닉";
            ioniq.Maker = "현대자동차";
            ioniq.Color = "White";
            ioniq.YearModel = 2018;
            ioniq.MaxSpeed = 220;
            ioniq.UniqueNumber = "54라 3346";

            ioniq.Start();
            ioniq.Accelerate();
            ioniq.Recharge();
            ioniq.TurnRight();
            ioniq.Break();
        }
    }
}
