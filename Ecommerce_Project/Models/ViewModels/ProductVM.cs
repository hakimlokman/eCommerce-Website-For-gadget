namespace Ecommerce_Project.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public double Price { get; set; }
        public double Quentity { get; set; }
        public string? Picture { get; set; } 
        public IFormFile? PictureFile { get; set; } 
    }
}
