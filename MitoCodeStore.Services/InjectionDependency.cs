using System;
using Microsoft.Extensions.DependencyInjection;
using MitoCodeStore.DataAccess.Repositories;
using MitoCodeStore.Services.Implementations;
using MitoCodeStore.Services.Interfaces;

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

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ISaleService, SaleService>();

            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();

            services.AddTransient<IUserService, UserService>();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                services.AddTransient<IFileUploader, FileUploader>();
            else
                services.AddTransient<IFileUploader, AzureBlobStorageUploader>();

            return services.AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<ICustomerService, CustomerService>();


        }
    }
}