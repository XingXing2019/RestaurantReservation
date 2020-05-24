using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using T3RMSWS.Data;

namespace T3RMSWS.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Manager")]
    public class ManagementAreaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        protected readonly ApplicationDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ManagementAreaController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            try
            {
                _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        protected async Task<IdentityUser> GetIdentityUserAsync()
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}