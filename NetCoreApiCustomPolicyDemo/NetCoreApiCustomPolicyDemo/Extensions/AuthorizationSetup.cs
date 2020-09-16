using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCoreApiCustomPolicyDemo.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiCustomPolicyDemo.Extensions
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 配置策略授权
            services.AddAuthorization(o =>
            {
                o.AddPolicy("CustomAuthorize", o =>
                   o.Requirements.Add(new CustomAuthorizePolicyRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, CustomAuthorizePolicyHandler>();

            //string key = Appsettings.app(new string[] { "JwtSettings", "SecurityKey" });
            //string issuer = Appsettings.app(new string[] { "JwtSettings", "Issuer" });
            //string audience = Appsettings.app(new string[] { "JwtSettings", "Audience" });


            services.AddAuthentication("Bearer").AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    //是否秘钥认证
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is a security key")),

                    //是否验证发行人
                    //ValidateIssuer = true,
                    //发行人
                    //ValidIssuer = issuer, 
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    //是否验证订阅
                    //ValidateAudience = true,
                    //ValidAudience = audience,

                    //是否验证过期时间
                    //RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
