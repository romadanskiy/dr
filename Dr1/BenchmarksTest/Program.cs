using System;
using BenchmarkDotNet.Running;

namespace BenchmarksTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}