using Microsoft.AspNetCore.Mvc;

namespace PillMedTech.Controllers
{
  public class ErrorController : Controller
  {
    public IActionResult CustomerError()
    {
      return View();
    }
  }
}
