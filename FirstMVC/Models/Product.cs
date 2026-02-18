using Microsoft.EntityFrameworkCore;

namespace FirstMVC.Models
{
    public class Product
    {

        public int ProductID { get; set; }
        public string StripeProductId { get; set; }

        public string StripePriceId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string? ProductImage { get; set; }
        public int ProductPrice { get; set; }

        public Product()
        {

        }
    }
}
