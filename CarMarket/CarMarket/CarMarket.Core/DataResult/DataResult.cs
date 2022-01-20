using System.Collections.Generic;

namespace CarMarket.Core.DataResult
{
    public class DataResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
    }
}
