using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Username { get; set; }
        public string LangCode { get; set; }
        //[Required]
        //[StringLength(20, MinimumLength = 8)]
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }

        public IEnumerable<SelectListItem> UserLevel { get; set; }
    }
}