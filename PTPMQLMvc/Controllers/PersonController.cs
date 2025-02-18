using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PTPMQLMvc.Models;

namespace PTPMQLMvc.Controllers;

public class PersonController : Controller
{
    public IActionResult Index(){
        return View();
    }
    [HttpPost]
    public  IActionResult Index(Person ps)
    {
        string strOutput = "Xin chao" + ps.PersonId + "-" + ps.FullName + "-" + ps.Address;
        ViewBag.infoPerson = strOutput;
        return View();
    }
    private readonly ILogger<PersonController> _logger;

    public PersonController(ILogger<PersonController> logger)
    {
        _logger = logger;
    }

    public IActionResult PersonIndex()
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
    }
}
