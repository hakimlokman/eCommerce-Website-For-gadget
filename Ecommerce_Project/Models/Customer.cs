namespace Ecommerce_Project.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Mobile     { get; set; } = default!;
        public string Address { get; set; } = default!;
        public List<SalesOrder> SalesOrders  { get; set; } = new List<SalesOrder>();
    }
}
