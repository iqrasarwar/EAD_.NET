using System;
using MathLib;
delegate void MyDelegate();


namespace SemesterVI
{
    //delegate point to functions and t can point to multiple functions
    //we can add and remove functions on run time without it we have to specifiy ta compile time.
    //
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
        static int add(int a , int b)
        {
            return a + b;
        }
        static int sub(int a, int b)
        {
            return a - b;
        }
        static void Main(string[] args)
        {
            ////ANONYMUS FUNCTIONS
            ////HAVE 2 TYPES
            ////will not tell return type in delegates
            ////ANONYMOUS METHODS
            MyDelegate d1 = delegate()
            {
                int sum = 0;
                for (int i = 0; i < 10; i++)
                    sum += i;
                Console.WriteLine("sum till 10 " +sum);
            };
            d1();
            myDelegate2 d2 = delegate (int s)
            {
               int sum = 0;
               for (int i = 0; i < s; i++)
                   sum += i;
               Console.WriteLine("sum till a " + sum);
            };
            d2(5);
            myDelegate3 d3 = delegate (int s)
            {
               int sum = 0;
               for (int i = 0; i < s; i++)
                   sum += i;
               Console.WriteLine("sum till a returns " + sum);
               return sum;
            };
            d3(10);
            ////LAMBDA STATMENT
            MyDelegate del = () =>
            {
               int sum = 0;
               for (int i = 0; i < 10; i++)
                   sum += i;
               Console.WriteLine("lambda sum till 10 " + sum);
            };
            del();
            myDelegate2 dell = (int a) =>
            {
               int sum = 0;
               for (int i = 0; i < 10; i++)
                   sum += i;
               Console.WriteLine("lambda sum till 10 " + sum);
            };
            dell(10);
            myDelegate3 delll = (int a) =>
            {
               int sum = 0;
               for (int i = 0; i < 10; i++)
                   sum += i;
               Console.WriteLine("lambda sum till 10 " + sum);
               return sum;
            };
            delll(10);
            ////Lambda EXPRESSION
            myDelegate3 doubleval = (int i) => i * i;
            Console.WriteLine(doubleval(5));
            //BUILT IN DLEGATE BY MICRSOFT
            //ACTION -> FOR VOID
            //FUNCT -> FOR ANY NONVOID RETURN TYPE

            // can send 16 paramenters and a void one so total 17

            Action<int> d1 = (int i) => i+4;
            d1(2);
            Func<string ,int> d2 = (string s) => s.Length;
            Console.WriteLine(d2("cconolefjsklh"));
            MathOperation op = delegate (int a, int b)
            {
               return a + b;
            };
            Console.WriteLine(op(2, 3));
            Class1 lib = new Class1();
            Console.WriteLine("enter input");
            int input = (int.Parse(Console.ReadLine()));
            var d = lib.GetFun(input);
            Console.Write(d(1,2));
            display();

            d1.Invoke();
            d1();

            MyDelegate d2 = new MyDelegate(display);
            d2 += display1;
            d2.Invoke();
            //d2 -= display;
            d2.Invoke();
            //delegeate.method tell the functions in delegate
            Console.WriteLine(d2.Method);
            Console.WriteLine(d2.GetInvocationList());
            Delegate[] data = d2.GetInvocationList();
            foreach(Delegate d in data)
            {
               Console.WriteLine(d.Method);
            }
            d2 = null;

            //delegate will return the return type/ data of the last refrenced function if delegate is pointing multiple function that are returning something

            MathOperation add = op;
            var data = add(2, 3);
            Console.WriteLine(data);

            //MultiCasting

            MathOperation add_ = add;
            add_ += sub;
            var data = add_(2, 3);
            Console.WriteLine(data);

            //USE OF PARARAMS IN DELEGATES

        }
    }
}
