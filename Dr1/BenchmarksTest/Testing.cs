using BenchmarkDotNet.Attributes;

namespace BenchmarksTest
{
    public class TestMethods
    {
        public static void StaticCycle(object obj)
        {
            var n = obj.ToString().Length;
            var res = 0;
            for (var i = 0; i < 10; i++)
            {
                res += n;
            }
        }
        
        public void Cycle(object obj)
        {
            var n = obj.ToString().Length;
            var res = 0;
            for (var i = 0; i < 10; i++)
            {
                res += n;
            }
        }
        
        public void Method()
        {
            Cycle("s");
        }

        public virtual void VirtualMethod()
        {
            Cycle("s");
        }
        
        public static void StaticMethod()
        {
            StaticCycle("s");
        }

        public void GenericMethod<T>(T obj)
            where T : TestMethods
        {
            obj.Cycle("s");
        }
        
        public void DynamicMethod(dynamic obj)
        {
            obj.Cycle("s");
        }

        public void ReflectionMethod()
        {
            GetType().GetMethod("Cycle")?.Invoke(new TestMethods(), new object[] {"s"});
        }
    }

    public class Benchmarks
    {
        public TestMethods test = new TestMethods();
        
        [Benchmark(Description = "NonStaticMethod")]
        public void InvokeNonStaticMethod()
        {
            test.Method();
        }
        
        [Benchmark(Description = "VirtualMethod")]
        public void InvokeVirtualMethod()
        {
            test.VirtualMethod();
        }
        
        [Benchmark(Description = "StaticMethod")]
        public void InvokeStaticMethod()
        {
            TestMethods.StaticMethod();
        }
        
        [Benchmark(Description = "GenericMethod")]
        public void InvokeGenericMethod()
        {
            test.GenericMethod(new TestMethods());
        }
        
        [Benchmark(Description = "DynamicMethod")]
        public void InvokeDynamicMethod()
        {
            test.DynamicMethod(test);
        }
        
        [Benchmark(Description = "ReflectionMethod")]
        public void InvokeReflectionMethod()
        {
            test.ReflectionMethod();
        }
    }
}