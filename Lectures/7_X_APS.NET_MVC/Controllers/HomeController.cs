using APS.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

/*razor
mvc 
blazer
proj in mvc
after mid through razor

Model view controller

VIEW
front end html razor bootstrp asp .net
equilent to presentation layer of mvc
input and output through view 

CONTROLLER
control flow of application
when data is added through view it should go tot view

VIEW ------> CONTROLLLER

MODEL
Database related operations and bussiness logic related operations
NO LOGIC IN CONTROLLER CONTROLLER MUST BE SIMPLE AND CLEAN 
IT WILL BE A ABSOLUTE COMMUNICATER

LET SAY we have an html form
USERNAME
PASSWORD 
SIGN IN BTN

When button presses go to controller
and receive data in controller

controller can not go to db directly
it goes to model who will verify login by interacting with db
reult will be returned to model
which will not go to view  directly
it will goto view via controller

VALIDATION 
2 TYPES
client end server side
Client side in browser
erver side some in controller
annotations in the model describe the validations

validation rules are defined in  model classes
N-TIER DESKTOP
MVC FOR WEB
Bussiness objects equilent in models

CONTROLLER is a brain decideding what actionto be performed against requests form view.
Either to go to model or not.
example of not going is need to show a message.that can be sent back from controller.
for example from action and method can help us decide

A VIEW CAN NEED DATA FROM MULTIPLE MODELS
we will use concept of virew model.
model that is according to a particular view. a new class view model class
properties required in this model(view model class), model related to a particular view.
combination of classes and thier particular properties required to us in particular view.



ASP.NET CORE WEB APPLICATION
MVC SPA WEB API -> INTENSIVELY USED IN WEB
*/


namespace APS.MVC.Controllers
{
    //NAMIGN CONVENTION controllerName+Controller ans UserController, ProductController
    //inherit it from Controller class

    //A controller contain different action methods.
    //Actions are to be performed by controller
    //we will define action methods for them.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //Action methods index privacy error
        /*public IActionResult Index()
        {
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
        }*/
        
        //by defult it will find home controller and index method in it
        //if you wirte home and index in browser you'will be dircted to +the index action method.

        //IIS internet express server
        public string Index()
        {
            return "HELLOW WORLD + My First app in mvc 01 999";
        }
        public string AboutUs()
        {
            return "ABOUT US";
        }
    }
}