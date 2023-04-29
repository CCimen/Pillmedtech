using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PillMedTech.Models;


namespace PillMedTech.Controllers
{
  [Authorize]
  /* Autentisering: 3.1. Jobba med IdentityUser och IdentityRole */
  public class AccountController : Controller
  {
    private UserManager<IdentityUser> userManager;
    private SignInManager<IdentityUser> signInManager;
    private IPillMedTechRepository repository;
    private IHttpContextAccessor contextAcc;

    public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, IPillMedTechRepository repo, IHttpContextAccessor ctxAcc)
    {
      userManager = userMgr;
      signInManager = signInMgr;
      repository = repo;
      contextAcc = ctxAcc;
    }

    [AllowAnonymous]
    public ViewResult Login(string returnUrl)
    {
      return View(new LoginModel
      {
        ReturnUrl = returnUrl
      });
    }

    /* Autentisering: 3.2 & 3.5: Generellt felmeddelande vid inloggning | Lösenordet ska vara hashad (kryptering).*/
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
      if (ModelState.IsValid)
      {
        IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);

        if (user != null)
        {
          await signInManager.SignOutAsync();

          if ((await signInManager.PasswordSignInAsync(user,
                          loginModel.Password, false, false)).Succeeded)
          {
            if (await userManager.IsInRoleAsync(user, "Employee"))
            {
              repository.Log(DateTime.Now,
                            HttpContext.Connection.RemoteIpAddress.ToString(),
                            user.ToString(),
                            "Sucessfully logged in");
              return Redirect("/Employee/StartEmployee");
            }
            else if (await userManager.IsInRoleAsync(user, "HRStaff"))
            {
              repository.Log(DateTime.Now,
                           HttpContext.Connection.RemoteIpAddress.ToString(),
                           user.ToString(),
                           "Sucessfully logged in");
              return Redirect("/Admin/HRStaff");
            }
            else if (await userManager.IsInRoleAsync(user, "ITStaff"))
            {
              repository.Log(DateTime.Now,
                           HttpContext.Connection.RemoteIpAddress.ToString(),
                           user.ToString(),
                           "Sucessfully logged in");
              return Redirect("/Admin/ITStaff");
            }
            else
            {
              repository.Log(DateTime.Now,
                           HttpContext.Connection.RemoteIpAddress.ToString(),
                           user.ToString(),
                           "Tried to log in, unsuccessfully");
              return Redirect("/Home/Index");
            }
          }
        }
      }
      ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
      repository.Log(DateTime.Now,
                           HttpContext.Connection.RemoteIpAddress.ToString(),
                           loginModel.UserName,
                           "Tried to log in, unsuccessfully");
      return View(loginModel);
    }

    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
      var user = contextAcc.HttpContext.User.Identity.Name;
      await signInManager.SignOutAsync();
      /* Autentisering: 3.2: Ta bort Cookie och session med utloggning.*/
      Response.Cookies.Delete("MyCookie");
      HttpContext.Session.Clear();

      repository.Log(DateTime.Now,
          HttpContext.Connection.RemoteIpAddress.ToString(),
          user.ToString(),
          "Successfully logged out");


      return Redirect(returnUrl);
    }

    /* Auktorisering: 4.2: Försök till åtkomst via url när man är inloggad kommer leda till AccessDenied*/
    [AllowAnonymous]
    public ViewResult AccessDenied()
    {
      var user = contextAcc.HttpContext.User.Identity.Name;

      repository.Log(DateTime.Now,
          HttpContext.Connection.RemoteIpAddress.ToString(),
          user.ToString(),
          "Tried to reach unauthorized view");

      return View();
    }
  }
}
