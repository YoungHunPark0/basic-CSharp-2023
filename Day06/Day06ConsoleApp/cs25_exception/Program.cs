using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs25_exception
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3 };

            try
            {
                for (var i = 0; i < 5; i++)
                {
                    Console.WriteLine(array[i]); // 예외발생 try, catch 사용
                } // 처리되지 않은 예외: System.IndexOutOfRangeException: 인덱스가 배열 범위를 벗어났습니다.
            }
            catch (Exception ex) // dividexception 등등 많음 모르겠다 싶으면 exception
            {
                Console.WriteLine(ex.Message); // ex.message 사용하면 '인덱스가 배열 범위를 벗어났습니다.' 간단하게 나옴 
            }
            finally
            {   
                // finally : 예외가 발생하더라도 무조건 처리햐야 되는 로직
                // file객체 close
                // DB연결 close
                // 네트워크 소켓 close
                Console.WriteLine("계속가요"); 
            }

            try
            {
                DivideTest(array[2], array[0]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("프로그램종료");

            //try
            //{
            //    Console.WriteLine("TEST TEST");
            //    throw new Exception("커스텀 예외!");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
                
        private static void DivideTest(int v1, int v2)
        {   // throw new NotImplementedException(); : 메소드 또는 연산이 구현되지 않았습니다.
            try
            {
                Console.WriteLine(v1 / v2);
            }
            catch
            {   // 현재 메서드에 예외처리하지 않고 메서드를 호출한 곳에서 예외처리한다.
                throw new Exception("DivideTest 메서드에서 예외가 발생 ");
            }       
        }
    }
}
