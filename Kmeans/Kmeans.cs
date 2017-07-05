using System;
using System.Collections.Generic;
using System.Linq;
using DataPackager;
using Kmeans.Adapter;

namespace Kmeans.Algorithms
{
    public class KMeans
    {
        private readonly int _k;
        private readonly int _maxIterations;
        private readonly int _dimensions;
        private readonly KmeansAdapter[] _data;
        private readonly Random _random;
        private Dictionary<int, GenericVector> _centroids;


        public KMeans(IEnumerable<GenericVector> data, int k, int maxIterations = 100)
        {
            _k = k;
            _maxIterations = maxIterations;
            _dimensions = data.First().Size;
            _data = ToKmeansAdapter(data);
            _random = new Random();
        }

        public void Run()
        {
            _centroids = GenerateCentroids();

            for (var i = 0; i < _maxIterations; i++)
            {
                UpdateClusters();
                UpdateCentroids();
            }

        }

        public double GetSSE()
        {
            var sse = 0.0;

            foreach (var adapter in _data)
            {
                sse += Math.Pow(GenericVector.Distance(adapter.Vector, _centroids[adapter.Cluster]), 2);
            }

            return sse;
        }

        private void UpdateClusters()
        {
            foreach (var adapter in _data)
            {
                adapter.Cluster = GetNearestCentroid(adapter);
            }
        }

        private void UpdateCentroids()
        {
            var clusters = _data.GroupBy(x => x.Cluster);
            foreach (var cluster in clusters)
            {
                var clusterId = cluster.Key;
                var vectorsSum = new GenericVector(_dimensions);
                var clusterSize = 0;
                foreach (var vector in cluster)
                {
                    vectorsSum = GenericVector.Sum(vectorsSum, vector.Vector);
                    clusterSize++;
                }

                _centroids[clusterId] = GenericVector.Devide(vectorsSum, clusterSize);
            }
        }

        private int GetNearestCentroid(KmeansAdapter adapter)
        {
            var closestCentroid = (int)Centroid.NotInitialized;
            var closestDistance = double.MaxValue;

            foreach (var centroid in _centroids)
            {
                var distance = GenericVector.Distance(adapter.Vector, centroid.Value);
                
                if (distance < closestDistance)
                {
                    closestCentroid = centroid.Key;
                    closestDistance = distance;
                }
            }

            return closestCentroid;
        }
        
        private Dictionary<int, GenericVector> GenerateCentroids()
        {
            var centroids = new Dictionary<int, GenericVector>();

            for (var i = 0; i < _k; i++)
            {
                centroids.Add(i, GetRandomPointInDataset());
            }

            return centroids;
        }

        private GenericVector GetRandomPointInDataset()
        {
            var random = _random.Next(0, _data.Length);
            return _data[random].Vector;
        }

        private static KmeansAdapter[] ToKmeansAdapter(IEnumerable<GenericVector> data)
        {
            var index = 0;
            var adapterArray = new KmeansAdapter[data.Count()];
            
            foreach (var vector in data)
            {
                adapterArray[index++] = new KmeansAdapter(vector);
            }

            return adapterArray;
        }
    }
}