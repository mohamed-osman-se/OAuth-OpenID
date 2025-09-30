using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using OAuthGitHubDemo.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly AppUser appUser;
    private readonly IJwtTokenService _jwtService;

    public AccountController(AppUser appUser, IJwtTokenService jwtService)
    {
        this.appUser = appUser;
        _jwtService = jwtService;
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult GoogleLogin()
    {
        var props = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleCallback")
        };

        return Challenge(props, GoogleDefaults.AuthenticationScheme);
    }
public async Task<IActionResult> GoogleCallback()
{
   
    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    if (!result.Succeeded || result.Principal == null)
        return RedirectToAction("Login");


    appUser.Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
    appUser.Name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;
    appUser.ProviderId = result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    appUser.Provider = "google";

    if (string.IsNullOrEmpty(appUser.Email) || string.IsNullOrEmpty(appUser.ProviderId))
        return BadRequest("Google login failed: missing claims.");

  
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, appUser.ProviderId),
        new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
        new Claim("name", appUser.Name),
        new Claim("provider", appUser.Provider)
    };

   
    var jwt = _jwtService.GenerateToken(claims);

   
    Response.Cookies.Append("AuthToken", jwt, new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.AddMinutes(60)
    });

   
    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);
    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    return Redirect("/Home/Dashboard");
}

    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");

        return Redirect("/Account/Login");
    }
}
