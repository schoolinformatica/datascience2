using System;
using System.Threading;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {            
            var geneticAlgo = new GeneticAlgorithm<MyInt>(new MaximizationFunction(), 100, 10, 0.1, 0.85, true);
            geneticAlgo.Run(); 
        }
    }
}