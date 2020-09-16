using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApiCustomPolicyDemo.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        public CustomAuthorizeAttribute(string role) : base("CustomAuthorize") => Role = role;
    }
}
