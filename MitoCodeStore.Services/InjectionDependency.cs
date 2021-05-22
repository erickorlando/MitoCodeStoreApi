using Microsoft.Extensions.DependencyInjection;
using MitoCodeStore.DataAccess;

namespace MitoCodeStore.Services
{
    public static class InjectionDependency
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            // Transient => Por request

            // Scoped => En el mismo request si tengo mas de un objeto del mismo tipo, entonces se utiliza una sola instancia.

            // Singleton => Se utiliza la misma instancia para todos los request.

            // STATELESS = SIN ESTADO.

            return services.AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<ICustomerService, CustomerService>();
        }
    }
}