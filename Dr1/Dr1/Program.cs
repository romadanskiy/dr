using System;
using static Dr1.Calculator;
using static Dr1.InputOutput;

namespace Dr1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = InputOutput.GetNumber1();
            var @operator = InputOutput.GetOperator();
            var b = InputOutput.GetNumber2();
            var res = Calculate(a, @operator, b);
            InputOutput.ShowResult(res);
        }
    }
}
