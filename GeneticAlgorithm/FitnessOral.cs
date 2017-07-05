using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace GeneticAlgorithm
{
    public class FitnessOral : IGeneticFunctions<MyInt>
    {
       private Random _r = new Random();

        public double ComputeFitness(MyInt individual)
        {
            var e = individual & 0x7;
            var d = (individual >> 3) & 0x7;
            var c = (individual >> 6) & 0x7;
            var b = (individual >> 9) & 0x7;
            var a = (individual >> 12) & 0x7;
            Console.WriteLine($"A {a}, B {b}, C {c}, D {d}, E {e}");
            if (a.AsInt() + b.AsInt() == 0)
                return -1000;
            var devision = a.AsInt() / ((a.AsInt() + b.AsInt()) * (c.AsInt() + 1.0));
            var fitness = 1.0 / (1.0 + (devision + e.AsInt()) - 13.0);
            return fitness;
        }

        public MyInt CreateIndividual()
        {
//            return new MyInt(_r.Next(0, 32767));
            return new MyInt(574);
        }

        public MyInt Mutate(MyInt individual, double mutationRate)
        {
            if (_r.NextDouble() < mutationRate)
            {
                var randomBit = _r.Next(0, 14);
                var index = 1 << randomBit;
                individual &= ~index;
            }
            return individual;
        }

        public Tuple<MyInt, MyInt> SelectParents(MyInt[] individuals, double[] fitnesses)
        {
            var minFitness = Math.Abs(fitnesses.Min()) + 1;
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
            var index = _r.Next(1, 14);
//            index = 9;
//            Console.WriteLine($"Parents {parents.Item1}, {parents.Item2}");
            var leftA = ((parents.Item1 >> index) & 32767) << index;
            var rightA = ((parents.Item1 <<  0xF - index) & 32767) >> 0xF - index;
            var leftB = ((parents.Item2 >> index) & 32767) << index;
            var rightB = ((parents.Item2 << 0xF - index) & 32767) >> 0xF - index;
//            Console.WriteLine($"LeftA {leftA} RightA {rightA} LeftB {leftB} RightB {rightB}");
//            Console.WriteLine($"Children: {leftA | rightB}, {rightA | leftB}");
            return new Tuple<MyInt, MyInt>(leftA | rightB, rightA | leftB);
        }
    }
}