using System;
using System.Collections.Generic;
using System.IO; // Directory 쓰면 켜짐
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs29_filehandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Directory == Folder
            // "C:\\Dev" == @"C:\Dev" -> 리터럴 : 여러줄 문자열 가능
            string curDirectory = @"C:\Temp"; // "."==현재 디렉토리(상대경로), ".." == 부모 디렉터리,
                                             // \(1개)는 특수문자 취급 쓸꺼면, \\ 두개써야함
                                             // 아니면 리터럴 @ 써야함
                                             // C:\\Dev 는 절대경로.

            Console.WriteLine("현재 디렉토리 정보");
            
            var dirs = Directory.GetDirectories(curDirectory); // 현재디렉토리에 모든 디렉토리를 불러오겠다. system IO켜짐
            foreach (var dir in dirs) 
            {
                var dirInfo = new DirectoryInfo(dir);

                Console.Write(dirInfo.Name); // 그냥 쓰는것. writeline은 쓰고 줄바꿔줌
                Console.WriteLine(" [{0}]", dirInfo.Attributes); // 하위 디렉토리 정보를 봄.
            }

            Console.WriteLine("현재 디렉토리 파일정보");

            var files = Directory.GetFiles(curDirectory);
            foreach(var file in files)
            {
                var fileInfo = new FileInfo(file);

                Console.Write(fileInfo.Name);
                Console.WriteLine(" [{0}]", fileInfo.Attributes);
            }

            // 특정 경로에 하위폴더/하위파일 조회 ~~!! 실무에서 많이 쓰이니 알아두기
            string path = @"C:\Temp\Csharp_Bank2"; //C:\Temp\Csharp_Bank폴더를 만들겠다. 만들고자 하는 폴더
                                                   // 구분자 /도 가능
            string sfile = "Test2.log"; // 생성할 파일

            if (Directory.Exists(path)) // 이런 디렉토리가 있으면
            {                           //거기에 test.log를 바로 만들겠다
                Console.WriteLine("경로가 존재하여 파일을 생성합니다.");
                File.Create(path + @"\" + sfile);  // C:\Temp\Csharp_Bank\Test.log                  
            }
            else
            {
                Console.WriteLine($"해당경로가 없습니다 {path}, 만듭니다.");
                Directory.CreateDirectory(path);

                Console.WriteLine("경로를 생성하여 파일을 생성합니다.");
                File.Create(path + @"\" + sfile);
            }
        }
    }
}
