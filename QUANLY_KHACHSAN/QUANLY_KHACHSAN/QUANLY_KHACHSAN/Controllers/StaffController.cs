// StaffController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;  // Add this using statement
using QUANLY_KHACHSAN.Filters;
using static QUANLY_KHACHSAN.Models.AuthorizationModel;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.Controllers
{
    [CustomAuthorization(UserRole.Staff)]
    public class StaffController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
    
        public IActionResult Letan()
        {
            return View();
        }
        public IActionResult Baove()
        {
            return View();
        }
        public IActionResult Nhabep()
        {
            return View();
        }
        public IActionResult Tapvu()
        {
            return View();
        }
    }
}
