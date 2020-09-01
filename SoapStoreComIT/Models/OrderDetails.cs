using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoapStoreComIT.Models
{
    public class OrderDetails
    {
        public OrderDetails()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [Required]
        public int StoreItemId { get; set; }

        [ForeignKey("StoreItemId")]
        public virtual StoreItem StoreItem { get; set; }

        public int Count { get; set; }

        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
