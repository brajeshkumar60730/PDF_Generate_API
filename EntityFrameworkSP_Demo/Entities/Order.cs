using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkSP_Demo.Entities
{
    public class Order
    {
        [Key]
        public Guid  OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Size { get; set; }
        public int? Quantity { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
