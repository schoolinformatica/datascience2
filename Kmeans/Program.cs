using System;
using Kmeans.Algorithms;

namespace Kmeans
{
    class Program
    {
        static void Main()
        {
            var dataset = Parser.Parse();
            var kmeans = new KMeans(dataset, 4);

            var bestSSE = double.MaxValue;
            
            for (var i = 0; i < 20; i++)
            {
                kmeans.Run();
                var sse = kmeans.GetSSE();
                Console.WriteLine($"SSE: {sse}");
                if (sse < bestSSE)
                    bestSSE = sse;
            }
            Console.WriteLine($"\nBest sse {bestSSE}");

        }
    }
}