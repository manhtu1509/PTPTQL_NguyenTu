using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BaiTaptrenlop.Models;
namespace BaiTaptrenlop.Controllers
{
    public class BillController : Controller
    {
        [HttpGet]
        public IActionResult BillIndex()
        {
            return View();

        }
        [HttpPost]
        public IActionResult BillIndex(BillModels ps)
        {
            float ThanhTien = ps.SoLuong * ps.GiaBan;
            ViewBag.Message= $"{ps.TenHang}, So Tien La: {ThanhTien}";
            return View();

        }
        

        
    }
}