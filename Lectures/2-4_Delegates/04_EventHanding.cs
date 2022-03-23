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
