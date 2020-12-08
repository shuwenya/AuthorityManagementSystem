using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.Model.Entity
{
    public class RoleMenu
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
