using solvexTecnical.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Domain.Entities
{
    public class ProductsBrands : CommonEntity
    {
        public string Name { get; set; }
        public int SuperMarketId { get; set; }
        public SuperMarket SuperMarket { get; set; }
        public IList<FinalProducts> FinalProducts { get; set; }

    }
}
