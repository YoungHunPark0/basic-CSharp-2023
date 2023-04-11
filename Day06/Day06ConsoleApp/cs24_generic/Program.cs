using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
/*
C#에서 가장 중요한 개념 일반화!
generic 일반화
특수한 개념으로부터 공통된 개념을 찾아 묶는 것
일반화 프로그래밍
일반화의 대상 – 데이터 형식
오버로딩 없이 모든 형식을 지원하는 프로그래밍 패러다임

메소드 일반화 단계
데이터 형식이 사용된 부분을 T 기호로 치환
< >를 이용해 형식을 매개 변수로 넘겨준다.
메소드 호출 시 < > 사이의 T 대신에 형식의 이름 입력

컬렉션은 object 형식에 기반하기 때문에 태생적 성능 문제 내포
일반화 컬렉션으로 해결
🡪컴파일 시 컬렉션에서 사용할 형식 결정
🡪잘못된 형식의 객체를 담게 될 위험 회피
System.Collections.Generic 네임스페이스 -> 중요 실무에서 이거자주씀
List<T>
Queue<T>
Stack<T>
Dictionary<TKey, TValue>

 */

namespace cs24_generic
{
    #region < 일반화 클래스 >
    class MyArray<T> where T : class // where T : class는 사용할 타입은 무조건 클래스 타입이여야 한다.
    {
        T[] array;
    }
    #endregion
    #region < 일반화 클래스 하기 전 >
    // 계속 새로 만들어야하는 단점 => 일반화 하자!
    //class MyArray_float
    //{
    //    float[] array;
    //}

    //class MyArray_double 
    //{
    //    double[] array;
    //}
    #endregion
    internal class Program
    {
        #region < 일반화 메서드 >
        // 너무 많으니 하나로 퉁치자 == 일반화
        static void CopyArray<T>(T[] source, T[] target)
        {
            for (var i = 0; i < source.Length; i++) 
            {
                target[i] = source[i]; // 복사
            }
        }
        #endregion
        #region < 일반화 하기전 >
        //static void CopyArray(long[] source, long[] target)
        //{
        //    for (var i = 0; i < source.Length; i++)
        //    {
        //        target[i] = source[i]; // 복사
        //    }
        //}

        //static void CopyArray(float[] source, float[] target)
        //{
        //    for (var i = 0; i < source.Length; i++)
        //    {
        //        target[i] = source[i]; // 복사
        //    }
        //}

        //static void CopyArray(double[] source, double[] target)
        //{
        //    for (var i = 0; i < source.Length; i++)
        //    {
        //        target[i] = source[i]; // 복사
        //    }
        //}
        //static void CopyArray(string[] source, string[] target)
        //{
        //    for (var i = 0; i < source.Length; i++)
        //    {
        //        target[i] = source[i]; // 복사
        //    }
        //}

        //static void CopyArray(bool[] source, bool[] target)
        //{
        //    for (var i = 0; i < source.Length; i++)
        //    {
        //        target[i] = source[i]; // 복사
        //    }
        //}
        #endregion

        static void Main(string[] args)
        {
            #region < 일반화 시키기 >
            int[] source = { 2, 4, 6, 8, 10 };
            int[] target = new int[source.Length];
            // 배열은 call by reference라 복사됨. <int> 일반화 후 붙이면끝
            CopyArray(source, target); // CopyArray<int>(source, target);와 동일
            foreach (var item in target) 
            {
                Console.WriteLine(item);
            }

            long[] source2 = { 2100000, 2300000, 3300000, 5600000, 7800000 };
            long[] target2 = new long[source2.Length];
            //copyarray가 안되는 건 위에는 int지정했기에 위로 올라가서 copyarary 추가지정(일반화전)
            CopyArray<long>(source2, target2); // <long> 생략해도 무관
            foreach (var item in target2)
            {
                Console.WriteLine(item);
            }

            float[] source3 = { 3.14f, 3.15f, 3.16f, 3.17f, 3.19f };
            float[] target3 = new float[source3.Length];
            // 같은 메소드인데 값만 다른걸 계속 만들어야 하는 단점 ->> 일반화 시켜야함
            CopyArray<float>(source3, target3);
            foreach (var item in target3)
            {
                Console.WriteLine(item);
            }
            #endregion

            // 일반화 List 컬렉션 - 제일 많이 씀
            List<int> list = new List<int>();
            for (var i = 10; i > 0; i--)
            {
                list.Add(i);
            }
            Console.WriteLine("=====일반화 list 컬렉션");
            foreach (var item in list)
            {               
                Console.WriteLine(item);
            }
            Console.WriteLine("=====RemoveAt(3); 7삭제");
            list.RemoveAt(3); // 7삭제

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=====Sort();");
            list.Sort();

            foreach (var item in list)
            { 
                Console.WriteLine(item); 
            }

            // 아래와 같은 일반화 컬렉션을 자주 볼 수 있음
            List<MyArray<string>> myStringArray = new List<MyArray<string>>();

            // 일반화 스택 - 형식 매개 변수 요구
            Stack<float> stFloats = new Stack<float>();
            stFloats.Push(3.15f);
            stFloats.Push(4.28f);
            stFloats.Push(7.24f);

            Console.WriteLine("=====일반화 스택;");
            while (stFloats.Count > 0) 
            {
               Console.WriteLine(stFloats.Pop()); // 기본 stack은 push하면 pop
            }

            // 일반화 Queue - 형식 매개 변수를 요구
            Queue<string> qStrings = new Queue<string>();
            qStrings.Enqueue("Hello");
            qStrings.Enqueue("World");
            qStrings.Enqueue("My");
            qStrings.Enqueue("C#");

            Console.WriteLine("=====일반화 Queue;");
            while (qStrings.Count > 0) 
            {
                Console.WriteLine(qStrings.Dequeue());
            }

            // 일반화 딕셔너리 dictionary<string, ?> - 일반화 list 다음으로 많이 씀
            Dictionary<string, int> dicNumbers = new Dictionary<string, int>();
            dicNumbers["하나"] = 1;
            dicNumbers["둘"] = 2;
            dicNumbers["셋"] = 3;
            dicNumbers["넷"] = 4;

            Console.WriteLine("=====일반화 딕셔너리;");
            Console.WriteLine(dicNumbers["셋"]);
        }
    }
}