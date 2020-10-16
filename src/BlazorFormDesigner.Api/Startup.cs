using BlazorFormDesigner.BusinessLogic.Interfaces;
using BlazorFormDesigner.BusinessLogic.Services;
using BlazorFormDesigner.Database.Repositories;
using BlazorFormDesigner.Database.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using AutoMapper;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;

namespace BlazorFormDesigner.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            //Database
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value ?? new DatabaseSettings());
            BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();

            //Services
            services.AddTransient<UserService, UserService>();
            services.AddTransient<FormService, FormService>();
            services.AddTransient<AnswerService, AnswerService>();

            services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConventionRegistry.Register("EnumStringConvention", new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, t => true);

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
