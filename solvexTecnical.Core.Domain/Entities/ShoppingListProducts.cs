using solvexTecnical.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Domain.Entities
{
    public class ShoppingListProducts: CommonEntity
    {
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public int FinalProductId { get; set; }
        public FinalProducts FinalProducts { get; set; }
        
    }
}
