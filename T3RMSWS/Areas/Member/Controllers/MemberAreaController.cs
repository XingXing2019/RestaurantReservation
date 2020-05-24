using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using T3RMSWS.Data;

namespace T3RMSWS.Areas.Member.Controllers
{
    [Area("Member")]
    public class MemberAreaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        protected readonly ApplicationDbContext _context;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public MemberAreaController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
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
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return user;
        }

        protected async Task<Person> GetMemberAsync()
        {
            var user = await GetIdentityUserAsync();
            var person = await _context.People.FirstOrDefaultAsync(p => p.Id.Equals(user.Id));
            return person;
        }
    }
}