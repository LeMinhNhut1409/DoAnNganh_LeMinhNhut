using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using System.Diagnostics;
using System.Security.Policy;
namespace QUANLY_KHACHSAN.Controllers
{
    public class CreateCustomerController : Controller
    {
        private readonly IKhachhangRepository clientRepo;
        private readonly IPhongRepository roomRepo;
        public CreateCustomerController(IKhachhangRepository clientRepo_, IPhongRepository roomRepo_)
        {
            this.clientRepo = clientRepo_;
            this.roomRepo = roomRepo_;
        }
        public async Task<IActionResult> Create(string id, string value1, int manager)
        {
            int ID = int.Parse(id);
            TempData["Manager"] = manager;
            var clientTypes = await clientRepo.GetAllLoaikhach();
            var rooms = await roomRepo.GetByIdAsync(ID);
            ViewBag.ClientType = new SelectList(clientTypes, "Maloaikhach", "Tenloaikhach");
            ViewData["TenPhong"] = rooms.Tenphong;
            TempData["TempMapt"] = int.Parse(value1); // Thay đổi tên biến
            TempData["TempMap"] = int.Parse(id);

            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Khachhang khach, int id, int value1)
        {

            khach.Map = id;
            await clientRepo.AddAsync(khach);
            return RedirectToAction("Details", "Rent", new { id = value1 });
        }

    }
}
