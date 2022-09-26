using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Infraestructure.Persistence.Context;
using solvexTecnical.Infraestructure.Persistence.Repositories;

namespace solvexTecnical.Infraestructure.Persistence
{
    public static class AddPersistenceConfiguration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("Database"),
                        m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //dependency injections
            services.AddTransient(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddTransient<IProductsBrandsRepository, ProductsBrandsRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ISuperMarketRepository, SuperMarketRepository>();
            services.AddTransient<IFinalProductsRepository, FinalProductsRepository>();
            services.AddTransient<IShoppingListProductsRepository, ShoppingListProductsRepository>();
            services.AddTransient<IShoppingListRepository, ShoppingListRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
        }

    }
}
