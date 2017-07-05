
using System;

namespace DataPackager
{
    public class GenericVector
    {
        private readonly double[] _vector;

        public int Size => _vector.Length;
        public double this[int x] => _vector[x];

        public GenericVector(params double[] vector)
        {
            _vector = vector;
        }

        public GenericVector(int size)
        {
            _vector = new double[size];
        }

        public static GenericVector Devide(GenericVector a, int devider)
        {
            var deviated = new double[a.Size];

            for (var i = 0; i < a.Size; i++)
            {
                deviated[i] = a[i] / devider;
            }
            
            return new GenericVector(deviated);
        }

        public static GenericVector Sum(GenericVector a, GenericVector b)
        {
            if(a.Size != b.Size)
                throw  new Exception("Vectors need to be of equal size to compute distance.");

            var sum = new double[a.Size];
            
            for (var i = 0; i < a.Size; i++)
            {
                sum[i] = a[i] + b[i];
            }

            return new GenericVector(sum);
        }
        
        public static double Distance(GenericVector a, GenericVector b)
        {
            if(a.Size != b.Size)
                throw  new Exception("Vectors need to be of equal size to compute distance.");

            var differenceSquared = 0.0;
            
            for (var i = 0; i < a.Size; i++)
            {
                differenceSquared += Math.Pow(a[i] - b[i], 2);
            }

            return Math.Sqrt(differenceSquared);
        }

//        public static GenericVector Zero(int size)
//        {
//            var vector = new GenericVector(size);
//
//            for (int i = 0; i < size; i++)
//            {
//                vector[i] = 0;
//            }
//        }
        
    }
}