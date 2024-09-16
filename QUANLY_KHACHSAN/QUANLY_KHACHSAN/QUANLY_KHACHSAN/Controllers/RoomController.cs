using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using QUANLY_KHACHSAN.ViewModels;
using System.Diagnostics;
using System.Security.Claims;


namespace QUANLY_KHACHSAN.Controllers
{
    public class RoomController : Controller
    {
        private readonly IPhongRepository roomRepo;
        public RoomController(IPhongRepository roomRepo_)
        {
            this.roomRepo = roomRepo_;
        }

        public async Task<IActionResult> RoomList(string searchString, string roomType, string SortOrder, string sortColumn, int pageNumber, string currentFilter, string currentFilter2, int manager)
        {
            ViewData["sortColumn"] = sortColumn;
            ViewData["sortOrder"] = SortOrder;
            ViewData["MaSortParam"] = sortColumn == "Map" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TenSortParam"] = sortColumn == "Tenphong" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TinhtrangSortParam"] = sortColumn == "Tinhtrang" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["DongiaSortParam"] = sortColumn == "Dongia" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (roomType != null)
            {
                pageNumber = 1;
            }
            else
            {
                roomType = currentFilter2;
            }

            ViewData["CurrentFilter2"] = roomType;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentPageNumber"] = pageNumber;
            var phongs = roomRepo.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                phongs = phongs.Where(r => r.Tenphong != null && r.Tenphong.ToLower().Contains(searchString.ToLower()));
            }
            if (!string.IsNullOrEmpty(roomType))
            {
                phongs = phongs.Where(r => r.MaloaiphongNavigation.Tenloai == roomType);
            }
            switch (sortColumn)
            {
                case "Map":
                    phongs = SortOrder == "desc" ? phongs.OrderByDescending(r => r.Map) : phongs.OrderBy(r => r.Map);
                    break;
                case "Tenphong":
                    phongs = SortOrder == "desc" ? phongs.OrderByDescending(r => r.Tenphong) : phongs.OrderBy(r => r.Tenphong);
                    break;
                case "Tinhtrang":
                    phongs = SortOrder == "desc" ? phongs.OrderByDescending(r => r.Tinhtrang) : phongs.OrderBy(r => r.Tinhtrang);
                    break;
                case "Dongia":
                    phongs = SortOrder == "desc" ? phongs.OrderByDescending(r => r.MaloaiphongNavigation.Dongia) : phongs.OrderBy(r => r.MaloaiphongNavigation.Dongia);
                    break;
                default:
                    phongs = phongs.OrderBy(r => r.Map);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 7;
            var roomTypes = await roomRepo.GetDistinctRoomTypeAsync();
            var roomTypeItems = roomTypes.Select(rt => new SelectListItem { Value = rt, Text = rt }).ToList();
            roomTypeItems.Insert(0, new SelectListItem { Value = "", Text = "Loại phòng" });
            ViewBag.RoomTypeList = roomTypeItems;
            ViewBag.Manager = manager;

            return View(await PaginatedList<Phong>.CreateAsync(phongs, pageNumber, pageSize));

        }

        public async Task<IActionResult> Create(int manager)
        {
            TempData["Manager"] = manager;
            var roomTypes = await roomRepo.GetAllLoaiphong();
            ViewBag.RoomType = new SelectList(roomTypes, "Maloaiphong", "Tenloai");
            ViewBag.RoomType2 = new SelectList(roomTypes, "Maloaiphong", "Dongia");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Phong phong, int manager)
        {
            phong.MaloaiphongNavigation = await roomRepo.GetRoomTypeById(phong.Maloaiphong);
            phong.Tinhtrang = 1;

            if (string.IsNullOrEmpty(phong.Tenphong))
            {
                var roomTypes = await roomRepo.GetAllLoaiphong(); // Fetch room types again
                ViewBag.RoomType = new SelectList(roomTypes, "Maloaiphong", "Tenloai");
                ViewBag.RoomType2 = new SelectList(roomTypes, "Maloaiphong", "Dongia");
                return View(phong);
            }

            await roomRepo.AddAsync(phong);
            return RedirectToAction("RoomList", new { manager = manager });
        }


        public async Task<IActionResult> Update(string roomid, int manager)
        {
            TempData["Manager"] = manager;
            var room = await roomRepo.GetByIdAsync(int.Parse(roomid));

            var roomTypes = await roomRepo.GetAllLoaiphong(); // Fetch room types again
            ViewBag.RoomType = new SelectList(roomTypes, "Maloaiphong", "Tenloai");
            ViewBag.RoomType2 = new SelectList(roomTypes, "Maloaiphong", "Dongia");
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Phong phong, int manager)
        {
            /*
            if (!ModelState.IsValid)
            { 
                var roomTypes = await roomRepo.GetAllLoaiphong(); // Fetch room types again
                ViewBag.RoomType = new SelectList(roomTypes, "Maloaiphong", "Tenloai");
                ViewBag.RoomType2 = new SelectList(roomTypes, "Maloaiphong", "Dongia");
                return View(phong);
            }
            */

            await roomRepo.UpdateAsync(phong);
            return RedirectToAction("RoomList", new { manager = manager });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string roomid, int manager)
        {
            await roomRepo.DeleteAsync(int.Parse(roomid));
            return RedirectToAction("RoomList", new { manager = manager });
        }
        public async Task<IActionResult> RoomUsageReport(int selectedMonth, string manager)
        {
            if (selectedMonth == 0)
            {
                selectedMonth = DateTime.Now.Month;
            }

            // Gọi phương thức GetRoomUsageReportAsync từ repository
            var phongs = await roomRepo.GetRoomUsageReportAsync(selectedMonth);

            // Set giá trị cho ViewBag.RoomUsageMonth
            ViewBag.RoomUsageMonth = selectedMonth;
            // Sorting logic for 'Tỷ lệ'

            // Trả về view với danh sách phòng đã tính toán
            return View("RoomUsageReport", phongs);
        }

    }
}
