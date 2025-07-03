using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperCrudApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? Note { get; set; }

        [Required]
        public bool IsActive { get; set; }


        public virtual ICollection<ProductDetails> ProductDetails { get; set; }
    }
}
