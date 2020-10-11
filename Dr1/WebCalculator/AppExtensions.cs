using Microsoft.AspNetCore.Builder;

namespace WebCalculator
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseCalculator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CalculatorMiddleware>();
        }
    }
}