using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DgWebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using AuthorizePolicy.JWT;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace DgWebAPI
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
            services.AddCors();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ResultHandingMiddleware));
                options.RespectBrowserAcceptHeader = true;
            });
            #region 验证
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//发行人
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //这个集合模拟用户权限表,可从数据库中查询出来
            var permission = new List<Permission> {
                               new Permission {  Url="/", Name="admin"},
                               new Permission {  Url="/api/values", Name="admin"},
                               new Permission {  Url="/", Name="system"},
                               new Permission {  Url="/api/values1", Name="system"}
                           };
            //如果第三个参数，是ClaimTypes.Role，上面集合的每个元素的Name为角色名称，如果ClaimTypes.Name，即上面集合的每个元素的Name为用户名
            var permissionRequirement = new PermissionRequirement(
                "/api/denied", permission,
                ClaimTypes.Role,
                audienceConfig["Issuer"],
                audienceConfig["Audience"],
                signingCredentials,
                expiration: TimeSpan.FromDays(1)//设置Token过期时间
                );
            services.AddAuthorization(options =>
            {

                options.AddPolicy("Permission",
                          policy => policy.Requirements.Add(permissionRequirement));

            }).AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //不使用https
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = tokenValidationParameters;
                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if (context.Request.Path.Value.ToString() == "/api/logout")
                        {
                            var token = ((context as TokenValidatedContext).SecurityToken as JwtSecurityToken).RawData;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);

            #endregion

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddSingleton<IAddressRepository, AddressRepository>();
            services.AddSingleton<IExpressRepository, ExpressRepository>();
            services.AddSingleton<IGoodsRepository, GoodsRepository>();
            services.AddSingleton<IBillRepository, BillRepository>();
            services.AddSingleton<IBillCustomerRepository, BillCustomerRepository>();
            services.AddSingleton<IBillGoodsRepository, BillGoodsRepository>();
            services.AddSingleton<IPositionRepository, PositionRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IShareRespository, ShareRespository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCors(builder => builder.WithOrigins("http://localhost:8888").AllowAnyMethod().AllowAnyHeader());
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
            app.UseErrorHandling();
        }
    }
}
