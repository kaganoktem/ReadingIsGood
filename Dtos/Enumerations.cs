using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class Enumerations
    {
        public enum BookDeliveryStatus
        {
            GettingPrepared = 0,
            Ongoing = 1,
            Delivered = 2,
            Cancelled = 3,
        }
    }
}
