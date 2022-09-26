using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Domain.Entities.Common
{
    public class CommonEntity
    {
        public int Id { get; set; }
        public int IsDeleted { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; } = null;

    }
}
