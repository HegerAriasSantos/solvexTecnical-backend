using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using solvexTecnical.Core.Application.Services;
using System.Reflection;

namespace solvexTecnical.Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services Injection
            services.AddTransient(typeof(ICommonService<,>), typeof(CommonServices<,>));
            services.AddTransient<IShoppingList, ShoppingListServices>();
            services.AddTransient<IProductsServices, ProductsServices>();
            services.AddTransient<ISuperMarketService, SuperMarketServices>();
            services.AddTransient<IUserServices, UsersServices>();
            services.AddTransient<IBrandService, BrandServices>();
            #endregion
        }
    }
}
