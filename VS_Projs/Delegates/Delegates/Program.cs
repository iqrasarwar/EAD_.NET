using System;

namespace WindowsForm
{
    delegate void MyEventHandler();
    class Button //publisher class
    {
        public event MyEventHandler click; //define event
        public void onClick()   //fire event
        {
            if (click != null)
            {
                click(); //syntax to fire event is same as function call
            }
        }
    }
    class myClass
    {
        public void FireOnClick()
        {
            Console.WriteLine("This is fired on click");
        }
    }
    class Event
    {
        static void Main(string[] args)
        {
            Button b1 = new Button();
            //use += always to add function
            b1.click += () =>
            {
                Console.WriteLine("Enent is fired");
            };
            b1.click += delegate ()
            {
                Console.WriteLine("do this also");
            };
            myClass c = new();
            MyEventHandler fire = new(c.FireOnClick);
            b1.click += fire;
            b1.onClick();   
        }
    }
}
//first parameter is the sender
//second is the Eventrgs
//EventArgs is the biltin class with empty properties we will inherit our class from it.
//In publisher class
//1 - define event
// 2- raise event
//maintain list count
// each time new object is added so we can't inc the attribute directly instead we have to keep a private vairibale
//using System;
//using System.Collections;
//namespace arrList
//{
//    delegate void MyEventHandler(object sender, MyArgs e);
//    public class MyArgs : EventArgs
//    {
//        public int Count { get; set; }
//        public List<Object> data = new List<object>();
//    }
//    class arrList : ArrayList
//    {
//        public event MyEventHandler Added;
//        private int c = 1;
//        List<Object> data = new List<object>();
//        public void OnAdded(object value)
//        {
//            MyArgs args = new MyArgs();
//            args.Count = c++;
//            data.Add(value);
//            args.data = data;
//            Added(this, args);
//        }
//        public override int Add(object? value)
//        {
//            OnAdded(value);
//            return base.Add(value);
//        }
//    }

//    class Program
//    {
//        static void Main()
//        {
//            arrList l = new arrList();
//            l.Added += delegate (object sender, MyArgs args)
//            {
//                Console.WriteLine(args.Count);
//                foreach (object obj in args.data)
//                {
//                    Console.WriteLine(obj);
//                }
//            };
//            l.Add(12);
//            l.Add(13);
//            l.Add(14);
//        }

