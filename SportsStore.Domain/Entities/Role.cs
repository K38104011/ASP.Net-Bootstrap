using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    [Table("webpages_Roles")]
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
