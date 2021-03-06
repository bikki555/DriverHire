using DriverHire.Repository.Interfaces;
using DriverHire.Repository.Repository;
using DriverHire.Services;
using DriverHire.Services.Infrastructure;
using DriverHire.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DriverHire.Services.Services.IBookingServices;

namespace DriverHire.Api.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IRandomNumberGeneratorServices, RandomNumberGeneratorServices>();
            services.AddScoped<IImageProcessingServices, ImageProcessingServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IApplicationRoleSevices, ApplicationRoleServices>();
            services.AddScoped<IBookingServices, BookingServices>();
            services.AddScoped<IDriverFormServices, DriverFormServices>();
            services.AddScoped<IRegisterServices, RegisterServices>();
            services.AddScoped<IUserRegistrationServices, UserRegistrationServices>();
            services.AddScoped<IFeedBackServices, FeedBackServices>();
            services.AddScoped<IPushNotificationGenerator, PushNotificationGenerator>();
            return services;
        }

        public static IServiceCollection AddRepositorysDI(this IServiceCollection services)
        {
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IDriverFormRepository, DriverFormRepository>();
            services.AddScoped<IRegisterRepository, RegisterRepository>();
            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddScoped<IFeedBackRepository, FeedBackRepository>();

            return services;
        }


        public static IServiceCollection AddAuthenticationDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(JwtGenerator));
            return services;
        }

    }
}
