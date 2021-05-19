using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthApp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AuthApp.Web;
using AuthApp.ServiceExtensions;
using System.Reflection;
using AuthApp.Authors.Dto;
using NetCore.AutoRegisterDi;
using AuthApp.EntityFrameworkCore.Repositories;
using AuthApp.Domian.IRepositories;
using AuthApp.Authors;
using AuthApp.Books;
using Autofac;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthApp.Configuration;
using AuthApp.Domian;
using Microsoft.AspNetCore.Identity;
using AuthApp.CustomerMiddlewares;
using AuthApp.Extensions;
using AuthApp.Authorization.Users;
using AuthApp.Authorization.Roles;

namespace AuthApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();

            //context ��
            services.AddDbContextPool<AppDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString(AuthAppConsts.ConnectionStringName),
                optionBuilder=> optionBuilder.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name)
                );
            });

            services.AddAutoMapperSetup();

            #region ��������ע����������
            services.AddDIService();
            services.AddDIRepository();
            services.AddScoped<DbContext, AppDbContext>();
            #endregion ��������ע����������

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });
            services.AddIdentity<User, Role>()//AddIdentity���������������UserManager��RoleManager���Լ������������ķ���
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>();

            
            services.AddAuthentication_JWTSetup();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthApp", Version = "v1" });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthApp v1"));
            }
            app.UseAuthentication();//����mvcǰ���м�Ȩ,UseRouting()�м��֮ǰ�����֤�м��
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
