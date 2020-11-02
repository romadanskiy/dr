using System.Net.Http;
using Dr1;

namespace ClientExpressionTree
{
    public class WebCalculator : ICalculator
    {
        public double Calculate(double num1, string operation, double num2)
        {
            var client = new HttpClient();
            var request = GetRequest(num1, operation, num2);
            var response = client.GetAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            return double.Parse(result);
        }
        
        private static string GetRequest(double num1, string operation, double num2)
        {
            operation = operation == "+" ? "%2b" : operation;
            return $"http://localhost:5000/?num1={num1}&operation={operation}&num2={num2}";
        }
    }
}