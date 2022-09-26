using solvexTecnical.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Domain.Entities
{
    public class SuperMarket: CommonEntity
    {
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }
        public ICollection<ProductsBrands> ProductsBrands { get; set; }
    }
}
