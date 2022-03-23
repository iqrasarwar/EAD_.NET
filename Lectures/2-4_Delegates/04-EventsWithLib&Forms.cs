using System;
using System.Collections;
namespace arrList
{
   delegate void MyEventHandler();
   class arrList : ArrayList
   {
       public event MyEventHandler Added;
       public override int Add(object? value)
       {
           if (Added != null)
               Added();
           return base.Add(value);
       }
   }
   class Program
   {
       static void Main()
       {
           arrList l = new arrList();
           l.Added += () => Console.WriteLine("Add in list");
           l.Add(new object());
       }
   }
}
//windows forms
private void button1_Click(object sender, EventArgs e)
{
    MessageBox.Show("Laiba is a Churail.");
    //form f2 = new form2() ;
}
