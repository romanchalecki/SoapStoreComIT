using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoapStoreComIT.Models
{
    public class StoreItem
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="Price should be greater than CA${1}")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public string Ingridients { get; set; }

        [Required]
        public uint Quantity { get; set; }

        public string Image { get; set; }

       
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }


        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }


        public StoreItem()
        {
        }
    }

}
