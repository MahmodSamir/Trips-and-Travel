using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trips_and_Travel.Models
{
    [MetadataType(typeof(CustomizeAttr))]
    public partial class User
    {
    }

    public class CustomizeAttr
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "*")]
        public string userName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*")]
        public string Fname { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*")]
        public string Lname { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b", ErrorMessage = "Invalid Email Format.")]
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }

        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})", ErrorMessage = "Weak Password.")]
        [Required(ErrorMessage = "Must enter password.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Must be between 8 to 16 characters.")]
        public string Password { get; set; }

        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }
        public string Photo { get; set; }
        
        [Required]
        public string Role { get; set; }
    }
}