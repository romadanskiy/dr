using Microsoft.AspNetCore.Builder;

namespace WebCalculator
{
    public static class AppExtensions
    {
        public static void UseCalculator(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CalculatorMiddleware>();
        }
    }
}