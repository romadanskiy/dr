using BenchmarkDotNet.Attributes;

namespace BenchmarksTest
{
   public class MyClass
    {
        public override string ToString()
        {
            return "s";
        }
    }
    
    public class Benchmarks
    {
        public void Cycle(object obj)
        {
            var s = obj.ToString();
            var testString = "";
            for (var i = 0; i < 100; i++)
            {
                testString += s;
            }
        }

        [Benchmark(Description = "PublicMethod")]
        public void InvokePubicMethod()
        {
            Public();
        }
        
        public void Public()
        {
            Cycle("s");
        }

        [Benchmark(Description = "VirtualMethod")]
        public void InvokeVirtualMethod()
        {
            VirtualMethod();
        }
        
        public virtual void VirtualMethod()
        {
            Cycle("s");
        }

        [Benchmark(Description = "StaticMethod")]
        public void InvokeStaticMethod()
        {
            Static();
        }
        
        public static void Static()
        {
            var s = "s";
            var testString = "";
            for (var i = 0; i < 100; i++)
            {
                testString += s;
            }
        }

        [Benchmark(Description = "GenericMethod")]
        public void InvokeGenericMethod()
        {
            Generic(new MyClass());
        }
        
        public void Generic<T>(T s)
            where T : MyClass
        {
            Cycle(s.ToString());
        }

        [Benchmark(Description = "DynamicMethod")]
        public void InvokeDynamicMethod()
        {
            Dynamic(new MyClass());
        }
        
        public void Dynamic(dynamic obj)
        {
            Cycle(obj);
        }

        [Benchmark(Description = "ReflectionMethod")]
        public void InvokeReflectionMethod()
        {
            ReflectionMethod();
        }
        
        public void ReflectionMethod()
        {
            GetType().GetMethod("Cycle").Invoke(new Benchmarks(), new [] {"s"});
        }
    }
}