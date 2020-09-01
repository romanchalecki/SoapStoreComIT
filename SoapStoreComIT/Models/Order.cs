using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoapStoreComIT.Models
{
    public class Order
    {
        public Order()
        {
            DeliveryCost = 5;
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name = "Total")]
        public decimal OrderTotal { get; set; }

        [Required]
        [Display(Name ="Delivery Cost")]
        public decimal DeliveryCost { get; set; }

        public string Status { get; set; }

        public string PaymentStatus { get; set; }

        public string Comments { get; set; }

        [Display(Name = "Delivery Name")]
        public string PickupName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Street Adress")]
        public string StreetAdress { get; set; }

        [Display(Name = "Apartment Number")]
        public string ApartmentNumber { get; set; }

        [Required]
        [Display(Name = "State/Province")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

    }
}
