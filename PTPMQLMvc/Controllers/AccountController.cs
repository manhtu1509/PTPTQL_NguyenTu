using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using PTPMQLMvc.Models;
using PTPMQLMvc.Models.Process;
using PTPMQLMvc.Models.ViewModel;
using System.Security.Claims;

namespace PTPMQLMvc.Controllers
{
    [Authorize( Policy ="PolicyByPhoneNumber")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Policy = nameof(SystemPermission.AccountView))]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userWithRoles = new List<UserWithRoleVM>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userWithRoles.Add(new UserWithRoleVM { User = user, Roles = roles.ToList() });
            }
            return View(userWithRoles);
        }
        [Authorize(Policy = nameof(SystemPermission.AssignRole))]
        public async Task<IActionResult> AssignRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.Select(r => new RoleVM { Id = r.Id, Name = r.Name }).ToListAsync();
            var ViewModel = new AssignRolesVM
            {
                UserId = userId,
                ALLRoles = allRoles,
                SelectionRoles = userRoles,
            };
            return View(ViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRolesVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in model.SelectionRoles)
                {
                    if (!userRoles.Contains(role))
                    {
                        await _userManager.AddToRoleAsync(user, role);

                    }

                }
                foreach (var role in userRoles)
                {
                    if (!model.SelectionRoles.Contains(role))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                }
                return RedirectToAction("Index", "Account");
            }
            return View(model);
        }
        public async Task<IActionResult> AddClaim(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var model = new UserClaimVM(userId, user.UserName, userClaims.ToList());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddClaim(string userId, string claimType, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
            if (result.Succeeded)
            {
                return RedirectToAction("AddClaim", new { userId });

            }
            return View();
        }

    }
}   