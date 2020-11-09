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
            var num1 = context.Request.Query["num1"];
            var operation = context.Request.Query["operation"];
            if (operation == "%2b") operation = "+";
            var num2 = context.Request.Query["num2"];
            if (Double.TryParse(num1, out var n1) && Double.TryParse(num2, out var n2))
            {
                var res = Calculator.calculate(n1, operation, n2);
                if (res != FSharpOption<double>.None)
                    await context.Response.WriteAsync(res.Value.ToString());
                else
                    context.Response.StatusCode = 404;
            }
            else
                context.Response.StatusCode = 404;
            await _next(context);
        }
    }
}