using System;

namespace MathLib
{
    public delegate int Mathop(int x, int y);
    public class Class1
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

