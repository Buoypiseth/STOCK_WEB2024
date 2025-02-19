using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TreeNode
    {
        //public int Id { get; set; }
        //public int ParentId { get; set; }
        //public string Name { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public List<TreeNode> Children { get; set; }
    }
}