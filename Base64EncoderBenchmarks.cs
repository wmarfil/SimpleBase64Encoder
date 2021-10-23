using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Base64EncoderSimple
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Base64EncoderBenchmarks
    {
        public static IEnumerable<byte[]> Sources()
        {
            yield return Encoding.ASCII.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt " +
                "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea " +
                "commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborumééééééééé");

            var fileName = "Test - Base64 - Rev2.pdf";
            var dir = VisualStudioProvider.TryGetSolutionDirectoryInfo();
            FileInfo file = dir.GetFiles(fileName).Single(f => f.Name == fileName);
            var fileBytes = File.ReadAllBytes(file.FullName);
            var source = new byte[fileBytes.Length + 1]; // to force multiple of 3
            fileBytes.CopyTo(source, 0);
            yield return source;
        }

        [ParamsSource(nameof(Sources))]
        public byte[] Source;


        [Benchmark(Baseline = true)]
        public void EncodeDotnet()
        {
            Convert.ToBase64String(Source);
        }

        [Benchmark]
        public void EncodeSafe()
        {
            Base64.EncodeSafe(Source);
        }

        [Benchmark]
        public void Encode()
        {
            Base64.Encode(Source);
        }

    }
}
