using System;
using static Dr1.InputOutput;

namespace Dr1
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            var num1 = GetNumber();
            var operation = GetOperator();
            var num2 = GetNumber();
            var res = calculator.Calculate(num1, operation, num2);
            ShowResult(res);
        }
    }
}
