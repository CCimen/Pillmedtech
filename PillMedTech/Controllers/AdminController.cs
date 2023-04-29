using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillMedTech.Models;

namespace PillMedTech.Controllers
{
  /* Autentisering: 3.1: Jobba med IdentityRole och User*/
  /* Autentisering 3.3: Försök till åtkomst via url går automatiskt till login sida*/
  /* Auktorisering: 4.1.1 Jobba med Authorize och Roles*/
  /* Auktorisering: 4.1 Begränsa behörighet*/
  [Authorize(Roles = "ITStaff, HRStaff")]
  public class AdminController : Controller
  {
    private IPillMedTechRepository repository;
    private IHttpContextAccessor contextAcc;

    public AdminController(IPillMedTechRepository repo, IHttpContextAccessor ctxAcc)
    {
      repository = repo;
      contextAcc = ctxAcc;
    }

    //Visa upp söksidan för HR-personal
    public ViewResult HRStaff()
    {
      return View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    /* Datainhämtning: 2.5.2: ValidateAntiForgeryToken Skydd mot CSRF*/
    [ValidateAntiForgeryToken]
    public ViewResult EmployeeInfo(SickErrand errand)
    {
      var errandsList = repository.SortedErrands(errand.EmployeeID);
      repository.Log(DateTime.Now,
                             HttpContext.Connection.RemoteIpAddress.ToString(),
                             contextAcc.HttpContext.User.Identity.Name,
                             $"Searched for {errand.EmployeeID}");
      return View(errandsList);
    }


    public ViewResult ITStaff()
    {
        var loggList = repository.ViewLog();
        repository.Log(DateTime.Now,
            HttpContext.Connection.RemoteIpAddress.ToString(),
            contextAcc.HttpContext.User.Identity.Name,
            "Looked at logglist");

        return View(loggList);
    }
    }
}
