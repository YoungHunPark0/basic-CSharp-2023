using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_2_App
{
    class Boiler
    {
        public string Brand;
        private int voltage;
        private int temperature;

        public int Voltage
        {
            get { return this.voltage; }
            set
            {
                // 110v 220v만 저장
                if (value != 110 && value !=220 )
                {
                    value = 0;
                }
                else 
                {
                    voltage = value;
                }
            }
        }
        public int Temperature
        {
            get
            {
                return this.temperature;
            }
            set
            {
                // 물온도는 5도 이하면 5도, 70도 이상이면 70도 제한
                if ( value <= 5 ) 
                {
                    value = 5;
                    temperature = value;
                }
                else if ( value >= 70)
                {
                    value = 70;
                    temperature = value;
                }
                else
                {
                    temperature = value; 
                }
            }
        }

        public void PrintAll()
        {
            Console.WriteLine("Brand = {0}, Voltage = {1}, Temperature = {2}", Brand, Voltage, Temperature);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Boiler kitturami = new Boiler { Brand = "귀뚜라미", Voltage = 230, Temperature =70 };
            kitturami.PrintAll();
        }
    }
}