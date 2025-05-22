using Microsoft.AspNetCore.Mvc;
namespace PTPMQLMvc.Controllers
{
    public class HelloWorldController : Controller
    { 
        // GET: /HelloWorld/
        public IActionResult IndexHelloWorld()
        {
            return View();
        } 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is my Welcome action";
        }
    }
}