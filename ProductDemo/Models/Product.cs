using System.Diagnostics;

namespace ProductDemo.Models
{
    public partial class Product
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set;}
    }

}
