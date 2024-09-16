// StaffController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;  // Add this using statement
using QUANLY_KHACHSAN.Filters;
using static QUANLY_KHACHSAN.Models.AuthorizationModel;

namespace QUANLY_KHACHSAN.Controllers
{
    [CustomAuthorization(UserRole.Staff)]
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
