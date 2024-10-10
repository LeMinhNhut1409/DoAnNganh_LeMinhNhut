using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using QUANLY_KHACHSAN.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QUANLY_KHACHSAN.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly INhanvienRepository _employeeRepo;

        public SecurityController(IVehicleRepository vehicleRepository, INhanvienRepository employeeRepo)
        {
            _vehicleRepository = vehicleRepository;
            _employeeRepo = employeeRepo;
        }

        public async Task<IActionResult> VehicleList(string searchString, string SortOrder, string sortColumn, int pageNumber, string currentFilter, int manager)
        {
            ViewData["sortColumn"] = sortColumn;
            ViewData["sortOrder"] = SortOrder;
            ViewData["MaSortParam"] = sortColumn == "Mabv" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["LicensePlateSortParam"] = sortColumn == "LicensePlate" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["CheckInDateSortParam"] = sortColumn == "CheckInDate" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["CheckOutDateSortParam"] = sortColumn == "CheckOutDate" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            TempData["Manager"] = manager;

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentPageNumber"] = pageNumber;
            var baoves = _vehicleRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                baoves = baoves.Where(r => r.LicensePlate != null && r.LicensePlate.ToLower().Contains(searchString.ToLower()));
            }
            
            switch (sortColumn)
            {
                case "Mabv":
                    baoves = SortOrder == "desc" ? baoves.OrderByDescending(r => r.Mabv) : baoves.OrderBy(r => r.Mabv);
                    break;
                case "LicensePlate":
                    baoves = SortOrder == "desc" ? baoves.OrderByDescending(r => r.LicensePlate) : baoves.OrderBy(r => r.LicensePlate);
                    break;
                case "CheckInDate":
                    baoves = SortOrder == "desc" ? baoves.OrderByDescending(r => r.CheckInDate) : baoves.OrderBy(r => r.CheckInDate);
                    break;
                case "Dongia":
                    baoves = SortOrder == "desc" ? baoves.OrderByDescending(r => r.CheckOutDate) : baoves.OrderBy(r => r.CheckOutDate);
                    break;
                default:
                    baoves = baoves.OrderBy(r => r.Mabv);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 7;

            ViewBag.Manager = manager;

            return View(await PaginatedList<Baove>.CreateAsync(baoves, pageNumber, pageSize));


        }
        [HttpGet]
        // Phương thức GET để thêm xe
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var nhanvienList = await _employeeRepo.GetAllAsync(); // Lấy danh sách nhân viên
            ViewBag.NhanVienList = nhanvienList; // Lưu danh sách vào ViewBag

            return View();
        }

        // Phương thức POST 
        [HttpPost]
        public async Task<IActionResult> Create(Baove baove)
        {
            if (baove.Manv == 0) // Kiểm tra mã nhân viên
            {
                ModelState.AddModelError("Manv", "Mã nhân viên không hợp lệ.");
                return View(baove);
            }

            if (string.IsNullOrEmpty(baove.LicensePlate))
            {
                ModelState.AddModelError("LicensePlate", "Biển số xe không được để trống.");
                return View(baove);
            }

            await _vehicleRepository.AddAsync(baove);
            return RedirectToAction("VehicleList");
        }


        // Phương thức GET 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var baove = await _vehicleRepository.GetByIdAsync(id);
            if (baove == null)
            {
                return NotFound(); 
            }

            var nhanvienList = await _employeeRepo.GetAllAsync(); // Lấy danh sách nhân viên
            ViewBag.NhanVienList = nhanvienList; // Lưu danh sách vào ViewBag

            return View(baove);
        }

        // Phương thức POST 
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Baove baove)
        {
            if (baove.Manv == 0) // Kiểm tra mã nhân viên
            {
                ModelState.AddModelError("Manv", "Mã nhân viên không hợp lệ.");
                return View(baove);
            }

            if (string.IsNullOrEmpty(baove.LicensePlate))
            {
                ModelState.AddModelError("LicensePlate", "Biển số xe không được để trống.");
                return View(baove);
            }

            
            var existingVehicle = await _vehicleRepository.GetByIdAsync(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            // Ghi đè các thuộc tính cần thiết
            existingVehicle.LicensePlate = baove.LicensePlate;
            existingVehicle.CheckInDate = baove.CheckInDate;
            existingVehicle.CheckOutDate = baove.CheckOutDate;
       

            await _vehicleRepository.UpdateAsync(existingVehicle); 
            return RedirectToAction("VehicleList" );
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !(await _vehicleRepository.VehicleExists(id.Value)))
            {
                return NotFound();
            }

            var rent = await _vehicleRepository.GetByIdAsync(id.Value);
            return View(rent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Lấy thông tin về phiếu thuê cần xóa
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            // Kiểm tra nếu xe không tồn tại
            if (vehicle == null)
            {
                return NotFound();
            }

           
            await _vehicleRepository.DeleteAsync(id);
            return RedirectToAction(nameof(VehicleList));
        }
    }
}
