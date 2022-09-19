using System.Threading;
using System.Diagnostics;
namespace ASYNC
{
   class Program
   {
      static void Main(string[] args)
      {
         var timer = Stopwatch.StartNew();
         Console.WriteLine("Hello World!");
         // A();
         // B();
         // C();
         Task a = asynA();
         Task b = asyncB();
         Task c = asynC();
         Task[] tasks = { a, b, c };
         Task.WaitAll(tasks);
         Console.Write(timer.ElapsedMilliseconds);
      }

      static void A()
      {
         Console.WriteLine("Before A");
         Thread.Sleep(3000);
         Console.WriteLine("After A");
      }
      static void B()
      {
         Console.WriteLine("Before B");
         Thread.Sleep(3000);
         Console.WriteLine("After B");
      }
      static void C()
      {
         Console.WriteLine("Before C");
         Thread.Sleep(3000);
         Console.WriteLine("After C");
      }

      static async Task asynA()
      {
         Console.WriteLine("Before A");
         await Task.Delay(3000);
         Console.WriteLine("After A");
      }
      static async Task asyncB()
      {
         Console.WriteLine("Before b");
         await Task.Delay(3000);
         Console.WriteLine("After b");
      }
      static async Task asynC()
      {
         Console.WriteLine("Before c");
         await Task.Delay(3000);
         Console.WriteLine("After c");
      }
   }
}
