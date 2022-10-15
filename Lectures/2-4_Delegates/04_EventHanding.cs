using System;

namespace WindowsForm
{
    delegate void MyEventHandler();
    class Button
    {
        public event MyEventHandler click;
        public void onClick()
        {
            if(click != null)
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
            }
             b1.click += FireOnClick;
            b1.onClick();
            b1.FireOnClick();
        }
    }
}
first parameter is the sender
second is the Eventrgs
EventArgs is the biltin class with empty properties we will inherit our class from it.
In publisher class 
1- define event 
2- raise event
maintain list count
each time new object is added so we can't inc the attribute directly instead we have to keep a private vairibale
using System;
using System.Collections;
namespace arrList
{
    delegate void MyEventHandler(object sender, MyArgs e);
    public class MyArgs : EventArgs
    {
        public int Count { get; set; }
        public List<Object> data = new List<object>();
    }
    class arrList : ArrayList
    {
        public event MyEventHandler Added;
        private int  c = 1;
        List<Object> data = new List<object>();
        public void OnAdded(object value)
        {
            MyArgs args = new MyArgs();
            args.Count = c++;
            data.Add(value);
            args.data = data;
            Added(this, args);
        }
        public override int Add(object? value)
        {
            OnAdded(value);
            return base.Add(value);
        }
    }
    
    class Program
    {
        static void Main()
        {
            arrList l = new arrList();
            l.Added += delegate (object sender, MyArgs args)
            {
                Console.WriteLine(args.Count);
                foreach(object obj in args.data)
                {
                    Console.WriteLine(obj);
                }
            };
            l.Add(12);
            l.Add(13);
            l.Add(14);
        }

    }
}
