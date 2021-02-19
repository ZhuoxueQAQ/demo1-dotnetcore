using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo1.SetUp;
using demo1.Common.Helper;
using Google.Protobuf.WellKnownTypes;

namespace demo1
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
            // swagger
            services.AddSwaggerSetup();

            //ע��appsettings��ȡ��
            services.AddSingleton(new AppSettings(Configuration));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "demo1", Version = "v1" });
            });

                //配置跨域处理
            services.AddCors(options =>
                {
                    //全局起作用
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                        });
                                     
                    options.AddPolicy("AnotherPolicy",
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:8080")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });

                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/V1/swagger.json", "WebApi.Core V1");
                    c.RoutePrefix = "";
                });

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
}
