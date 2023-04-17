using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 폴더만들고 - 추가 - 클래스 - helpers라는 네임스페이스 생성
namespace wf13_bookrentalshop.Helpers
{
    internal class Commons
    {                 // readonly 읽는순간 수정불가
        // 모든 프로그램상에서 다 사용가능
        // static은 하나 만들어서 모든 프로세스에 적용 할 때 => 효율적
        // DB연결문자열은 여기서만 수정하면 됨
        //== "Server=localhost;Port=3306;Database=bookrentalshop;Uid=root;Pwd=12345";
        //;마다 " + => 수정하기 편하게 하기위해서
        public static readonly string ConnString = "Server=localhost;" +
                                                   "Port=3306;" +
                                                   "Database=bookrentalshop;" +
                                                   "Uid=root;" +
                                                   "Pwd=12345";

        // 로그인 사용자 아이디 저장변수 
        // 프로그램 전체에서 이 데이터를 공유
        public static string LoginID = string.Empty;
    }
}
