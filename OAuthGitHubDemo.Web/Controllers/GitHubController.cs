using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

  [Authorize]
public class GitHubController : Controller
{
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;

    public GitHubController(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Connect()
    {
        var clientId = _config["Authentication:GitHub:ClientId"];
        var redirectUri = Url.Action("Callback", "GitHub", null, Request.Scheme);

        var url = "https://github.com/login/oauth/authorize" +
               $"?client_id={Uri.EscapeDataString(clientId!)}" +
               $"&redirect_uri={Uri.EscapeDataString(redirectUri!)}" +
               "&scope=repo";
        return Redirect(url);
    }


    public async Task<IActionResult> Callback(string code)
    {
        var clientId = _config["Authentication:GitHub:ClientId"];
        var clientSecret = _config["Authentication:GitHub:ClientSecret"];

        var http = _httpClientFactory.CreateClient();

        var tokenResponse = await http.PostAsync("https://github.com/login/oauth/access_token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"client_id", clientId!},
                {"client_secret", clientSecret!},
                {"code", code}
            }));

        var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
        var queryParams = System.Web.HttpUtility.ParseQueryString(tokenContent);
        var accessToken = queryParams["access_token"];


        TempData["GitHubToken"] = accessToken;

        return RedirectToAction("Repos");
    }

    public async Task<IActionResult> Repos()
    {
        var token = TempData["GitHubToken"]?.ToString();
        if (string.IsNullOrEmpty(token)) return RedirectToAction("Connect");

        var http = _httpClientFactory.CreateClient();
        http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await http.GetStringAsync("https://api.github.com/user/repos");
        var repos = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(response);

        return View(repos);
    }
}
