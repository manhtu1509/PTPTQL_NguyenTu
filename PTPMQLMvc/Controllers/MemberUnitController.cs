using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTPMQLMvc.Data;
using PTPMQLMvc.Models.Entities;
using Microsoft.AspNetCore.Authorization;

[Authorize(Policy = "Permission")]
public class MemberUnitController : Controller
{
    private readonly ApplicationDbContext _context;
    public MemberUnitController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.memberUnits.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var memberUnit = await _context.memberUnits
            .FirstOrDefaultAsync(m => m.MemberUnitId == id);
        if (memberUnit == null)
        {
            return NotFound();
        }

        return View(memberUnit);
    }

    [Authorize(Policy = "Role")]
    // GET: MemberUnit/Create
    public IActionResult Create()
    {
        return View();
    }
}
