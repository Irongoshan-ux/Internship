using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Exceptions
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException() : base()
        {
        }

        public CarNotFoundException(string message)
            : base(message)
        {
        }
    }
}
