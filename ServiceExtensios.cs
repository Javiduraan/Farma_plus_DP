using Farma_plus.Interfaces;
using Farma_plus.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Farma_plus
{
    public static class ServiceExtensios
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITratamientoRepository, TratamientoRepository>();
            services.AddTransient<ICatalogoArticulos, CatalogoArticulosRepository>();
            services.AddTransient<IDatosGralesRepository, DatosGralesRepository>();
            services.AddTransient<IHistorialCitasRepository, HistorialCitasRepository>();
            services.AddTransient<ISurtidoFarmaAlternoRepository, SurtidoFarmaAlternoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
