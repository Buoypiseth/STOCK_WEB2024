using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class SupplierRepository
    {
        private DataContext context;
        private tbl_suppliers model;
        public SupplierRepository()
        {
            this.context = new DataContext();
            this.model = new tbl_suppliers();
        }
        public List<tbl_suppliers> GetForDataTable()
        {
            return context.tbl_suppliers.ToList();
        }
        public List<tbl_suppliers> SortByColumnWithOrder(string order, string orderDir, List<tbl_suppliers> data)
        {
            // Initialization.  
            List<tbl_suppliers> lst = new List<tbl_suppliers>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.CompanyName).ToList()
                                                                                                 : data.OrderBy(p => p.CompanyName).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ContFirstName).ToList()
                                                                                                 : data.OrderBy(p => p.ContFirstName).ToList();
                        break;
                    case "2":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ContLastName).ToList()
                                                                                                 : data.OrderBy(p => p.ContLastName).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MobilePhone).ToList()
                                                                                                 : data.OrderBy(p => p.MobilePhone).ToList();
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
        //public string Create(tbl_suppliers data)
        //{
        //    try
        //    {
        //        model.SupplierID = Helper.AutoID();
        //        model.CompanyName = data.CompanyName == null ? "" : data.CompanyName;
        //        model.ContFirstName = data.ContFirstName == null ? "" : data.ContFirstName;
        //        model.ContLastName = data.ContLastName == null ? "" : data.ContLastName;
        //        model.JobTitle = data.JobTitle == null ? "" : data.JobTitle;
        //        model.BusPhone = data.BusPhone == null ? "" : data.BusPhone;
        //        model.MobilePhone = data.MobilePhone == null ? "" : data.MobilePhone;
        //        model.FaxNumber = data.FaxNumber == null ? "" : data.FaxNumber;
        //        model.Address = data.Address == null ? "" : data.Address;
        //        model.ZipPostalCode = data.ZipPostalCode == null ? "" : data.ZipPostalCode;
        //        model.Region = data.Region == null ? "" : data.Region;
        //        model.supCity = data.supCity == null ? "" : data.supCity;
        //        model.supCountry = data.supCountry == null ? "" : data.supCountry;
        //        model.CompEmail = data.CompEmail == null ? "" : data.CompEmail;
        //        model.WebPage = data.WebPage == null ? "" : data.WebPage;
        //        model.Notes = data.Notes == null ? "" : data.Notes;
        //        model.LastUpdate = data.LastUpdate == null ? DateTime.Now : data.LastUpdate;
        //        model.autoNo = data.autoNo == null ? 0 : data.autoNo;
        //        context.tbl_suppliers.Add(model);
        //        this.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //    return "True";
        //}
        public string Create(Supplier data)
        {
            try
            {
                model.SupplierID = Helper.AutoID();
                model.CompanyName = data.CompanyName ?? "";
                model.ContFirstName = data.ContFirstName ?? "";
                model.ContLastName = data.ContLastName ?? "";
                model.JobTitle = data.JobTitle ?? "";
                model.BusPhone = data.BusPhone ?? "";
                model.MobilePhone = data.MobilePhone ?? "";
                model.FaxNumber = data.FaxNumber ?? "";
                model.Address = data.Address ?? "";
                model.ZipPostalCode = data.ZipPostalCode ?? "";
                model.Region = data.Region ?? "";
                model.supCity = data.supCity ?? "";
                model.supCountry = data.supCountry ?? "";
                model.CompEmail = data.CompEmail ?? "";
                model.WebPage = data.WebPage ?? "";
                model.Notes = data.Notes ?? "";
                model.LastUpdate = data.LastUpdate ?? DateTime.Now;
                model.autoNo = data.autoNo ?? 0;
                context.tbl_suppliers.Add(model);
                this.Save();
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string Update(Supplier data)
        {
            try
            {
                model = context.tbl_suppliers.FirstOrDefault(x => x.SupplierID == data.SupplierID);
                model.CompanyName = data.CompanyName ?? "";
                model.BusPhone = data.BusPhone ?? "";
                model.CompEmail = data.CompEmail ?? "";
                model.Address = data.Address ?? "";
                model.Notes = data.Notes ?? "";
                model.WebPage = data.WebPage ?? "";
                model.ContFirstName = data.ContFirstName ?? "";
                model.ContLastName = data.ContLastName ?? "";
                model.JobTitle = data.JobTitle ?? "";
                model.MobilePhone = data.MobilePhone ?? "";
                context.Entry(model).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Delete(tbl_suppliers data)
        {
            try
            {
                model = context.tbl_suppliers.Find(data.SupplierID);
                context.tbl_suppliers.Remove(model);
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