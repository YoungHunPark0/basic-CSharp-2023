using System;

namespace cs04_enums
{
    internal class Program
    {
        // enum 열거 같은형태를 모아논것
        enum HomeTown
        {
            // 순서대로 값이 지정됐지만, 값지정 따로 할 수 있음
            // 열거형은 숫자로 값 지정가능함
            // ctrl+shift+U = 대문자, ctrl+U = 소문자
           SEOUL = 1, 
           DAEJEON = 2,
           DAEGU,
           BUSAN,
           JEJU = 9
        }
        // 열거형으로 만들면 자동으로 나오며, 오타날 일이 적음
        static void Main(string[] args)
        {
            HomeTown myHometown;
            myHometown = HomeTown.BUSAN;

            if (myHometown == HomeTown.SEOUL)
            {
                Console.WriteLine("서울 출신이시군요!");
            }
            else if (myHometown == HomeTown.DAEJEON)
            {
                Console.WriteLine("충청도예요~");
            }
            else if (myHometown == HomeTown.DAEGU)
            {
                Console.WriteLine("대구요~");
            }
            else if (myHometown == HomeTown.BUSAN)
            {
                Console.WriteLine("부산입니다~");
            }
        }
    }
}
