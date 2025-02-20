using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BaiTaptrenlop.Models;

namespace BaiTaptrenlop.Controllers
{
    public class SubjectController : Controller
    {
        [HttpGet]
        public IActionResult SubjectIndex()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubjectIndex(SubjectModels ps)
        {
            ps.SubjectScore =((ps.DiemA*6 + ps.DiemB*4 + ps.DiemC*1)/10.0f);
            ViewBag.Message=$"{ps.Name}, {ps.Subject},Diem la: {ps.SubjectScore:F2} ";
            return View();
        }
    }
}