using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace number_5_app
{
    interface IAnimal
    {
        int Age { get; set; }
        string Name { get; set; }

        void Eat();
        void Sleep();
        void Sound();
    }

    class Dog : IAnimal
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private int age;
        public int Age { get { return age; } set { age = value; } }
        public void Eat()
        {
            Console.WriteLine("멍");
        }
        public void Sleep()
        { 
            Console.WriteLine("zzz"); 
        }
        public void Sound() 
        { 
            Console.WriteLine("왕");
        }

    }
    class Cat : IAnimal 
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private int age;
        public int Age { get { return age; } set { age = value; } }
        public void Eat()
        {
            Console.WriteLine("냐옹");
        }
        public void Sleep()
        { 
            Console.WriteLine("zzz"); 
        }
        public void Sound() 
        { 
            Console.WriteLine("냥");
        }
    }
    class Horse : IAnimal
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private int age;
        public int Age { get { return age; } set { age = value; } }
        public void Eat()
        {
            Console.WriteLine("히잉");
        }
        public void Sleep()
        {
            Console.WriteLine("zzz");
        }
        public void Sound()
        {
            Console.WriteLine("힝");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Horse ho = new Horse();
            Dog dog = new Dog();
            Cat cat = new Cat();
            dog.Eat();
            dog.Sleep();
            dog.Sound();
            cat.Eat();
            cat.Sleep();
            cat.Sound();
            ho.Eat();
            ho.Sleep();
            ho.Sound();
        }
    }
}
