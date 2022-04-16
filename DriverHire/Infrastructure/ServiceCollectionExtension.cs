using DriverHire.Repository.Interfaces;
using DriverHire.Repository.Repository;
using DriverHire.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverHire.Api.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IApplicationRoleSevices, ApplicationRoleServices>();
            services.AddScoped<IBookingServices, BookingServices>();
            services.AddScoped<IDriverFormServices, DriverFormServices>();
            return services;
        }

        public static IServiceCollection AddRepositorysDI(this IServiceCollection services)
        {
            services.AddScoped<IBookingRepository, BookingRepository>();
            return services;
        }


        public static IServiceCollection AddAuthenticationDI(this IServiceCollection services)
        {
            return services;
        }

    }
}
