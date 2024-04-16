using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Project.Models
{
    public class SalesItem
    {
        public int Id { get; set; }
        [ForeignKey("SalesOrder")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; } = default!;
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public int ProudctId { get; set; }
        public Products Products { get; set; } = new Products();
    }
}
