using solvexTecnical.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace solvexTecnical.Core.Domain.Entities
{
    public class Users : CommonEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Cedula { get; set; }
        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
