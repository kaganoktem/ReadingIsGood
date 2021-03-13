using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class ServiceResultDto
    {
        public bool IsSuccess { get; set; }

        public object Result { get; set; }

        public ReadingIsGoodException Rex { get; set; }
    }
}
