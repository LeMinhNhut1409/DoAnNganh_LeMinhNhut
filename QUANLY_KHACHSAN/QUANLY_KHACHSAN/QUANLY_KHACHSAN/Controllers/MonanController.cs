using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QUANLY_KHACHSAN.Controllers
{
    public class MonanController : Controller
    {
        private readonly IMonanRepository _monanRepo;
        private readonly INhanvienRepository _employeeRepo;
        public MonanController(IMonanRepository monanRepo, INhanvienRepository employeeRepo)
        {
            _monanRepo = monanRepo;
            _employeeRepo = employeeRepo;
        }

        // Hiển thị danh sách món ăn
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var monans = await _monanRepo.GetAllAsync();
            return View(monans);
        }
        // Phương thức GET để tạo món ăn
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var nhanvienList = await _employeeRepo.GetAllAsync(); // Lấy danh sách nhân viên
            ViewBag.NhanVienList = nhanvienList; // Lưu danh sách vào ViewBag

            return View();
        }

        // Phương thức POST để lưu món ăn
        [HttpPost]
        public async Task<IActionResult> Create(Monan monan)
        {
            if (monan.Manv == 0) // Kiểm tra mã nhân viên
            {
                ModelState.AddModelError("Manv", "Mã nhân viên không hợp lệ.");
                return View(monan);
            }

            if (string.IsNullOrEmpty(monan.Tenmon))
            {
                ModelState.AddModelError("Tenmon", "Tên món không được để trống.");
                return View(monan);
            }

            await _monanRepo.AddAsync(monan);
            return RedirectToAction("Index");
        }

        // Phương thức GET để chỉnh sửa món ăn
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var monan = await _monanRepo.GetByIdAsync(id); // Lấy món ăn theo ID
            if (monan == null)
            {
                return NotFound(); // Nếu không tìm thấy món ăn, trả về NotFound
            }

            var nhanvienList = await _employeeRepo.GetAllAsync(); // Lấy danh sách nhân viên
            ViewBag.NhanVienList = nhanvienList; // Lưu danh sách vào ViewBag

            return View(monan);
        }

        // Phương thức POST để lưu thay đổi món ăn
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Monan monan)
        {
            if (monan.Manv == 0) // Kiểm tra mã nhân viên
            {
                ModelState.AddModelError("Manv", "Mã nhân viên không hợp lệ.");
                return View(monan);
            }

            if (string.IsNullOrEmpty(monan.Tenmon))
            {
                ModelState.AddModelError("Tenmon", "Tên món không được để trống.");
                return View(monan);
            }

            // Lấy món ăn cũ từ cơ sở dữ liệu trước khi ghi đè
            var existingMonan = await _monanRepo.GetByIdAsync(id);
            if (existingMonan == null)
            {
                return NotFound(); // Nếu món ăn không tồn tại, trả về NotFound
            }

            // Ghi đè các thuộc tính cần thiết
            existingMonan.Tenmon = monan.Tenmon;
            existingMonan.Mota = monan.Mota;
            existingMonan.Gia = monan.Gia;
            existingMonan.Thoigianchebien = monan.Thoigianchebien;

            await _monanRepo.UpdateAsync(existingMonan); // Cập nhật món ăn
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !(await _monanRepo.MonanExists(id.Value)))
            {
                return NotFound();
            }

            var rent = await _monanRepo.GetByIdAsync(id.Value);
            return View(rent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Lấy thông tin về phiếu thuê cần xóa
            var vehicle = await _monanRepo.GetByIdAsync(id);

            // Kiểm tra nếu xe không tồn tại
            if (vehicle == null)
            {
                return NotFound();
            }


            await _monanRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}