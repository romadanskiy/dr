using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CalculatorFSharp;
using Microsoft.FSharp.Core;

namespace WebCalculator
{
    public class CalculatorMiddleware
    {
        private readonly RequestDelegate _next;

        public CalculatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
		
        public async Task InvokeAsync(HttpContext context)
        {
            var expression = context.Request.Query["expression"];
            if (!context.Request.Query.ContainsKey("expression"))
            {
                await context.Response.WriteAsync("Set expression!");
            }
            else if (!ValidateExpression(expression))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync($"Expression {expression} is invalid");
            }
            else
            {
                var (num1, operation, num2) = SplitExpression(expression);
                var res = Calculator.calculate(num1, operation, num2);
                if (res == FSharpOption<double>.None)
                    await context.Response.WriteAsync("Cannot be divided by zero");
                else 
                    await context.Response.WriteAsync($"Result: {res.Value}");
                await _next(context);
            }
        }

        private static bool ValidateExpression(string expression)
        {
            var regex = new Regex(@"^\d+[\+\-\*\/]\d+$");
            return regex.IsMatch(expression);
        }

        private static Tuple<double, string, double> SplitExpression(string expression)
        {
            var operations = new[] {'+', '-', '*', '/'};
            var operationIndex = expression.IndexOfAny(operations);
            var nums = expression.Split(operations);
            return Tuple.Create(Convert.ToDouble(nums[0]), expression[operationIndex].ToString(), Convert.ToDouble(nums[1]));
        }
    }
}