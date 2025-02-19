using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public static class IUser
    {
        private static DataContext context;
        public static string IValue 
        { get 
            { 
            return ((System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal)
                   .Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Name)
                   .Select(c => c.Value)
                   .SingleOrDefault();
            } 
        }
        public static string Id
        { get 
            {
                context = new DataContext();
                return context.tbl_useraccount.Where(x => x.UserName == IValue)
                    .Select(x => x.UserID)
                    .FirstOrDefault();
            } 
        }
        public static string Name
        {
            get
            {
                context = new DataContext();
                return context.tbl_useraccount.Where(x => x.UserName == IValue)
                    .Select(x => x.UserName)
                    .FirstOrDefault();
            }
        }
        public static bool IsAdmin
        {
            get
            {
                context = new DataContext();
                var userType = context.tbl_useraccount.Where(x => x.UserName == IValue)
                    .Select(x => x.UserLevel)
                    .FirstOrDefault();
                return userType == 3 ? true : false;
            }
        }
        public static string StockID
        {
            get
            {
                context = new DataContext();
                return context.tbl_useraccount.Where(x => x.UserName == IValue)
                    .Select(x => x.StockID)
                    .FirstOrDefault();
            }
        }

        public static long userId()
        {
           using(var _context = new DataContext())
            {
                var userId = _context.tbl_useraccount.Where(x => x.UserName == IValue)
                .Select(x => x.UserID)
                .FirstOrDefault();
                if (userId == null || userId == "0")
                    return 0;

                return long.Parse(userId);
            }
        }
    }
}