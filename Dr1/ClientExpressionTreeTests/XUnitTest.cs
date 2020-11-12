using System;
using System.Linq.Expressions;
using ClientExpressionTree;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClientExpressionTreeTests
{
    public class XUnitTest
    {
        [Theory]
        [InlineData("1+2")]
        [InlineData("1+2*3")]
        [InlineData("1+2*3-4")]
        [InlineData("1+(2*3-4)/5")]
        [InlineData("(1+2)*3-(4/5)+6")]
        [InlineData("(((((1+2)*3)-4)/5)+6)*7")]
        [InlineData("12*34/56*78")]
        [InlineData("1+(2*34)/5+6*(78-9)")]
        [InlineData("(2+3)/12*7+8*9")]
        [InlineData("356-(22+(1-8)*3)")]
        public void CompiledAndActualResultsAreEqual(string input)
        {
            var fakeClient = GetFakeClient();
            
            var expression = fakeClient.GetExpressionTree(fakeClient.GetRPN(input));
            var compiledResult = Expression.Lambda<Func<double>>(expression).Compile().Invoke();

            var actualResult = fakeClient.GetResult(input);

            Assert.Equal(compiledResult, actualResult);
        }

        [Theory]
        [InlineData("1+1", 2)]
        [InlineData("1+2*3", 7)]
        [InlineData("1+(2*3-4)/5", 1.4)]
        [InlineData("(1+2)*3-(4/5)+6", 14.2)]
        public void ExpressionIsCorrect(string input, double expectedResult)
        {
            var fakeClient = GetFakeClient();
            var expression = fakeClient.GetExpressionTree(fakeClient.GetRPN(input));
            var compiledResult = Expression.Lambda<Func<double>>(expression).Compile().Invoke();
            Assert.Equal(expectedResult, compiledResult);
        }

        [Theory]
        [InlineData("1+1", 2)]
        [InlineData("1+2*3", 7)]
        [InlineData("1+(2*3-4)/5", 1.4)]
        [InlineData("(1+2)*3-(4/5)+6", 14.2)]
        public void ClientGivesCorrectResult(string input, double expectedResult)
        {
            var fakeClient = GetFakeClient();
            var actualResult = fakeClient.GetResult(input);
            Assert.Equal(expectedResult, actualResult);
        }

        private static Client GetFakeClient()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<Dr1.ICalculator, Dr1.Calculator>();
            
            var fakeClient = new ClientExpressionTree.Client(serviceCollection);

            return fakeClient;
        }
    }
}