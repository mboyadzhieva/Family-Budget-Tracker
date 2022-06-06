namespace FBT.WebAPI.Infrastructure
{
    using AutoMapper;
    using FBT.WebAPI.Data;
    using FBT.WebAPI.Data.Models;
    using FBT.WebAPI.Features.Budget;
    using FBT.WebAPI.Features.Expenses;
    using FBT.WebAPI.Features.Identity;
    using FBT.WebAPI.Features.Incomes;
    using FBT.WebAPI.Features.RecurringExpenses;
    using FBT.WebAPI.Features.RecurringIncomes;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Text;

    public static class ServiceCollectionExtension
    {
        public static AppSettings GetAppSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettingsConfig = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsConfig);

            return appSettingsConfig.Get<AppSettings>();
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddDbContext<FamilyBudgetTrackerDbContext>(options => options
                        .UseSqlServer(configuration
                            .GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 4;
                })
                .AddEntityFrameworkStores<FamilyBudgetTrackerDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services
               .AddTransient<IIdentityService, IdentityService>()
               .AddTransient<IExpenseService, ExpenseService>()
               .AddTransient<IIncomeService, IncomeService>()
               .AddTransient<IRecurringExpenseService, RecurringExpenseService>()
               .AddTransient<IRecurringIncomeService, RecurringIncomeService>()
               .AddTransient<IBudgetService, BudgetService>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Family Budget Tracker API",
                        Version = "v1"
                    });
            });

        public static IServiceCollection AddAutoMapper(this IServiceCollection sevices)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            sevices.AddSingleton(mapper);

            return sevices;
        }

    }

}
