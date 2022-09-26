using solvexTecnical.Core.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace solvexTecnical.Core.Application.DTOs
{
    public class FinalProductDTO : DeletedProps
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public ProductDTO Product { get; set; }
        [Required]
        public int BrandId { get; set; }
        public BrandDTO Brand { get; set; }

        [Required]
        public int SuperMarketId { get; set; }
        public decimal Price { get; set; }
    }
}
