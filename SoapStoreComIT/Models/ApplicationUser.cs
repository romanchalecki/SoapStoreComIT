using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SoapStoreComIT.Models
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Street Adress")]
        public string StreetAdress { get; set; }

        //[Display(Name = "Apartment Number")]
        //public string ApartmentNumber { get; set; }


        //[Display(Name = "State/Province")]
        //public string Province { get; set; }


        //[Display(Name = "City")]
        //public string City { get; set; }


        //[Display(Name = "Postal Code")]
        //public string PostalCode { get; set; }


        //[Display(Name = "Country")]
        //public string Country { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "First Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
