using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Cedula { get; set; }

        public List<ShoppingListDTO> ShoppingLists { get; set; }

    }
}
