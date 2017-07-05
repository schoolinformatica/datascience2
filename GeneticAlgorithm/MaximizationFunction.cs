using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GeneticAlgorithm
{
    public class MaximizationFunction : IGeneticFunctions<MyInt>
    {
        private readonly Random _r = new Random();

        public double ComputeFitness(MyInt individual)
        {
            return -(Math.Pow(individual.AsInt(), 2)) + 7 * individual.AsInt();
        }

        public MyInt CreateIndividual()
        {
            return new MyInt(_r.Next(0, 31));
        }

        public MyInt Mutate(MyInt individual, double mutationRate)
        {
            if (_r.NextDouble() < mutationRate)
            {
                var randomBit = _r.Next(0, 4);
                var index = 1 << randomBit;
                individual &= ~index;
            }
            return individual;
        }

        public Tuple<MyInt, MyInt> SelectParents(MyInt[] individuals, double[] fitnesses)
        {
            var minFitness = fitnesses.Min() + 1;
            var totalFitness = 0.0;
            var individualsWithFitness = new List<Tuple<MyInt, double>>();
            var parents = new List<MyInt>();

            for (var i = 0; i < fitnesses.Length; i++)
            {
                var fitness = fitnesses[i] + minFitness;
                totalFitness += fitness;
                individualsWithFitness.Add(new Tuple<MyInt, double>(individuals[i], fitness));
            }

            var index = 0;
            while (parents.Count < 2)
            {
                var chance = _r.NextDouble();

                if (chance < individualsWithFitness[index].Item2 / totalFitness)
                {
                    parents.Add(individualsWithFitness[index++].Item1);
                }

                if (index + 1 == parents.Count)
                    index = 0;
            }

            return new Tuple<MyInt, MyInt>(parents[0], parents[1]);
         }

        public Tuple<MyInt, MyInt> Crossover(Tuple<MyInt, MyInt> parents)
        {
            var index = _r.Next(1, 4);
            var leftA = ((parents.Item1 >> index) & 0x1F) << index;
            var rightA = ((parents.Item1 <<  0x5 - index) & 0x1F) >> 0x5 - index;
            var leftB = ((parents.Item2 >> index) & 0x1F) << index;
            var rightB = ((parents.Item2 << 0x5 - index) & 0x1F) >> 0x5 - index;
            return new Tuple<MyInt, MyInt>(leftA | rightB, rightA | leftB);
        }
    }
}