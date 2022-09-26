using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace solvexTecnical.Core.Application.DTOs.Common
{
    public  class DeletedProps
    {
        [JsonIgnore]
        public int IsDeleted { get; set; } = 0;
        [JsonIgnore]
        public DateTime? DeletedDate { get; set; } = null;
    }
}
