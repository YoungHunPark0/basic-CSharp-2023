using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace cs03_object
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Object형식
            // int == System.Int32
            // long == System.Int64
            long idata = 1024; // 클래스이름에 Int64,32등 직접쓰지 않음! (쓰면 흐리게나옴->쓰지말라는것) 
            Console.WriteLine(idata);
            Console.WriteLine(idata.GetType()); // GetType메소드를 쓰면 어떤타입인지 알 수 있음.
            Console.WriteLine("==============");
            // 박싱 : 데이터타입의 값을 Object로 담아라
            object iobj = (object)idata; // object 대문자로 쓰면 흐리게나옴->단순화 하라는소리(alt+enter)
            Console.WriteLine(iobj);
            Console.WriteLine(iobj.GetType());
            Console.WriteLine("==============");
            // 언박싱 : object를 원래 데이터타입으로 바꿔라
            long udata = (long)iobj; // = iobj하면 안들어가서 (long) 써야함
            Console.WriteLine(udata);
            Console.WriteLine(udata.GetType());

            double ddata = 3.141592;
            object pobj = (object)ddata;
            double pdata = (double)pobj;

            Console.WriteLine(pobj);
            Console.WriteLine(pobj.GetType());
            Console.WriteLine(pdata);
            Console.WriteLine(pdata.GetType());

            short sdata = 32000;
            int indata = sdata;
            Console.WriteLine(indata);

            long lndata = long.MaxValue;
            Console.WriteLine(lndata); // 9경
            indata = (int)lndata; // overflow 작은사이즈 데이터에 큰사이즈 데이터타입을 넣을때는 상관없음
            Console.WriteLine(indata); // -1, 큰값을 담지 못해서 -1
            Console.WriteLine("==============");
            // float double 간 형변환
            float fval = 3.141592f; // float형은 마지막에 f 붙여줘야함
            Console.WriteLine("fval = " + fval); // fval = 3.141592
            double dval = (double)fval;
            Console.WriteLine("dval = " + dval); // dval = 3.14159202575684 정밀도가 바뀜
            Console.WriteLine(fval == dval);     // True
            Console.WriteLine(dval == 3.141592); // False

            Console.WriteLine("==============");
            // 정말 중요! 실무에서 쓰임! 문자열을 숫자로, 숫자를 문자열로
            // 문자 🡪 숫자 해결책: Parse(), Convert.ToInt32()
            // 숫자 🡪 문자 해결책: ToString()
            int numival = 1024;
            string strival = numival.ToString();
            //Console.WriteLine(numival==strival); int랑 str랑 비교불가
            Console.WriteLine(strival);
            Console.WriteLine(numival);
            Console.WriteLine(strival.GetType());

            double numdval = 3.14159265358979;
            string strdval = numdval.ToString();
            Console.WriteLine(strdval);
            Console.WriteLine("==============");

            // 문자열을 숫자로
            // 문자->숫자 변환 시, 문자열내에 숫자가 아닌 특수문자나 정수인데 소수점이 있거나 등 예외발생!
            string originstr = "3000000"; // "3million", "3456.7890" 소수점은 예외발생함!
            int convval = Convert.ToInt32(originstr); // int.Parse() 동일
            Console.WriteLine(convval);
            originstr = "1.2345";
            float convfloat = float.Parse(originstr);
            Console.WriteLine(convfloat);
            Console.WriteLine("==============");

            // 예외발생하지 않도록 형변환 방법
            originstr = "123.4f";
            float ffval; 
            // TryParse는 예외가 발생하면 값은 0 대체, 예외가 없으면 원래값으로!
            float.TryParse(originstr, out ffval); // 예외발생하지 않게 숫자변환. 
            Console.WriteLine(ffval); // 0나오는건 Parse 실패해서 0출력
            
            Console.WriteLine("==============");
            // 상수화 const
            const double pi = 3.14159265358979;
            Console.WriteLine(pi);

            //pi = 4.56; // 못쓰는이유는 const 상수화! 못바꿈
        }
    }
} 
