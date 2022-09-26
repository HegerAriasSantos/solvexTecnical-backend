using solvexTecnical.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Domain.Entities
{
    public class ShoppingList: CommonEntity
    {
        public decimal TotalPrice { get; set; }
        
        public int SuperMarketId { get; set; }

        public int UserId { get; set; }

        public Users User { get; set; }

        public SuperMarket SuperMarket { get; set; }

        public IList<ShoppingListProducts> ShoppingListProducts { get; set; }
    }
}
