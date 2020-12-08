using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.Model.Entity
{
    public class Role : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
