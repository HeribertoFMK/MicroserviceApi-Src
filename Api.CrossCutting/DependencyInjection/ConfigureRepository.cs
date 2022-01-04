using Api.Domain.Interfaces;
using Api.Data.Repository;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql("Server=localhost;port=3306;Database=Api;Uid=root;Pwd=190981ju")

            );
        }
    }
}
