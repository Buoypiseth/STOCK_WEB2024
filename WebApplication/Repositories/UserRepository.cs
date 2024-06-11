using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Web.Mvc;

namespace WebApplication.Repositories
{
    public class UserRepository
    {
        private DataContext context;
        private tbl_useraccount model;
        public IEnumerable<SelectListItem> UserLevel { get; set; }
        public UserRepository()
        {
            this.context = new DataContext();
            this.model = new tbl_useraccount();
        }
        public List<tbl_useraccount> GetForDataTable()
        {

            var users = context.tbl_useraccount.ToList();
            List<tbl_useraccount> lst = new List<tbl_useraccount>();
            foreach (var i in users)
            {
                tbl_useraccount infoObj = new tbl_useraccount();
                infoObj.UserID = i.UserID;
                infoObj.FirstName = i.FirstName;
                infoObj.LastName = i.LastName;
                infoObj.UserName = i.UserName;
                infoObj.UserLevel = i.UserLevel;
                lst.Add(infoObj);
            }
            return lst;
        }
        public List<tbl_useraccount> SortByColumnWithOrder(string order, string orderDir, List<tbl_useraccount> data)
        {
            // Initialization.  
            List<tbl_useraccount> lst = new List<tbl_useraccount>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.UserID).ToList()
                                                                                                 : data.OrderBy(p => p.UserID).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UserName).ToList()
                                                                                                 : data.OrderBy(p => p.UserName).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UserLevel).ToList()
                                                                                                 : data.OrderBy(p => p.UserLevel).ToList();
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
        public string Create(UserModel data)
        {
            try
            {
                model.UserID = Helper.AutoID();
                model.FirstName = data.FirstName;
                model.LastName = data.LastName;
                model.UserName = data.Username;
                model.UserPwd = data.Password;
                model.UserLevel = 0;
                model.StockID = "0";
                model.LangCode = "km";
                context.tbl_useraccount.Add(model);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Update(UserModel data)
        {
            try
            {
                model = context.tbl_useraccount.FirstOrDefault(x => x.UserID == data.UserID);
                model.FirstName = data.FirstName;
                model.LastName = data.LastName;
                model.UserName = data.Username;
                model.UserPwd = data.Password;
                context.Entry(model).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string ChangePassword(UserModel data)
        {
            try
            {
                model = context.tbl_useraccount.FirstOrDefault(x => x.UserID == data.UserID);
                model.UserName = data.Username;
                model.UserPwd = data.Password;
                context.Entry(model).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Delete(tbl_useraccount data)
        {
            try
            {
                model = context.tbl_useraccount.Find(data.UserID);
                context.tbl_useraccount.Remove(model);
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