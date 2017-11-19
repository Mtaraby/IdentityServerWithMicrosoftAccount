using Fiver.Security.AuthServer.Client.Models.Home;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Fiver.Security.AuthServer.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Movies()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            await testAsync();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/movies");

            var model = JsonConvert.DeserializeObject<List<MovieViewModel>>(content);

            return View(model);
        }

        public IActionResult Claims()
        {
            return View();
        }

        async Task testAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var discoveryClient = new DiscoveryClient("http://localhost:5000/");
            var doc = await discoveryClient.GetAsync();

            UserInfoClient uic = new UserInfoClient("http://localhost:5000/connect/userinfo");
            var result = await uic.GetAsync(accessToken);


            var test = Content(JsonConvert.SerializeObject(result.Claims));


        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
