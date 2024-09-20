using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using QUANLY_KHACHSAN.ViewModels;


namespace QUANLY_KHACHSAN.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillRepository _billRepository;
        private readonly IKhachhangRepository _khachhangRepository;
        private readonly IPhieuthueRepository _rentRepository;
        private readonly INhanvienRepository _nhanvienRepository;
        private readonly IPhuthuRepository _phuthuRepository;
        private readonly IPhongRepository _phongRepository;

        public BillController(IBillRepository billRepository, IPhieuthueRepository rentRepository, IKhachhangRepository khachhangRepository, INhanvienRepository nhanvienRepository, IPhuthuRepository phuthuRepository, IPhongRepository phongRepository)
        {
            _billRepository = billRepository;
            _rentRepository = rentRepository;
            _khachhangRepository = khachhangRepository;
            _nhanvienRepository = nhanvienRepository;
            _phuthuRepository = phuthuRepository; ;
            _phongRepository = phongRepository;
        }
        // GET: Bill
        public async Task<IActionResult> Index()
        {
            var bills = await _billRepository.GetAllBills();
            return View(bills);
        }


        // GET: Bill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !(await _billRepository.BillExists(id.Value)))
            {
                return NotFound();
            }
            var hoadon = await _billRepository.GetBillById(id.Value);
            return View(hoadon);
        }
        public async Task<TimeSpan> TinhThoiGianThueAsync(int id)
        {
            var rent = await _rentRepository.GetRentAsync(id);
            DateTime ngayLapHoaDon = rent.Ngaylappt;
            DateTime ngayHienTai = DateTime.Now;
            if (ngayHienTai.Date == ngayLapHoaDon.Date)
            {
                return TimeSpan.FromDays(1);
            }
            return ngayHienTai - ngayLapHoaDon;
        }
        private async Task<int> TinhSoLuongKhachAsync(int map)
        {
            // Sử dụng repository hoặc các phương thức tương ứng để lấy số lượng khách
            // có Map giống với Mapt từ cơ sở dữ liệu
            var soLuongKhach = await _khachhangRepository.GetSoLuongKhachByMapAsync(map);
            return soLuongKhach;
        }
        // GET: Bill/Create
        [Route("Create/khachhangID/{khanghangID}/rentID/{rentID}")]
        public async Task<IActionResult> CreateAsync(int khanghangID, int rentID)
        {
            var client = await _khachhangRepository.GetByIdAsync(khanghangID);
            var rent = await _rentRepository.GetRentByIDAsync(khanghangID);
            var IDrent = await _rentRepository.GetRentAsync(rentID);
            int soLuongKhach = await TinhSoLuongKhachAsync(IDrent.Map);
            var room = await _phongRepository.GetByIdAsync(IDrent.Map);
            var phuthu = await _phuthuRepository.GetFirstPhuthuAsync();
            double tyle = ((double)phuthu.Giatriphuthu) / 100.0;
            var employeeList = await _nhanvienRepository.GetAllEmAsync();
            TimeSpan thoiGianThue = await TinhThoiGianThueAsync(rentID);
            int songayo = thoiGianThue.Days;
            int? donGiaBase1 = IDrent.MapNavigation.MaloaiphongNavigation.Dongia * songayo;
            double? donGiaBase = 0;
            double heSoPhuThu = 0.0;

            if (soLuongKhach > room.Soluongkhachtoida)
            {
                heSoPhuThu = tyle * (double)(soLuongKhach - room.Soluongkhachtoida);
                donGiaBase = (int)(donGiaBase1 * (heSoPhuThu));
                heSoPhuThu = (heSoPhuThu * 100);
            }
            else
            {
                heSoPhuThu = 0.0;
                donGiaBase = 0.0;
            }
            int rate = (int)heSoPhuThu;
            int? cost = donGiaBase1 + (int)donGiaBase;
            ViewData["Manv"] = new SelectList(employeeList, "Manv", "Email");
            ViewBag.songay = songayo;
            ViewBag.Makh = client.Tenkh;
            ViewBag.Mapt = IDrent.MapNavigation.Tenphong;
            ViewBag.Cost = cost;
            ViewBag.Ngaylaphd = DateTime.Now;
            TempData["Mapt"] = IDrent.Mapt;
            TempData["Map"] = IDrent.Map;
            ViewBag.Ngaydat = IDrent.Ngaylappt;
            ViewBag.Phuthu = heSoPhuThu;
            ViewBag.CCCD = client.Cccdkh;
            return View();
        }
        


        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create/khachhangID/{khanghangID}/rentID/{rentID}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahd,Songayo,Manv,Tongtien,Tenkh,Tenphong,Ngaylaphd,Ngaydat,Tylephuthu,IdphuThu,Cccd")] Hoadon hoadon)
        {
            // Lấy thông tin phụ thu
            var phuthu = await _phuthuRepository.GetFirstPhuthuAsync();
            if (phuthu != null)
            {
                hoadon.IdphuThu = phuthu.Idphuthu; // Giả sử Id là ID của phụ thu
                hoadon.Tylephuthu = (double)phuthu.Giatriphuthu; // Lưu tỷ lệ phụ thu
            }

            await _billRepository.CreateBill(hoadon);
            await _rentRepository.DeleteAsync((int)TempData["Mapt"]);
            bool check = await _rentRepository.CheckCCCDExistsInPhieuthueAsync(hoadon.Cccd);
            await _khachhangRepository.DeleteCustomersByRoomAsync(hoadon.Tenphong, hoadon.Cccd);
            var room = await _phongRepository.GetRoomByNameAsync(hoadon.Tenphong);
            if (room != null)
            {
                room.Tinhtrang = 1; // Update Tinhtrang to the desired value
                await _phongRepository.UpdateAsync(room);
            }
            if (check == false)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var rent = await _rentRepository.GetRentByCCCDAsync(hoadon.Cccd);
                var customr = await _khachhangRepository.GetClientByCCCDAsync(hoadon.Cccd);
                var viewModel = new Bill
                {
                    khachhang = customr,
                    phieuthues = rent
                };
                return View("Test", viewModel);
            }

        }

        // GET: Bill/Edit/5


        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !(await _billRepository.BillExists(id.Value)))
            {
                return NotFound();
            }

            var hoadon = await _billRepository.GetBillById(id.Value);
            return View(hoadon);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _billRepository.DeleteBill(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Test(int id)
        {

            var customers = await _khachhangRepository.GetClientByIDAsync(id);
            var phuthus = await _phuthuRepository.GetPhuthuByIdAsync(id);

            var rentList = await _rentRepository.GetRentsCustomerIdAsync(id);
            if (customers == null)
            {
                // Handle the case when the customer is not found
                return NotFound(); // or return an appropriate IActionResult
            }
            var viewModel = new Bill
            {
                khachhang = customers,
                phieuthues = rentList,
                phuthu = phuthus
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Test2()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Test2(test1 viewModel)
        {
            var client = await _khachhangRepository.GetClientByCCCDAsync(viewModel.CCCD);
            if (client == null || client.Makh == 0)
            {
                // Display an error message to the user
                ModelState.AddModelError("CCCD", "CCCD Không Hợp Lệ. Vui Lòng Nhập Lại");
                return View(viewModel);
            }
            int id = client.Makh;
            return RedirectToAction("Test", new { id = id });
        }

    }
}
