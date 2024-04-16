using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Project.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public string OrderNo { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        [ForeignKey("Customer")]
        public  int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public List<SalesItem> SalesItems { get; set; }=new List<SalesItem>();
    }
}
