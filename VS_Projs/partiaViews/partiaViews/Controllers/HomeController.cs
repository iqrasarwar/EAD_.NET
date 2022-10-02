using Microsoft.AspNetCore.Mvc;
using partiaViews.Models;
using partiaViews.ViewModels;
using System.Diagnostics;

namespace partiaViews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
           
        public IActionResult Index()
        {
            var user = new List<User>();
            user.Add(new User(34));

            var cus = new List<Customer>();
            cus.Add(new Customer("iqra"));

            var cusUser = new List<CustomerViewModel>();
            var cm = new CustomerViewModel();
            cm.users = user;
            cm.customer = cus;

            cusUser.Add(cm);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}