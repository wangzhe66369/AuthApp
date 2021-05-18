using AuthApp.Authors.Dto;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AuthApp.ServiceExtensions
{
    /// <summary>
    /// Automapper 启动服务
    /// </summary>
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddAutoMapper(typeof(AutoMapperSetup));
        }
    }
}
