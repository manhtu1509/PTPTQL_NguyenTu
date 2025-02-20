using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BaiTaptrenlop.Models;

namespace BaiTaptrenlop.Controllers
{
    public class BmiController : Controller
    {
        [HttpGet]
        public IActionResult BmiIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BmiIndex(BMIModels ps)
        {
            float BMI = ps.CanNang / (ps.ChieuCao * ps.ChieuCao);
            ViewBag.Message = $"{ps.HovaTen}, BMI: {BMI:0.##}";
            return View();
        }
        
    }
}
