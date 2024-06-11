using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    interface ICustomPrincipal : IPrincipal
    {
        string Id { get; set; }
        string Name { get; set; }
        int UserLevel { get; set; }
    }
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public int UserLevel { get; set; }
        public bool IsAdmin { get { return this.UserLevel == 3 ? true : false; } }
    }
    public class CustomPrincipalSerializeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int UserLevel { get; set; }
    }
}