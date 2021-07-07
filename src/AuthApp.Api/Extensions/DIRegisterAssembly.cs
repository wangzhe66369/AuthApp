using AuthApp.Api.HttpContextUser;
using AuthApp.Domian.Identity;
using AuthApp.Domian.IRepositories;
using AuthApp.EntityFrameworkCore;
using AuthApp.EntityFrameworkCore.Identity;
using AuthApp.Users.Dto;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Reflection;

namespace AuthApp.Extensions
{
    /// <summary>
    /// Automapper 启动服务
    /// </summary>
    public static class DIRegisterAssembly
    {
        public static void AddDIService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            var assembliesToScanService = new[]   {
                 Assembly.GetExecutingAssembly(),
                 Assembly.GetAssembly(typeof(UserOutputDto)),
            };
            //自动注入服务到依赖注入容器
            var p = services.RegisterAssemblyPublicNonGenericClasses(assembliesToScanService)//将获取到的程序集信息注册到我们的依赖注入容器中
             .Where(c => c.Name.EndsWith("Service"))
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
        }

        public static void AddDIRepository(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            var assembliesToScanRepository = new[]   {
                 Assembly.GetExecutingAssembly(),
                 Assembly.GetAssembly(typeof(Repository)),
                 Assembly.GetAssembly(typeof(IRepository)),
            };
            //自动注入服务到依赖注入容器
            var p1 = services.RegisterAssemblyPublicNonGenericClasses(assembliesToScanRepository)//将获取到的程序集信息注册到我们的依赖注入容器中
             .Where(c => c.Name.EndsWith("Repository"))
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();

        }
    }
}
