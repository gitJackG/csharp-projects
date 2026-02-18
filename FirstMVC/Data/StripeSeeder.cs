using FirstMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;

namespace FirstMVC.Data
{
    public class StripeSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService _productService;
        private readonly StripeModel _stripeModel;

        public StripeSeeder(
            ApplicationDbContext context,
            ProductService productService,
            IOptions<StripeModel> stripeOptions)
        {
            _context = context;
            _productService = productService;
            _stripeModel = stripeOptions.Value;
        }

        public async Task SeedAsync()
        {
            StripeConfiguration.ApiKey = _stripeModel.SecretKey;

            var options = new ProductListOptions
            {
                Expand = new List<string> { "data.default_price" }
            };

            var stripeProducts = _productService.List(options);

            foreach (var stripeProduct in stripeProducts.Data)
            {
                var exists = await _context.Product
                    .AnyAsync(p => p.StripeProductId == stripeProduct.Id);

                if (!exists)
                {
                    var price = stripeProduct.DefaultPrice as Price;

                    var newProduct = new Models.Product
                    {
                        StripeProductId = stripeProduct.Id,
                        StripePriceId = price.Id,
                        ProductName = stripeProduct.Name,
                        ProductDescription = stripeProduct.Description,
                        ProductImage = stripeProduct.Images.FirstOrDefault(),
                        ProductPrice = price != null ? (int)(price.UnitAmount ?? 0) : 0
                    };

                    await _context.Product.AddAsync(newProduct);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}