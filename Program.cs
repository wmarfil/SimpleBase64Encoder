using BenchmarkDotNet.Running;

namespace Base64EncoderSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Base64EncoderBenchmarks>();
        }
    }
}
