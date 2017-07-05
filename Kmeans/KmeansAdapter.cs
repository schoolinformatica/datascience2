
using DataPackager;

namespace Kmeans.Adapter
{
    public class KmeansAdapter
    {
        public GenericVector Vector;
        public int Cluster;

        public KmeansAdapter(GenericVector vector)
        {
            Vector = vector;
        }

    }
}