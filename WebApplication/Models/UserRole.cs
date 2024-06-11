using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class UserRole
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public long RoleId { get; set; }
    }
}