//    }
//}
// Lecture # 03 (02 on delegates)
//annymus func and library 
//using System;
//using MathLib;
//delegate void MyDelegate();
//namespace SemesterVI
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            MyDelegate d1 = delegate {
//                Console.WriteLine("delegate in global space");
//            };
//            d1();
//            MathLibrary lib = new MathLibrary();
//            Console.WriteLine("enter input");
//            int input = int.Parse(Console.ReadLine());
//            var d = lib.GetFun(input);
//            Console.WriteLine(d(1, 2));
//            Console.WriteLine(lib.add(2, 3));
//            Console.WriteLine(lib.sub(2, 3));
//        }
//    }
//}

//using System;
//delegate void MyDelegate();
//namespace SemesterVI
//{
//    delegate void MyDelegate();
//    delegate void myDelegate2(int s);
//    delegate int myDelegate3(int s);
//    delegate int MathOperation(int a, int b);

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            ////ANONYMUS FUNCTIONS
//            ////HAVE 2 TYPES
//            ////will not tell return type in delegates
//            ////ANONYMOUS METHODS
//            MyDelegate d1 = delegate()
//            {
//                int sum = 0;
//                for (int i = 0; i < 10; i++)
//                    sum += i;
//                Console.WriteLine("sum till 10 " +sum);
//            };
//            d1();
//            myDelegate2 d2 = delegate (int s)
//            {
//               int sum = 0;
//               for (int i = 0; i < s; i++)
//                   sum += i;
//               Console.WriteLine("sum till "+s+" " + sum);
//            };
//            d2(5);
//            MathOperation op = delegate (int a, int b)
//            {
//                return a + b;
//            };
//            Console.WriteLine(op(2, 3));
//            myDelegate3 d3 = delegate (int s)
//            {
//               int sum = 0;
//               for (int i = 0; i < s; i++)
//                   sum += i;
//                Console.WriteLine("sum till " + s + " " + sum);
//                return sum;
//            };
//            d3(10);
//            ////LAMBDA STATMENT
//            MyDelegate del = () =>
//            {
//               int sum = 0;
//               for (int i = 0; i < 10; i++)
//                   sum += i;
//               Console.WriteLine("lambda sum till 10 " + sum);
//            };
//            del();
//            myDelegate2 dell = (int a) =>
//            {
//               int sum = 0;
//               for (int i = 0; i < 10; i++)
//                   sum += i;
//               Console.WriteLine("lambda sum till 10 " + sum);
//            };
//            dell(10);
//            myDelegate3 delll = (int a) =>
//            {
//               int sum = 0;
//               for (int i = 0; i < a; i++)
//                   sum += i;
//               Console.WriteLine("lambda sum till 11 " + sum);
//               return sum;
//            };
//            delll += (int a) =>
//            {
//                Console.WriteLine("lambda sum till 11 455");
//                return 70;
//            };
//            Console.WriteLine(delll(11));
//            ////Lambda EXPRESSION
//            myDelegate3 doubleval = (int i) => i * i;
//            Console.WriteLine(doubleval(5));


//            //BUILT IN DLEGATE BY MICRSOFT
//            //ACTION -> FOR VOID
//            //FUNCT -> FOR ANY NONVOID RETURN TYPE

//            // can send 16 paramenters and a void one so total 17

//            Action<int> ad1= (int i) => Console.WriteLine(i);
//            ad1(2);
//            Action<int,int,double> ad = (int i,int b,double c) => Console.WriteLine(i+b+c);
//            ad(1, 2, 3.4);
//            Func<string ,int> fd1 = (string s) => s.Length;
//            Console.WriteLine(fd1("cconole"));
//            Action<int, string> concate = (int a, string str) => Console.WriteLine(a + str);
//            concate(22, "vvvvvvvvvvvvvvv");
//            //NESTED DELGS
//            myDelegate3 deln = delegate (int a)
//            {
//                myDelegate3 delnn = delegate (int b)
//                {
//                    return a + b;
//                };
//                return delnn(27);
//            };
//            Console.WriteLine(deln(3));
//            myDelegate2 den = delegate (int a)
//            {
//                myDelegate3 dlnn = delegate (int b)
//                {
//                    return a + b;
//                };
//                Console.WriteLine(dlnn(3));
//            };
//            den(3);
//        }
//    }
//}
//                                              //LECTURE # 02(1 on delegates)
//using System;
//namespace EAD_WF
//{
//    //delegate point to functions and t can point to multiple functions
//    //we can add and remove functions on run time without it we have to specifiy ta compile time.
//    delegate void MyDelegate();
//    delegate void myDelegate2(int s);
//    delegate int myDelegate3(int s);
//    delegate int MathOperation(int a, int b);
//    delegate object ParamDelegate(params object[] obj);
//    class Program
//    {
//        static void display1()
//        {
//            Console.WriteLine("aoa pak");
//        }

//        static void display()
//        {
//            Console.WriteLine("hello world");
//        }
//        static void oneParamFunc(int a)
//        {
//            Console.WriteLine(a);
//        }
//        static int oneParamReturn(int a)
//        {
//            Console.WriteLine(a);
//            return a;
//        }
//        static int add(int a, int b)
//        {
//            Console.WriteLine(a + b);
//            return a + b;
//        }
//        static int sub(int a, int b)
//        {
//            Console.WriteLine(a - b);
//            return a - b;
//        }
//        static object paramObjFunction(params object[] obj)
//        {
//            foreach (object obj2 in obj)
//                Console.WriteLine(obj2);
//            return obj[0];
//        }
//        static void Main(string[] args)
//        {
//            //delegate will return the return type/ data of the last refrenced function if delegate
//            //is pointing multiple function that are returning something
//            MyDelegate d1 = new MyDelegate(display);
//            //can add multiple times -> invoke that time
//            d1 += display;
//            d1 += display;
//            d1 += display;
//            d1 += display;
//            //can remove that is not added
//            d1 -= display1;
//            d1 -= display1;
//            d1 -= display1;
//            d1 -= display1;
//            d1();

//            MyDelegate d2 = new MyDelegate(display1);
//            d2 += display1;
//            d2.Invoke();
//            Console.WriteLine("Second call to delegate ::");
//            d2 -= display1;
//            d2.Invoke();
//            //delegeate.method tell the lastly addded function in delegate

//            Console.WriteLine(d2.Method);
//            //retrun list of delegate presenting names of methods in it
//            Console.WriteLine(d2.GetInvocationList());
//            Delegate[] data = d2.GetInvocationList();
//            foreach (Delegate d in data)
//            {
//                Console.WriteLine(d.Method);
//            }
//            d2 = null;
//            //d2(); ERROR! Obj REf set to null
//            myDelegate2 d3 = new myDelegate2(oneParamFunc);
//            d3(2);
//            myDelegate3 d4 = new myDelegate3(oneParamReturn);
//            d4(3);
//            d4(4);
//            d4(5);
//            int returned = d4(6);
//            Console.WriteLine(returned); //returning the last one i.e. 6
//            MathOperation op = new MathOperation(add);
//            Console.WriteLine(op.ToString());//gives type eadNet.Mathop same as Console.WriteLine(op.GetType())
//            op += sub;
//            Console.WriteLine("First call to delegate ::");
//            Console.WriteLine(op(2, 3));
//            op += sub;
//            op -= add;
//            Console.WriteLine("Second call to delegate ::");
//            Console.WriteLine(op(2, 3));

//            //USE OF PARARAMS IN DELEGATES
//            ParamDelegate dp = new ParamDelegate(paramObjFunction);
//            dp(1, "iqra", 2, "vkvf");
//            Console.WriteLine(dp(1, "iqra", 2, "vkvf"));

//            Console.WriteLine(op.Target);//gives the class instance and null for static
//        }
//    }
//}