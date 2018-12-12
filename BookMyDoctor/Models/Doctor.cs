using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyDoctor.Models
{
    public class DoctorModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Display(Name = "Qualification")]
        [Required(ErrorMessage = "Qualification is required.")]
        public string Qualification { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact is required.")]
        public string Contact_Number { get; set; }

        [Display(Name = "Email ID")]
        public string Email_id { get; set; }

        [Display(Name = "Alternative Contact Number")]
        public string Alternative_Number { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        public string Image { get; set; }

        public int Status { get; set; }


    }
}