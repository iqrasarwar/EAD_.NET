using System;
using MathLib;
delegate void MyDelegate();
namespace SemesterVI
{
    delegate void MyDelegate();
    delegate void myDelegate2(int s);
    delegate int myDelegate3(int s);
    delegate int MathOperation(int a, int b);

    class Program
    {
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
               Console.WriteLine("sum till "+s+" " + sum);
            };
            d2(5);
            MathOperation op = delegate (int a, int b)
            {
                return a + b;
            };
            Console.WriteLine(op(2, 3));
            myDelegate3 d3 = delegate (int s)
            {
               int sum = 0;
               for (int i = 0; i < s; i++)
                   sum += i;
                Console.WriteLine("sum till " + s + " " + sum);
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
               for (int i = 0; i < a; i++)
                   sum += i;
               Console.WriteLine("lambda sum till 11 " + sum);
               return sum;
            };
            delll += (int a) =>
            {
                Console.WriteLine("lambda sum till 11 455");
                return 70;
            };
            Console.WriteLine(delll(11));
            ////Lambda EXPRESSION
            myDelegate3 doubleval = (int i) => i * i;
            Console.WriteLine(doubleval(5));


            //BUILT IN DLEGATE BY MICRSOFT
            //ACTION -> FOR VOID
            //FUNCT -> FOR ANY NONVOID RETURN TYPE

            // can send 16 paramenters and a void one so total 17

            Action<int> ad1= (int i) => Console.WriteLine(i);
            ad1(2);
            Action<int,int,double> ad = (int i,int b,double c) => Console.WriteLine(i+b+c);
            ad(1, 2, 3.4);
            Func<string ,int> fd1 = (string s) => s.Length;
            Console.WriteLine(fd1("cconole"));
            Action<int, string> concate = (int a, string str) => Console.WriteLine(a + str);
            concate(22, "vvvvvvvvvvvvvvv");
            //NESTED DELGS
            myDelegate3 deln = delegate (int a)
            {
                myDelegate3 delnn = delegate (int b)
                {
                    return a + b;
                };
                return delnn(27);
            };
            Console.WriteLine(deln(3));
            myDelegate2 den = delegate (int a)
            {
                myDelegate3 dlnn = delegate (int b)
                {
                    return a + b;
                };
                Console.WriteLine(dlnn(3));
            };
            den(3);
        }
    }
}
