using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using QUANLY_KHACHSAN.ViewModels;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.ViewModels;

namespace QUANLY_KHACHSAN.Controllers
{
    public class SaleReportController : Controller
    {

        private readonly ISaleReportRepository _dbsalereport;
        public SaleReportController(ISaleReportRepository _context)
        {
            _dbsalereport = _context;
        }
        public async Task<IActionResult> Index(string sortOrder, int manager)
        {
            TempData["Manager"] = manager;
            ViewBag.Month = DateTime.Now.Month;

            List<SaleReportViewModel> salerp = await _dbsalereport.GetSaleReportForMonthYear(ViewBag.Month);

            // Sorting logic for 'Tỷ lệ'
            ViewData["TyleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "tyle_desc" : "";

            switch (sortOrder)
            {
                case "tyle_desc":
                    salerp = salerp.OrderByDescending(s => s.tyle).ToList();
                    break;
                default:
                    salerp = salerp.OrderBy(s => s.tyle).ToList();
                    break;
            }

            return View(salerp);
        }


        [HttpPost]
        public async Task<IActionResult> Index(int month, string sortOrder, int manager)
        {
            TempData["Manager"] = manager;
            ViewBag.Month = month;

            List<SaleReportViewModel> salerp = await _dbsalereport.GetSaleReportForMonthYear(ViewBag.Month);

            // Sorting logic for 'Tỷ lệ'
            ViewData["TyleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "tyle_desc" : "";

            switch (sortOrder)
            {
                case "tyle_desc":
                    salerp = salerp.OrderByDescending(s => s.tyle).ToList();
                    break;
                default:
                    salerp = salerp.OrderBy(s => s.tyle).ToList();
                    break;
            }

            return View(salerp);
        }



    }
}
