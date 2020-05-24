using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using T3RMSWS.Data;

namespace T3RMSWS.Areas.Management.Controllers
{
    public class HomeController : ManagementAreaController
    {
        public HomeController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
            : base(userManager, context)
        {
        }
       
        public IActionResult Index()
        {
            return View();
        }

    }
}