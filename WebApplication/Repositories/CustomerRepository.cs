using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Web.Mvc;

namespace WebApplication.Repositories
{
    public class CustomerRepository
    {
        private DataContext context;
        private tbl_customers model;
        public CustomerRepository()
        {
            this.context = new DataContext();
            this.model = new tbl_customers();
        }
        public IEnumerable<SelectListItem> memberTitle { get; set; }
        public IEnumerable<SelectListItem> Gender { get; set; }
        public IEnumerable<SelectListItem> idType { get; set; }
        public IEnumerable<SelectListItem> Nationality { get; set; }
        public List<tbl_customers> GetForDataTable()
        {
            return context.tbl_customers
                .OrderBy(x => x.memberID)
                .ToList();
        }
        public List<tbl_customers_account> GetForAccount(string custId)
        {
            return context.tbl_customers_account.Where(x => x.memberID == custId).ToList();
        }
        public List<CustomerAccount> GetDataTransaction(string custId = "20230814113725", string tranType = "Customer Refill")
        {
            // Initialization.
            List<CustomerAccount> lst = new List<CustomerAccount>();
            try
            {
                var custAccInOutHistory = (from a in context.tbl_customers_account
                                           join h in context.tbl_customers_account_InOut_History on a.CustomerAccountID equals h.CustomerAccountID
                                           where (a.memberID == custId
                                           && h.Type == tranType
                                           && a.IsActive == true)
                                           select new
                                           {
                                               CustAccInOutHistoryID = h.CustAccInOutHistoryID,
                                               CustomerAccountID = h.CustomerAccountID,
                                               AccountType = a.AccountType,
                                               Amount = h.Amount,
                                               RealMoney = h.RealMoney,
                                               Date = h.Date,
                                               Description = h.Description,
                                               Type = h.Type,
                                               BeginAmount = h.BeginAmount,
                                               Username = h.Username
                                           }).ToList()
                                .OrderBy(x => x.Date);
                foreach (var i in custAccInOutHistory)
                {
                    CustomerAccount setAccInOut = new CustomerAccount();
                    setAccInOut.CustAccInOutHistoryID = i.CustAccInOutHistoryID;
                    setAccInOut.CustomerAccountID = i.CustomerAccountID;
                    setAccInOut.AccountType = i.AccountType;
                    setAccInOut.Amount = i.Amount;
                    setAccInOut.RealMoney = i.RealMoney;
                    setAccInOut.Date = i.Date;
                    setAccInOut.Type = i.Type;
                    setAccInOut.BeginAmount = i.BeginAmount;
                    setAccInOut.Username = i.Username;
                    setAccInOut.Description = i.Description;
                    lst.Add(setAccInOut);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }
        public List<tbl_customers> SortByColumnWithOrder(string order, string orderDir, List<tbl_customers> data)
        {
            // Initialization.  
            List<tbl_customers> lst = new List<tbl_customers>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.memberID).ToList()
                                                                                                 : data.OrderBy(p => p.memberID).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.icpassportNo).ToList()
                                                                                                 : data.OrderBy(p => p.icpassportNo).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.fullName).ToList()
                                                                                                 : data.OrderBy(p => p.fullName).ToList();
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
        //public string Create(Customer data)
        //{
        //    try
        //    {
        //        model.memberID = Helper.AutoID();
        //        model.fullName = data.fullName;
        //        model.Gender = data.Gender;
        //        model.idType = data.idType;
        //        model.icpassportNo = data.icpassportNo;
        //        model.Nationality = data.Nationality;
        //        model.Mobile = data.Mobile;
        //        model.eMail = data.eMail;
        //        model.Address = data.Address;
        //        context.tbl_customers.Add(model);
        //        this.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        // info.  
        //        Console.Write(ex);
        //        return ex.ToString();
        //    }
        //    return "True";
        //}
        //public string Update(Customer data)
        //{
        //    try
        //    {
        //        model = context.tbl_customers.Find(data.memberID);
        //        model.fullName = data.fullName;
        //        model.Gender = data.Gender;
        //        model.idType = data.idType;
        //        model.icpassportNo = data.icpassportNo;
        //        model.Nationality = data.Nationality;
        //        model.Mobile = data.Mobile;
        //        model.eMail = data.eMail;
        //        model.Address = data.Address;
        //        context.Entry(model).State = EntityState.Modified;
        //        this.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        // info.  
        //        Console.Write(ex);
        //        return ex.ToString();
        //    }
        //    return "True";
        //}

