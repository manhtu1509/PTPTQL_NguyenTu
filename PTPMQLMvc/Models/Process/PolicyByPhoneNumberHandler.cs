using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PTPMQLMvc.Models;


public class PolicyByPhoneNumberRequirement : IAuthorizationRequirement { }

public class PolicyByPhoneNumberHandler : AuthorizationHandler<PolicyByPhoneNumberRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PolicyByPhoneNumberHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyByPhoneNumberRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
            return;

        var userManager = httpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.GetUserAsync(context.User);

        if (user != null && !string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            context.Succeed(requirement);
        }
    }
}
