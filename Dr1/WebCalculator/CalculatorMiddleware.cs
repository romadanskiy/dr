using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Dr1;

namespace WebCalculator
{
    public class CalculatorMiddleware
    {
        private readonly RequestDelegate _next;

        public CalculatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
		
        public async Task InvokeAsync(HttpContext context, ICalculator calculator)
        {
            var num1 = context.Request.Query["num1"];
            var operation = context.Request.Query["operation"];
            if (operation == "%2b") operation = "+";
            var num2 = context.Request.Query["num2"];
            if (double.TryParse(num1, out var n1) && double.TryParse(num2, out var n2))
            {
                try
                {
                    var res = calculator.Calculate(n1, operation, n2);
                    await context.Response.WriteAsync(res.ToString());
                }
                catch
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
                context.Response.StatusCode = 404;
            
            await _next(context);
        }
    }
}