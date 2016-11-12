using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace KennedyLabsWebsite.Controllers
{
    public class AccountController : Controller
    {
        private string CallbackUrl =>
            Url.Action("SignedOut", "Account", values: null, protocol: Request.Scheme);

        public IActionResult SignIn() =>
            Challenge(new AuthenticationProperties { RedirectUri = "/" },
                OpenIdConnectDefaults.AuthenticationScheme);

        public IActionResult SignOut() =>
            SignOut(new AuthenticationProperties { RedirectUri = CallbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);

        public IActionResult SignedOut() =>
            HttpContext.User.Identity.IsAuthenticated ?
                RedirectToAction(nameof(HomeController.Index), "Home") as IActionResult : View();
    }
}
