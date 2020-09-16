using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetCoreApiCustomPolicyDemo.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiCustomPolicyDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
        [Route(nameof(GetToken))]
        public string GetToken(string userRole) 
        {
            string key = "this is a security key";
            SecurityToken securityToken = new JwtSecurityToken(
                            //issuer: "issuer", //签发人
                            //audience: "audience", //受众
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256), //秘钥
                            //过期时间
                            expires: DateTime.Now.AddHours(1),

                            //自定义JWT字段
                            claims: new Claim[] {
                                    new Claim("Role",userRole), //把模拟请求的角色权限添加到Role中
                                    //下面的都是自定义字段，可以任意添加到Claim作为信息共享
                                    new Claim("Name","TestUser"),
                            });
            return JsonConvert.SerializeObject(new { token = new JwtSecurityTokenHandler().WriteToken(securityToken) });
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(TestAuth))]
        public string TestAuth() 
        {
            return "success";
        }

        [CustomAuthorize("TestRole")]
        [HttpGet]
        [Route(nameof(TestRole))]
        public string TestRole() 
        {
            return "success";
        }
    }
}
