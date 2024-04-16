namespace Ecommerce_Project.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public double Price { get; set; }
        public double Quentity { get; set; }
        public string Picture { get; set; } = default!;
        public List<SalesItem> SalesItems { get; set; }=new List<SalesItem>();
    }
}
