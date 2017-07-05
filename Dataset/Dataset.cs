using System;
using System.Collections;
using System.Collections.Generic;

namespace DataPackager
{
    public class Dataset : IEnumerable<GenericVector>
    {
        private readonly IEnumerable<GenericVector> _data;

        public Dataset(IEnumerable<GenericVector> data)
        {
            _data = data;
        }
        
        public IEnumerator<GenericVector> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}