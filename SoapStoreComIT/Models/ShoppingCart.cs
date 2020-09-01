using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoapStoreComIT.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }

        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int StoreItemId { get; set; }

        [NotMapped]
        [ForeignKey("StoreItemId")]
        public virtual StoreItem StoreItem { get; set; }

        [Range(1, int.MaxValue,ErrorMessage ="Please enter a value rather than or equal to {1}")]
        public int Count { get; set; }


    }
}
