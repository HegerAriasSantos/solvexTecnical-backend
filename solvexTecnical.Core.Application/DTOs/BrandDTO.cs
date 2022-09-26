using solvexTecnical.Core.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace solvexTecnical.Core.Application.DTOs
{
    public class BrandDTO : DeletedProps
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int SuperMarketId { get; set; }
        public SuperMarketDTO SuperMarket { get; set; }
    }
}
