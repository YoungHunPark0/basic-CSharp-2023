using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 객체의 멤버에 접근하기전에 널인지 검사
객체 ?. 반환멤버  
객체 ?[배열(컬렉션)의 인덱스]
 */
namespace cs09_nullcondition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Foo myfoo = null; /* new foo();
             myfoo.member = 30;

            int? bar;
            if(myfoo != null) 
            {
                bar = myfoo.member;
            }
            else
            {
                bar = null;
            } 이렇게 하면 너무 기니까
            */ 
            // 위의 9줄을(주석부분) 모두 축약시킴.
            int? bar = myfoo?.member; // myfoo가 null이아니면 그때 그 밑에 있는 member변수에서 값을 집어넣어라
        }
    }
}

class Foo
{
    public int member;
}