using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Models
{
    public class AssignRoleViewModel
    {
        public string RoleName { get; set; }
        public string UserName { get; set; }

        public List<SelectListItem> UserList { get; set; }
        public List<SelectListItem> RolesList { get; set; } 
    }

    public class RoleAndUser
    {
        public string RoleName { get; set; }
        public string UserName { get; set; }
    }  
}