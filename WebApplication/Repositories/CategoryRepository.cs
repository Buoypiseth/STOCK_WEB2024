using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class CategoryRepository
    {
        private DataContext context;
        private tbl_prdcategory model;
        public CategoryRepository()
        {
            this.context = new DataContext();
            this.model = new tbl_prdcategory();
        }
        public List<tbl_prdcategory> GetForDataTable()
        {
            return context.tbl_prdcategory
                            .OrderBy(x => x.CategoryOrder)
                            .ToList();
        }
        public List<tbl_prdcategory> SortByColumnWithOrder(string order, string orderDir, List<tbl_prdcategory> data)
        {
            // Initialization.  
            List<tbl_prdcategory> lst = new List<tbl_prdcategory>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CategoryOrder).ToList()
                                                                                                 : data.OrderBy(p => p.CategoryOrder).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PrdCategory).ToList()
                                                                                                 : data.OrderBy(p => p.PrdCategory).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CategoryOrder).ToList()
                                                                                                 : data.OrderBy(p => p.CategoryOrder).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
            }

            // info.  
            return lst;
        }
        public (string status, string message) Create(tbl_prdcategory data)
        {
            try
            {
                model.PrdCategID = Helper.AutoID();
                model.PrdCmpntID = "0";
                model.PrdCategIDMain = "0";
                model.PrdCategory = data.PrdCategory;
                model.PrdCategoryKh = data.PrdCategory;
                model.CategoryOrder = data.CategoryOrder;
                model.MiniMumInstock = 0;
                context.tbl_prdcategory.Add(model);
                this.Save();
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Category_Created);
        }
        public (string status, string message) Update(tbl_prdcategory data)
        {
            try
            {
                model = context.tbl_prdcategory.FirstOrDefault(x => x.PrdCategID == data.PrdCategID);
                model.PrdCategory = data.PrdCategory;
                model.PrdCategoryKh = data.PrdCategory;
                model.CategoryOrder = data.CategoryOrder;
                context.Entry(model).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Category_Updated);
        }
        public string Delete(tbl_prdcategory data)
        {
            try
            {
                tbl_prdcategory _category = context.tbl_prdcategory.FirstOrDefault(x => x.PrdCategID == data.PrdCategID);
                context.tbl_prdcategory.Remove(_category);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "True";
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}