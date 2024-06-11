using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Category
    {
        [Key]
        public string PrdCategID { get; set; }
        public string PrdCmpntID { get; set; }
        public string PrdCategory { get; set; }
        public string PrdCategoryKh { get; set; }
        public string PrdCategIDMain { get; set; }
        public Nullable<int> CategoryOrder { get; set; }
        public Nullable<double> MiniMumInstock { get; set; }
    }
}