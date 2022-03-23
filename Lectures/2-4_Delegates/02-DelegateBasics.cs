using System;
namespace EAD_WF
{
    //delegate point to functions and t can point to multiple functions
    //we can add and remove functions on run time without it we have to specifiy ta compile time.
    delegate void MyDelegate();
    delegate void myDelegate2(int s);
    delegate int myDelegate3(int s);
    delegate int MathOperation(int a, int b);

    class Program
    {
        static void display1()
        {
            Console.WriteLine("aoa pak");
        }

        static void display()
        {
            Console.WriteLine("hello world");
        }
        static void oneParamFunc(int a)
        {
            Console.WriteLine(a);
        }
        static int add(int a, int b)
        {
            return a + b;
        }
        static int sub(int a, int b)
        {
            return a - b;
        }
        static void Main(string[] args)
        {
            MyDelegate d1 = new MyDelegate(display);
            d1();
            MyDelegate d2 = new MyDelegate(display1);
            d2 += display1;
            d2.Invoke();
            d2 -= display;
            d2.Invoke();
            //delegeate.method tell the functions in delegate
            Console.WriteLine(d2.Method);
            Console.WriteLine(d2.GetInvocationList());
            Delegate[] data = d2.GetInvocationList();
            foreach (Delegate d in data)
            {
                Console.WriteLine(d.Method);
            }
            d2 = null;

            //delegate will return the return type/ data of the last refrenced function if delegate is pointing multiple function that are returning something



            //USE OF PARARAMS IN DELEGATES

        }
    }
}
