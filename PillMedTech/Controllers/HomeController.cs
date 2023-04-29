using Microsoft.AspNetCore.Mvc;


namespace PillMedTech.Controllers
{
  public class HomeController : Controller
  {
    public ViewResult Index()
    {
      return View();
    }
  }
}