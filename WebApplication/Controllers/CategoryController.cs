using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private DataContext context;
        private CategoryRepository categoryRepository;
        public CategoryController()
        {
            this.context = new DataContext();
            this.categoryRepository = new CategoryRepository();
        }
        // GET: Category
        public ActionResult Index()
        {
            //ViewBag.PrdCategIDMain = new SelectList(context.tbl_prdcategory, "PrdCategID", "PrdCategory");
            var categories = context.tbl_prdcategory
                .OrderBy(x => x.CategoryOrder)
                .ToList();
            ViewBag.PrdCategIDMains = new SelectList(categories, "PrdCategID", "PrdCategory");
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            string paraId = Request["PrdCategID"];
            var data = categoryRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdCategory.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = categoryRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

            var displayResult = data.Skip(param.iDisplayStart)
                                .Take(param.iDisplayLength).ToList();
            var totalRecords = data.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);
        }
        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // POST: Category/Create
        [Route("Category/Create")]
        [HttpPost]
        public JsonResult Create(tbl_prdcategory data)
        {
            if (!string.IsNullOrEmpty(data.PrdCategID))
            {
                var resUpdate = categoryRepository.Update(data);
                return Json(new { resUpdate.status, resUpdate.message }, JsonRequestBehavior.AllowGet);
            }
            var resCreate = categoryRepository.Create(data);
            return Json(new { resCreate.status, resCreate.message }, JsonRequestBehavior.AllowGet);
        }

        //// POST: Category/Edit/5
        [Route("Category/Edit")]
        [HttpPost]
        public JsonResult Edit(tbl_prdcategory data)
        {
            var dataJson = context.tbl_prdcategory.FirstOrDefault(x => x.PrdCategID == data.PrdCategID);
            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }

        // GET: Category/Delete/5
        [Route("Category/Delete")]
        [HttpPost]
        public JsonResult Delete(tbl_prdcategory data)
        {
            return Json(categoryRepository.Delete(data), JsonRequestBehavior.AllowGet);
        }



        //------------------------///------------------------//
        public JsonResult GetTreeData()
        {
            var categories = context.tbl_prdcategory.ToList();
            var treeData = new List<TreeNode>
            {
                //new TreeNode { Id = 1, ParentId = 0, Name = "Root 1" },
                //new TreeNode { Id = 2, ParentId = 1, Name = "Child 1" },
                //new TreeNode { Id = 3, ParentId = 1, Name = "Child 2" },
                //new TreeNode { Id = 4, ParentId = 0, Name = "Root 2" },
                //new TreeNode { Id = 5, ParentId = 4, Name = "Child 3" },
                //new TreeNode { Id = 6, ParentId = 4, Name = "Child 4" },
                //new TreeNode { Id = 7, ParentId =5, Name = "Child 3 1" },
                //new TreeNode { Id = 8, ParentId =5, Name = "Child 3 2" },
            };

            // Assuming the categories have fields like Id, ParentId, and Name
            foreach (var category in categories)
            {
                treeData.Add(new TreeNode
                {
                    Id = category.PrdCategID, // Assuming this is the unique ID field
                    ParentId = category.PrdCategIDMain, // Assuming you have a ParentId field
                    Name = category.PrdCategory // Assuming this is the name of the category
                });
            }

            // Build the hierarchical structure
            var tree = BuildTree("0", treeData);
            return Json(tree, JsonRequestBehavior.AllowGet);
        }

        //public List<TreeNode> GetTreeData()
        //{
        //    // Retrieve the categories from your context or database
        //    var categories = context.tbl_prdcategory.ToList();

        //    // Create a list of TreeNode objects based on the categories
        //    var allNodes = categories.Select(c => new TreeNode
        //    {
        //        Id = c.PrdCategID,  // Assuming PrdCategID is a string or can be converted to string
        //        ParentId = c.PrdCategIDMain,  // Assuming ParentId is a string or can be converted to string
        //        Name = c.PrdCategory,  // Assuming PrdCategory is the name of the category
        //    }).ToList();

        //    // Initialize the treeData list that will hold the root nodes
        //    var treeData = new List<TreeNode>();

        //    // Build the tree structure by finding parents and adding them as children
        //    foreach (var node in allNodes)
        //    {
        //        // Find the parent node (if any)
        //        var parentNode = allNodes.FirstOrDefault(n => n.Id == node.ParentId);

        //        // If a parent exists, add the current node to its Children list
        //        if (parentNode != null)
        //        {
        //            parentNode.Children.Add(node);
        //        }
        //        else
        //        {
        //            // If no parent (root node), add it directly to the root-level treeData list
        //            treeData.Add(node);
        //        }
        //    }

        //    // Return the root nodes, which now contain nested children
        //    return treeData;
        //}

        private List<TreeNode> BuildTree(string parentId, List<TreeNode> data)
        {
            return data.Where(x => x.ParentId == parentId)
                       .Select(x => new TreeNode
                       {
                           Id = x.Id,
                           ParentId = x.ParentId,
                           Name = x.Name,
                           Children = BuildTree(x.Id, data) // Recursively get children
                       }).ToList();
        }
    }
}
