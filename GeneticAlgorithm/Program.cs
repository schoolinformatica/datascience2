using System;
using System.Threading;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {            
            var geneticAlgo = new GeneticAlgorithm<MyInt>(new FitnessOral(), 10, 10, 0.1, 0.85, true);
            geneticAlgo.Run(); 
        }
    }
}