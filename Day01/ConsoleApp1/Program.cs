using System; // include, import 같은것

/// 네임스페이스 ConsoleApp1
namespace ConsoleApp1
{
    /// <summary>
    /// 프로그램 클래스
    /// </summary>
    internal/*public,private 같은느낌*/ class Program//파일명과 클래스명 되도록 같이쓰기
    {
        /// <summary>
        /// 메인 엔트리 포인트
        /// </summary>
        /// <param name="args">콘솔 매개변수</param>
        static/*정적인함수(메소드)*/ void Main(string[] args/*아규먼트(여러개값 받아서처리) 매개변수*/) // 기본틀. 없으면 실행x
        {   //도와주는전구 - alt+enter 
            Console.WriteLine/*콘솔에다가 글자를 출력하는 건 system,namespace,whiteline==print*/("Hello C#!!");
        } // 실행 - 솔루션빌드 - 빌드 성공 - ctrl+f5
    }
}
