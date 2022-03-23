using System;
namespace EAD_WF
{
    //delegate point to functions and t can point to multiple functions
    //we can add and remove functions on run time without it we have to specifiy ta compile time.
    delegate void MyDelegate();
    delegate void myDelegate2(int s);
    delegate int myDelegate3(int s);
    delegate int MathOperation(int a, int b);
    delegate object ParamDelegate(params object[] obj);
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
        static int oneParamReturn(int a)
        {
            Console.WriteLine(a);
            return a;
        }
        static int add(int a, int b)
        {
            Console.WriteLine(a+b); 
            return a + b;
        }
        static int sub(int a, int b)
        {
            Console.WriteLine(a-b);
            return a - b;
        }
        static object paramObjFunction(params object[] obj)
        {
            foreach (object obj2 in obj)
                Console.WriteLine(obj2);
            return obj[0];
        }
        static void Main(string[] args)
        {
            //delegate will return the return type/ data of the last refrenced function if delegate
            //is pointing multiple function that are returning something
            MyDelegate d1 = new MyDelegate(display);
            //can add multiple times -> invoke that time
            d1+= display;
            d1 += display;
            d1 += display;
            d1 += display;
            //can remove that is not added
            d1 -= display1;
            d1 -= display1;
            d1 -= display1;
            d1 -= display1;
            d1();

            MyDelegate d2 = new MyDelegate(display1);
            d2 += display1;
            d2.Invoke();
            Console.WriteLine("Second call to delegate ::");
            d2 -= display1;
            d2.Invoke();
            //delegeate.method tell the lastly addded function in delegate
            
            Console.WriteLine(d2.Method);
            //retrun list of delegate presenting names of methods in it
            Console.WriteLine(d2.GetInvocationList());
            Delegate[] data = d2.GetInvocationList();
            foreach (Delegate d in data)
            {
                Console.WriteLine(d.Method);
            }
            d2 = null;
            //d2(); ERROR! Obj REf set to null
            myDelegate2 d3= new myDelegate2(oneParamFunc);
            d3(2);
            myDelegate3 d4 = new myDelegate3(oneParamReturn);
            d4(3);
            d4(4);
            d4(5);
            int returned = d4(6);
            Console.WriteLine(returned); //returning the last one i.e. 6
            MathOperation op = new MathOperation(add);
            op += sub;
            Console.WriteLine("First call to delegate ::");
            Console.WriteLine(op(2, 3));
            op += sub;
            op -= add;
            Console.WriteLine("Second call to delegate ::");
            Console.WriteLine(op(2, 3));
            ParamDelegate dp = new ParamDelegate(paramObjFunction);
            dp(1, "iqra", 2, "vkvf");
            Console.WriteLine(dp(1, "iqra", 2, "vkvf"));
            //USE OF PARARAMS IN DELEGATES

        }
    }
}