        public string Create(Customer data)
        {
            try
            {
                model.memberID = Helper.AutoID();
                model.memberTitle = data.memberTitle == null ? "" : data.memberTitle;
                model.firstName = data.firstName == null ? "" : data.firstName;
                model.lastName = data.lastName == null ? "" : data.lastName;
                model.fullName = data.fullName == null ? "" : data.fullName;
                model.nickName = data.nickName == null ? "" : data.nickName;
                model.idType = data.idType == null ? "" : data.idType;
                model.icpassportNo = data.icpassportNo == null ? "" : data.icpassportNo;
                model.Nationality = data.Nationality == null ? "" : data.Nationality;
                model.Gender = data.Gender == null ? "" : data.Gender;
                model.Birthdate = data.Birthdate == null ? DateTime.Now : data.Birthdate;
                model.MaritalStatus = data.MaritalStatus == null ? "" : data.MaritalStatus;
                model.Address = data.Address == null ? "" : data.Address;
                model.ZipCode = data.ZipCode == null ? "" : data.ZipCode;
                model.PostalCode = data.PostalCode == null ? "" : data.PostalCode;
                model.POBox = data.POBox == null ? "" : data.POBox;
                model.City = data.City == null ? "" : data.City;
                model.Country = data.Country == null ? "" : data.Country;
                model.Tel1 = data.Tel1 == null ? "" : data.Tel1;
                model.Tel2 = data.Tel2 == null ? "" : data.Tel2;
                model.Fax = data.Fax == null ? "" : data.Fax;
                model.Mobile = data.Mobile == null ? "" : data.Mobile;
                model.eMail = data.eMail == null ? "" : data.eMail;
                model.autono = data.autono == null ? 0 : data.autono;
                model.VAT_NO = data.VAT_NO == null ? "" : data.memberTitle;
                model.Water_MeterNo = data.Water_MeterNo;
                model.Water_Diameter = data.Water_Diameter;
                model.Water_inactive = data.Water_inactive;
                model.Water_Decription = data.Water_Decription;
                model.StreetNo = data.StreetNo == null ? "" : data.StreetNo;
                model.HouseNo = data.HouseNo == null ? "" : data.HouseNo;
                model.village = data.village == null ? "" : data.village;
                model.communes = data.communes == null ? "" : data.communes;
                model.district = data.district == null ? "" : data.district;
                model.city_province = data.city_province == null ? "" : data.city_province;
                model.Passport_Issue = data.Passport_Issue == null ? DateTime.Now : data.Passport_Issue;
                model.Passport_Expire = data.Passport_Expire == null ? DateTime.Now : data.Passport_Expire;
                model.Visa_Expire = data.Visa_Expire == null ? DateTime.Now : data.Visa_Expire;
                model.Group_Customers_ID = data.Group_Customers_ID == null ? -1 : data.Group_Customers_ID;
                model.CodeFP = data.CodeFP == null ? 0 : data.CodeFP;
                model.MemberCardNo = data.MemberCardNo == null ? "" : data.MemberCardNo;
                model.Sincroni_Info = data.Sincroni_Info == null ? "" : data.Sincroni_Info;
                context.tbl_customers.Add(model);
                context.SaveChanges();

                ////Store Account
                //tbl_customers_account setCustAcc = new tbl_customers_account();
                //setCustAcc.CustomerAccountID = Helper.AutoID();
                //setCustAcc.memberID = model.memberID;
                //setCustAcc.AccountType = data.AccountType ?? "";
                //setCustAcc.Amount = 0;
                //setCustAcc.RealMoneyRemain = 0;
                //setCustAcc.StartDate = data.StartDate ?? DateTime.Now;
                //setCustAcc.DateExpire = data.DateExpire ?? DateTime.Now;
                //setCustAcc.LimitePay1Day = data.LimitePay1Day ?? 0;
                //setCustAcc.LimitePay1Time = data.LimitePay1Time ?? 0;
                //setCustAcc.Description = data.Address ?? "";
                //setCustAcc.EditeNote = IUser.Name + "/UserID " + IUser.Id;
                //setCustAcc.UpdatedAt = DateTime.Now;
                //setCustAcc.IsActive = true;
                //setCustAcc.IsDefault = true;
                //context.tbl_customers_account.Add(setCustAcc);
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Update(Customer data)
        {
            try
            {
                model = context.tbl_customers.Find(data.memberID);
                model.fullName = data.fullName == null ? "" : data.fullName;
                model.Gender = data.Gender;
                model.idType = data.idType;
                model.icpassportNo = data.icpassportNo == null ? "" : data.icpassportNo;
                model.Passport_Issue = data.Passport_Issue == null ? DateTime.Now : data.Passport_Issue;
                model.Passport_Expire = data.Passport_Expire == null ? DateTime.Now : data.Passport_Expire;
                model.Nationality = data.Nationality == null ? "" : data.Nationality;
                model.Mobile = data.Mobile == null ? "" : data.Mobile;
                model.eMail = data.eMail == null ? "" : data.eMail;
                model.Address = data.Address == null ? "" : data.Address;
                context.Entry(model).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Delete(Customer data)
        {
            try
            {
                model = context.tbl_customers.Find(data.memberID);
                context.tbl_customers.Remove(model);
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
        //create customer account
        public string StoreAccount(CustomerAccount data)
        {
            try
            {
                tbl_customers_account setCustAcc = new tbl_customers_account();
                setCustAcc.CustomerAccountID = Helper.AutoID();
                setCustAcc.memberID = data.memberID;
                setCustAcc.AccountType = data.AccountType ?? "";
                setCustAcc.Amount = data.Amount ?? 0;
                setCustAcc.RealMoneyRemain = data.RealMoneyRemain ?? 0;
                setCustAcc.StartDate = data.StartDate ?? DateTime.Now;
                setCustAcc.DateExpire = data.DateExpire ?? DateTime.Now;
                setCustAcc.LimitePay1Day = data.LimitePay1Day ?? 0;
                setCustAcc.LimitePay1Time = data.LimitePay1Time ?? 0;
                setCustAcc.Description = data.Description ?? "";
                setCustAcc.EditeNote = IUser.Name + "/UserID " + IUser.Id;
                setCustAcc.UpdatedAt = DateTime.Now;
                setCustAcc.IsActive = true;
                setCustAcc.IsDefault = false;
                context.tbl_customers_account.Add(setCustAcc);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        //store customer account deposited
        public string StoreDeposited(CustomerAccount data)
        {
            try
            {
                var setCustAcc = context.tbl_customers_account.FirstOrDefault(x => x.CustomerAccountID == data.CustomerAccountID);
                setCustAcc.Amount = setCustAcc.Amount + data.Amount;
                setCustAcc.RealMoneyRemain = setCustAcc.RealMoneyRemain + data.Amount;
                //setCustAcc.Description = data.Description ?? "";
                setCustAcc.EditeNote = IUser.Name + "/UserID " + IUser.Id;
                setCustAcc.UpdatedAt = DateTime.Now;
                context.Entry(setCustAcc).State = EntityState.Modified;
                context.SaveChanges();

                this.StoreAccInOutHistory(data);
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        //store customer account cashouts
        public string StoreCashouts(CustomerAccount data)
        {
            try
            {
                var setCustAcc = context.tbl_customers_account.FirstOrDefault(x => x.CustomerAccountID == data.CustomerAccountID);
                setCustAcc.Amount = setCustAcc.Amount + data.Amount;
                setCustAcc.RealMoneyRemain = setCustAcc.RealMoneyRemain + data.Amount;
                //setCustAcc.Description = data.Description ?? "";
                setCustAcc.EditeNote = IUser.Name + "/UserID " + IUser.Id;
                setCustAcc.UpdatedAt = DateTime.Now;
                context.Entry(setCustAcc).State = EntityState.Modified;
                context.SaveChanges();

                this.StoreAccInOutHistory(data);
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        //update customer account
        public string UpdateAccount(CustomerAccount data)
        {
            try
            {
                var setCustAcc = context.tbl_customers_account.FirstOrDefault(x => x.CustomerAccountID == data.CustomerAccountID);
                setCustAcc.DateExpire = data.DateExpire ?? DateTime.Now;
                setCustAcc.LimitePay1Day = data.LimitePay1Day ?? 0;
                setCustAcc.LimitePay1Time = data.LimitePay1Time ?? 0;
                setCustAcc.Description = data.Description ?? "";
                setCustAcc.IsActive = data.IsActive;
                setCustAcc.EditeNote = IUser.Name + "/UserID " + IUser.Id;
                setCustAcc.UpdatedAt = DateTime.Now;
                context.Entry(setCustAcc).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string StoreAccInOutHistory(CustomerAccount data)
        {
            try
            {
                var custAcc = context.tbl_customers_account.FirstOrDefault(x => x.CustomerAccountID == data.CustomerAccountID);
                tbl_customers_account_InOut_History setAccInOutHistory = new tbl_customers_account_InOut_History();
                setAccInOutHistory.CustAccInOutHistoryID = Helper.AutoID();
                setAccInOutHistory.CustomerAccountID = custAcc.CustomerAccountID;
                setAccInOutHistory.Amount = data.Amount ?? 0;
                setAccInOutHistory.RealMoney = data.Amount ?? 0;
                setAccInOutHistory.Date = data.Date ?? DateTime.Now;
                setAccInOutHistory.Description = data.Description ?? "";
                setAccInOutHistory.Type = data.Type ?? "Customer Refill";
                setAccInOutHistory.BeginAmount = custAcc.RealMoneyRemain - data.Amount;
                setAccInOutHistory.customerOrderID = data.customerOrderID ?? "";
                setAccInOutHistory.UserID = IUser.Id;
                setAccInOutHistory.Username = IUser.Name;
                setAccInOutHistory.EditeNote = IUser.Name + "/UserID " + IUser.Id;
                context.tbl_customers_account_InOut_History.Add(setAccInOutHistory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public void Save()
        {
            context.SaveChangesAsync();
            //context.SaveChanges();
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