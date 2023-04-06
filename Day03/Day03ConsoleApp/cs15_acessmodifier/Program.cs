using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 public : 클래스 내외부 모든곳에서 접근가능
 provate : 클래스 내부에서만 접근가능
 protected : 클래스 외부에서만 접근가능
 */
namespace cs15_accessmodifier
{
    internal class WaterHeater // class에 기본 접근한정자 internal
    {
        protected int temp; // private 써도 되긴함, public쓰면 접근을 위한 settemp, gettemp 쓸 필요가 없어짐 

        public void SetTemp(int temp)
        {
            if (temp < -5 || temp > 40)
            {
                Console.WriteLine("범위 이탈");
                return;
            }

            this.temp = temp;
        }
        public int GetTemp() 
        {
            return this.temp;
        }

        internal void TurnOnHeater()
        {
            Console.WriteLine("보일러 켭니다 : {0}", temp);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            WaterHeater boiler = new WaterHeater();
            boiler.SetTemp(30);
            Console.WriteLine(boiler.GetTemp());
            boiler.TurnOnHeater();
           // boiler.SetTemp = 38; protected는 접근불가함
        }
    }
}
