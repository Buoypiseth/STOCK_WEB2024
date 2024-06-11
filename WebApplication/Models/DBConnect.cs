using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DBConnect
    {
        public string Getconnectionstring(string keyName)
        {
            string connection = string.Empty;
            switch (keyName)
            {
                case "SqlConnection":
                    connection = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                    break;
                default:
                    break;
            }
            return connection;

        }
    }
}