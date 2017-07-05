using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataPackager;

namespace Kmeans
{
    public class Parser
    {
        public static Dataset Parse()
        {
            var lines = File.ReadAllLines(@"Files/data.csv");
            var vectors = new double[lines.First().Split(',').Length][];
            
            for (var i = 0; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',');
                for (var j = 0; j < columns.Length; j++)
                {
                    if(i == 0)
                        vectors[j] = new double[lines.Length];
                    
                    vectors[j][i] = double.Parse(columns[j]);
                }
            }

            return new Dataset(vectors.Select(x => new GenericVector(x)));
        }
    }
}