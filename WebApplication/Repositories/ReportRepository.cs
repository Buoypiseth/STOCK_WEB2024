using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication.Repositories
{
    public class ReportRepository
    {
        private DataContext context;
        private DBConnect dBConnect;
        public ReportRepository()
        {
            this.context = new DataContext();
            this.dBConnect = new DBConnect();
        }
        long UserID = long.Parse(IUser.Id);
        public string DeleteOtherOptionEmployeeSet()
        {
            try
            {
                var Items = context.tbl_OTHER_OPTIONS_Employee_Set.Where(x => x.UserID.Equals(UserID)).ToList();
                if (Items != null)
                {
                    Items.ForEach(x => context.tbl_OTHER_OPTIONS_Employee_Set.Remove(x));
                    this.Save();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string InsertOtherOptionEmployeeSet(tbl_OTHER_OPTIONS_Employee_Set data)
        {
            try
            {
                context.tbl_OTHER_OPTIONS_Employee_Set.Add(data);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string InsertOtherOptionFilter(tbl_OTHER_OPTIONS_FILTER data)
        {
            try
            {
                var dt = context.tbl_OTHER_OPTIONS.FirstOrDefault(x => x.para_ReportName == data.para_ReportName);
                if (dt != null)
                {
                    tbl_OTHER_OPTIONS_FILTER otherOption = new tbl_OTHER_OPTIONS_FILTER();
                    otherOption.ID = dt.ID;
                    otherOption.EmployeeID = -1;
                    otherOption.RunID = "-1";
                    otherOption.UserID = UserID;
                    otherOption.para07_cboName = data.para07_cboName;
                    otherOption.para06_cboGroup = "-1";
                    otherOption.para08_0_TabMonthly = dt.para08_0_TabMonthly;
                    otherOption.para08_1_cboMonthly = data.para08_1_cboMonthly;
                    otherOption.para08_2_Monthly_cboFrom = data.para08_2_Monthly_cboFrom;
                    otherOption.para08_3_Monthly_cboTo = data.para08_3_Monthly_cboTo;
                    otherOption.para09_1_Date = data.para09_1_Date;
                    otherOption.para10_1_Date_From_To_cboFrom = data.para10_1_Date_From_To_cboFrom;
                    otherOption.para10_2_Date_From_To_cboTo = data.para10_2_Date_From_To_cboTo;
                    otherOption.para11_1_Yearly_cboYear = data.para11_1_Yearly_cboYear;

                    otherOption.para_ReportName = dt.para_ReportName;
                    otherOption.para_Stored_Pro_Name = dt.para_Stored_Pro_Name;
                    otherOption.para_Calculate_Time = dt.para_Calculate_Time;
                    otherOption.paraEmployee_Set = dt.paraEmployee_Set;
                    otherOption.para01_cmdCalculate = dt.para01_cmdCalculate;
                    otherOption.para02_cmdViewReportMS = dt.para02_cmdViewReportMS;
                    otherOption.para03_cmdViewReportCR = dt.para03_cmdViewReportCR;
                    otherOption.para04_cmdExport = dt.para04_cmdExport;
                    otherOption.para05_cmdImport = dt.para05_cmdImport;
                    otherOption.para09_0_TabDate = dt.para09_0_TabDate;
                    otherOption.para10_0_TabDate_From_To = dt.para10_0_TabDate_From_To;
                    otherOption.para11_0_TabYearly = dt.para11_0_TabYearly;

                    this.DeleteOtherOptionFilter(otherOption);
                    context.tbl_OTHER_OPTIONS_FILTER.Add(otherOption);
                    this.Save();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string DeleteOtherOptionFilter(tbl_OTHER_OPTIONS_FILTER data)
        {
            try
            {
                var Items = context.tbl_OTHER_OPTIONS_FILTER.Where(x => x.UserID.Equals(data.UserID)).ToList();
                if (Items != null)
                {
                    Items.ForEach(x => context.tbl_OTHER_OPTIONS_FILTER.Remove(x));
                    this.Save();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public SqlDataReader dataReader(tbl_OTHER_OPTIONS_FILTER data)
        {
            SqlConnection conn = new SqlConnection(dBConnect.Getconnectionstring("SqlConnection"));
            SqlCommand command = new SqlCommand(data.para_Stored_Pro_Name, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ID", SqlDbType.BigInt).Value = data.ID;
            command.Parameters.Add("UserID", SqlDbType.BigInt).Value = data.UserID;
            command.Parameters.Add("EmployeeID", SqlDbType.BigInt).Value = data.EmployeeID;
            command.Parameters.Add("RunID", SqlDbType.VarChar).Value = data.RunID;
            conn.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            return dataReader;

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