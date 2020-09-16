using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApiCustomPolicyDemo.Security
{
    public class CustomAuthorizePolicyHandler : AuthorizationHandler<CustomAuthorizePolicyRequirement>
    {
        public CustomAuthorizePolicyHandler()
        {

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext authoriazationContext, CustomAuthorizePolicyRequirement requirement)
        {
            if (authoriazationContext.Resource is Endpoint endpoint)
            {
                //var attribute = endpoint.Metadata.GetMetadata<CustomAuthorizeAttribute>();
                var attributeList = endpoint.Metadata.GetOrderedMetadata<CustomAuthorizeAttribute>();
                if (attributeList != null && attributeList.Count > 0)
                {
                    foreach (var attribute in attributeList)
                    {
                        var claim = authoriazationContext.User.Claims.Where(r => r.Type == "Role").FirstOrDefault();
                        if (claim == null || !claim.Value.Equals(attribute.Role, StringComparison.CurrentCultureIgnoreCase)) 
                        {
                            //如果没有权限失败
                            authoriazationContext.Fail();
                            return Task.CompletedTask;
                        }

                        //有权限成功
                        authoriazationContext.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
