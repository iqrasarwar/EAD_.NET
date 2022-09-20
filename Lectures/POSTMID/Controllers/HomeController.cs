using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateManagment.Models;

namespace StateManagment.Controllers;

public class HomeController : Controller
{
   private readonly ILogger<HomeController> _logger;

   public HomeController(ILogger<HomeController> logger)
   {
      _logger = logger;
   }


   public IActionResult Index()
   {
      if (!HttpContext.Request.Cookies.ContainsKey("request_one"))
      {
         CookieOptions options = new CookieOptions();
         options.Expires = DateTime.Now.AddDays(1);
         HttpContext.Response.Cookies.Append("request_one", DateTime.Now.ToString(), options);
         // HttpContext.Response.Cookies.Append("request_one", DateTime.Now.ToString());
      }
      else
      {
         DateTime firstVisitedDateTime = DateTime.Parse(HttpContext.Request.Cookies["request_one"]);
         Object data = "welcome back user, your fist visit was on : " + firstVisitedDateTime.ToString();
         HttpContext.Response.Cookies.Delete("request_one");
         return View(data);
      }
      return View();
   }

   public IActionResult Privacy()
   {
      object data = string.Empty;
      if (HttpContext.Session.Keys.Contains("first_request_of_session"))
      {
         object firstVisitedDateTime = HttpContext.Session.GetString("first_request_of_session");
         data = "welcom back " + firstVisitedDateTime;
      }
      else
      {
         data = "1st visit";
         HttpContext.Session.SetString("first_request_of_session", System.DateTime.Now.ToString());
      }
      return View(data);
   }

   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   public IActionResult Error()
   {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
   }
}
