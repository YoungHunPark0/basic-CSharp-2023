using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs19_Interface
{
    interface ILogger
    {
        void WriteLog(string log);
    }

    interface IFormattableLogger : ILogger //  IFormattableLogger는 Ilogger도 구현하고  IFormattableLogger도 구현
    {
        void WriteLog(string format, params object[] args); // args 다중 파라미터
    }

    class ConsoleLogger : ILogger // 부모클래스 상속이랑 같은형태 
                                  // 인터페이스를 상속하는건 "구현"이라고함(implement)
     // 클래스 상속과 다르게 ILogger에 빨간색 밑줄 있음 consolelogger에 writelog를 안썻기에
    {
        public void WriteLog(string log)
        {
            Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), log);
        }
    }

    class ConsoleLogeer2 : IFormattableLogger
    {
        public void WriteLog(string format, params object[] args) // IFormattableLogger의 writelog
        {
            string message = string.Format(format, args);
            Console.WriteLine("{0}, {1}", DateTime.Now.ToLocalTime(), message);
        }

        public void WriteLog(string log) // ILogger의 writelog
        {
            throw new NotImplementedException();
        }
    }

    class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public void Stop() 
        {
            Console.WriteLine("정지");
        }
    }

    interface IHoverable
    {
        void Hover(); // 물에서 달린다
    }

    interface IFlyable
    {
        void Fly(); // 날다
    }

    // C#에는 다중상속이 없는 대신 이용하는 방식
    class FlyHoverCar : Car, IFlyable, IHoverable// ConsoleLogger 이미 상속 하고있기에
    {
        public void Fly()
        {
            Console.WriteLine("납니다.");
        }

        public void Hover()
        {
            Console.WriteLine("물에서 달립니다.");
        }
    }

    internal class Program
    {
        
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger(); // 인터페이스는 생성 x new안씀, new는 클래스에만 붙임
            logger.WriteLog("안녕~!!");

            IFormattableLogger logger2 = new ConsoleLogeer2();
            logger2.WriteLog("{0} * {1} = {2}", 6, 5, (6 * 5));
        }
    }
}
