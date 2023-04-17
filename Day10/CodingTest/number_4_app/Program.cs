using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_4_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> rainbow = new Dictionary<string, string>();
            rainbow["Red"] = "빨간색";
            rainbow["Orange"] = "주황색";
            rainbow["Yellow"] = "노란색";
            rainbow["Green"] = "초록색";
            rainbow["Blue"] = "파란색";
            rainbow["Navy"] = "남색";
            rainbow["Purple"] = "보라색";

            Console.Write("무지개 색은 ");
            foreach (string val in rainbow.Values)
            {
                Console.Write($"{val}, "); 
            }
            Console.WriteLine("입니다.");
            
            Console.WriteLine($"Puple은 {rainbow["Purple"]} 입니다.");
        }
    }
}
