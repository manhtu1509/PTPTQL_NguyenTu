using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTPMQLMvc.Models;
using PTPMQLMvc.Data;

namespace PTPMQLMvc.Controllers;

public class PersonController : Controller
{
    private readonly ApplicationDbContext _context;
    public PersonController(ApplicationDbContext context){
        _context = context;
    }
    public async Task<IActionResult> Index(){
        var model = await _context.Person.ToListAsync();
        return View(model);
    }
    public IActionResult Create()
    {
        return View();
    }
    public IActionResult Index(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>create([Bind("Person,Fullname,Address")] Person person)
    {
        if (ModelState.IsValid)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(index));

        }
        return View(person);

    }
    public async Task<IActionResult>Edit(string id)
    {
        if (id == null || _context.Person ==  null)
        {
            return NotFound();
        }
        return View(person);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Edit(string id, [Bind("PersonId, Fullname, Address")] Person person)
    {
        if (id != person.PersonId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try{
                _context.Update(person);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(person);//đã đến slide 52
    }
    public async Task<IActionResult>Delete(string id);
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)///Manhj tu ne
    
    private bool PersonExists(string id);
    [HttpPost]
    
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
