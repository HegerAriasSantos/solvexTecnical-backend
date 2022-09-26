using solvexTecnical.Core.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace solvexTecnical.Core.Application.DTOs
{
    public class SuperMarketDTO : DeletedProps
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<BrandDTO> Brands { get; set; }
        public List<FinalProductDTO> Products { get; set; }

    }
}
