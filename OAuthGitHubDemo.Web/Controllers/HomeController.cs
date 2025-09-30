using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//
// 
namespace OAuthGitHubDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
      [Authorize]
public IActionResult Dashboard()
{
    return View();
}


    }
}
