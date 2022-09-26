using solvexTecnical.Core.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Application.DTOs
{
    public class ProductDTO : DeletedProps
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SuperMarketId { get; set; }
        public SuperMarketDTO SuperMarket { get; set; }
        public IList<FinalProductDTO> FinalProducts { get; set; }
    }
}
