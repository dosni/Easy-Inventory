using ServiceLayer.ContactServices.Concrete;
using ServiceLayer.ProductCategoryServices.Concrete;
using ServiceLayer.ProductService.Concrete;
using ServiceLayer.ProductStockServices.Concrete;
using ServiceLayer.ProductTransactionServices.Concrete;
using ServiceLayer.ProductUnitServices.Concrete;
using ServiceLayer.StoreServices.Concrete;
using ServiceLayer.TransactionServices.Concrete;
using System.Transactions;

namespace Stock.Settings.Extensions
{
    public static class StockServiceExtensions
    {
        public static void AddStockServices(this IServiceCollection services)
        {
            services.AddScoped<UnitServices>();
            services.AddScoped<CategoryServices>();
            services.AddScoped<ProductServices>();
            services.AddScoped<ProductSKUServices>();
            services.AddScoped<StoreServices>();

            services.AddScoped<ReceiveStockServices>();
            services.AddScoped<StockServices>();
            services.AddScoped<ContactServices>();

            services.AddScoped<TransactionServices>();
            services.AddScoped<TransactionLineServices>();
        }
    }
}
