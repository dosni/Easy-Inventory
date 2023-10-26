using ServiceLayer.ProductCategoryServices.Concrete;
using ServiceLayer.ProductService.Concrete;
using ServiceLayer.ProductTransactionServices.Concrete;
using ServiceLayer.ProductUnitServices.Concrete;
using ServiceLayer.StoreServices.Concrete;

namespace Stock.Settings.Extensions
{
    public static class StockServiceExtensions
    {
        public static void AddStockServices(this IServiceCollection services)
        {
            services.AddScoped<UnitServices>();
            services.AddScoped<InitStockServices>();
            services.AddScoped<CategoryServices>();
            services.AddScoped<ProductServices>();
            services.AddScoped<ProductSKUServices>();
            services.AddScoped<StoreServices>();
        }
    }
}
