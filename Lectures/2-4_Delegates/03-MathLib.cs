namespace MathLib
{
    public delegate int Mathop(int x, int y);
    public class MathLibrary
    {
        public int add(int a,int b)
        {
            return a + b;
        }
        public int sub(int a, int b)
        {
            return a - b;
        }
        public Mathop GetFun(int input)
        {
            Mathop op = null;
            if (input == 1)
                op = this.add;
            else if (input == 2)
                op = this.sub;
            return op;
        }
    }
}

using System;
using MathLib;
delegate void MyDelegate();
namespace SemesterVI
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDelegate d1 = delegate {
                Console.WriteLine("delegate in global space");
            };
            d1();
            MathLibrary lib = new MathLibrary();
            Console.WriteLine("enter input");
            int input = int.Parse(Console.ReadLine());
            var d = lib.GetFun(input);
            Console.WriteLine(d(1, 2));
            Console.WriteLine(lib.add(2, 3));
            Console.WriteLine(lib.sub(2, 3));
        }
    }
}

