using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 foreach 구문은 IEnumerable 과 IEnumerator를 구현(상속=클래스만)하는 형식만 지원
IEnumerable의 유일한 메소드 - GetEnumerator( )
구현 시 yield return 문 필요 - yield(도출하다, 산출하다)는 실무에서 거의 안씀
IEnumerator 형식의 객체 반환
 */
namespace cs23_collection
{
    class CustomEnumerator : IEnumerable // foreach를 쓸수 있는 객체로 만들래
    {
        public int[] list = { 1, 3, 5, 7, 9 };

        public IEnumerator GetEnumerator() // getenumerator가 있기에 foreach를 사용할 수 있음
        {   // yield가 어떻게 쓰이는지 예시 - 무식하게 하는법이라 추천안함
            yield return list[0]; // 메서드를 빠져나가지 않고 값만 돌려줌
            yield return list[1]; // return과 달리 호출값을 보내주고 멈춰있음
            yield return list[2];
            yield return list[3];
            //    yield break;
            yield return list[4]; // 브레이크 만나서 접근못함 9출력 안됨
        }

        //public IEnumerator GetEnumerator() // getenumerator가 있기에 foreach를 사용할 수 있음
        //{
        //    yield return list[0]; // 메서드를 빠져나가지 않고 값만 돌려줌
        //    yield return list[1]; // return과 달리 호출값을 보내주고 멈춰있음
        //    yield return list[2];
        //    yield return list[3];
        //    yield break;
        //    yield return list[4]; // 브레이크 만나서 접근못함 9출력 안됨

        //}
    }
    
    class MyArrayList : IEnumerator, IEnumerable
    {
        int[] array; // 배열값 집어넣는 곳
        int position = -1; // 인덱스

        public MyArrayList()
        {
            array = new int[3]; // 기본크기 3으로 초기화
        }
        // 인덱서 프로퍼티
        public int this[int index] 
        {
            get { return array[index]; }
            set 
            { 
                if (index>= array.Length)
                {
                    Array.Resize<int>(ref array, index + 1);
                    Console.WriteLine("MyArrayList Resize : {0}", array.Length); // 개발완료후 주석처리 해야함
                }
                array[index] = value;
            }
        }

        #region < IEnumerable 인터페이스 구현 >
        public IEnumerator GetEnumerator()
        {
            for (var i =0; i < array.Length; i++) 
            {
                yield return array[i];
            }
        }
        #endregion

        #region < IEnumerator 인터페이스 구현 >
        public object Current
        {
            get
            {
                return array[position];
            }
        }

        public bool MoveNext()
        {
            if (position == array.Length -1)
            {
                Reset();
                return false;
            }

            position++;
            return (position < array.Length);
        }

        public void Reset()
        {
            position = -1;
        }
        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var obj = new CustomEnumerator(); // var == 뭐든 쓰겠다.

            foreach (var item in obj) 
            {
                Console.WriteLine(item);
            }

            var myArrayList = new MyArrayList();
            for (var i = 0; i <= 5;  i++) //i<=5 6번
            {   
                // Indexer 인덱서 프로퍼티를 만들었기 때문에 사용가능
                myArrayList[i] = i; 
            }

            // IEnumerable 인터페이스를 구현했기 때문에 가능
            foreach (var item in myArrayList) 
            {
                Console.WriteLine(item); 
            }
        }
    }
}
