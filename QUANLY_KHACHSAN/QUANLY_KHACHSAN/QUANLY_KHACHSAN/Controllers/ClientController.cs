using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using QUANLY_KHACHSAN.ViewModels;
using System.Diagnostics;


namespace QUANLY_KHACHSAN.Controllers
{
    public class ClientController : Controller
    {
        private readonly IKhachhangRepository clientRepo;
        private readonly IPhongRepository roomRepo;
        private readonly IPhieuthueRepository rentRepo;
        public ClientController(IKhachhangRepository clientRepo_, IPhongRepository roomRepo_, IPhieuthueRepository rentrepo)
        {
            this.clientRepo = clientRepo_;
            this.roomRepo = roomRepo_;
            this.rentRepo = rentrepo;
        }

        public async Task<IActionResult> ClientList(int manager, string searchString, string clientType, string SortOrder, string sortColumn, int pageNumber, string currentFilter, string currentFilter2, int rentid)
        {
            if (rentid != 0)
            {
                TempData["Manager"] = manager;
                return RedirectToAction("Details", "Rent", new { id = rentid });
            }
            TempData["Manager"] = manager;
            ViewData["sortColumn"] = sortColumn;
            ViewData["sortOrder"] = SortOrder;
            ViewData["MaSortParam"] = sortColumn == "Makh" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TenSortParam"] = sortColumn == "Tenkh" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TinhSortParam"] = sortColumn == "Tuoi" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (clientType != null)
            {
                pageNumber = 1;
            }
            else
            {
                clientType = currentFilter2;
            }

            ViewData["CurrentFilter2"] = clientType;
            ViewData["CurrentFilter"] = searchString;
            var khachs = clientRepo.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                khachs = khachs.Where(cl => cl.Tenkh != null && cl.Tenkh.ToLower().Contains(searchString.ToLower()));
            }
            if (!string.IsNullOrEmpty(clientType))
            {
                khachs = khachs.Where(r => r.MaloaikhachNavigation.Tenloaikhach == clientType);
            }
            switch (sortColumn)
            {
                case "Makh":
                    khachs = SortOrder == "desc" ? khachs.OrderByDescending(cl => cl.Makh) : khachs.OrderBy(cl => cl.Makh);
                    break;
                case "Tenkh":
                    khachs = SortOrder == "desc" ? khachs.OrderByDescending(cl => cl.Tenkh) : khachs.OrderBy(cl => cl.Tenkh);
                    break;
                case "Tuoi":
                    khachs = SortOrder == "desc" ? khachs.OrderByDescending(cl => cl.Tuoi) : khachs.OrderBy(cl => cl.Tuoi);
                    break;


                default:
                    khachs = khachs.OrderBy(cl => cl.Makh);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 7;
            var clientTypes = await clientRepo.GetDistinctClientTypeAsync();
            var clientTypeItems = clientTypes.Select(ct => new SelectListItem { Value = ct, Text = ct }).ToList();
            clientTypeItems.Insert(0, new SelectListItem { Value = "", Text = "Loại Khách" });
            ViewBag.ClientTypeList = clientTypeItems;

            var errorMessage = TempData["ErrorMessage"] as string;
            ViewBag.ErrorMessage = errorMessage;
            return View(await PaginatedList<Khachhang>.CreateAsync(khachs, pageNumber, pageSize));

        }

        public async Task<IActionResult> Create(int manager)
        {
            TempData["Manager"] = manager;
            var clientTypes = await clientRepo.GetAllLoaikhach();
            ViewBag.ClientType = new SelectList(clientTypes, "Maloaikhach", "Tenloaikhach");
            var roomList = await roomRepo.GetRoomsByTinhtrangAsync(1);
            ViewData["Map"] = new SelectList(roomList, "Map", "Tenphong");
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Khachhang khach, int manager)
        {
            TempData["Manager"] = manager;
            khach.MaloaikhachNavigation = await clientRepo.GetClientTypeById(khach.Maloaikhach);


            if (string.IsNullOrEmpty(khach.Tenkh))
            {

                var clientTypes = await clientRepo.GetAllLoaikhach(); // Fetch client types again
                var rooms = roomRepo.GetAllAsync();
                ViewBag.ClientType = new SelectList(clientTypes, "Maloaikhach", "Tenloaikhach");
                var room = await roomRepo.GetByIdAsync(khach.Map);
                if (room != null)
                {
                    room.Tinhtrang = 2; // Update Tinhtrang to the desired value
                    await roomRepo.UpdateAsync(room);
                }
                
                return View(khach);
            }

            await clientRepo.AddAsync(khach);
            return RedirectToAction(nameof(ClientList), new { manager = manager });
        }


        public async Task<IActionResult> Update(int clientid, int manager, int value1)
        {
            Debug.WriteLine("id :" + value1);
            TempData["TempMapt"] = value1;
            TempData["Manager"] = manager;
            var client = await clientRepo.GetByIdAsync(clientid);
            var clientTypes = await clientRepo.GetAllLoaikhach(); // Fetch client types again
            ViewBag.ClientType = new SelectList(clientTypes, "Maloaikhach", "Tenloaikhach");

            var rooms = roomRepo.GetAllAsync();
            ViewBag.ClientRoom = new SelectList(rooms, "Map", "Tenphong");
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Khachhang khach, int manager, int clientid, int value1)
        {
            khach.Makh = clientid;
            /*
            if (!ModelState.IsValid)
            {
                var clientTypes = await clientRepo.GetAllLoaikhach(); // Fetch client types again
                ViewBag.ClientType = new SelectList(clientTypes, "Maloaikhach", "Tenloaikhach");
                var rooms = roomRepo.GetAllAsync();
                ViewBag.ClientRoom = new SelectList(rooms, "Map", "Tenphong");
                return View(khach);
            }*/

            await clientRepo.UpdateAsync(khach, clientid);
            return RedirectToAction("ClientList", new { rentid = value1 });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int clientid, int manager, int value1)
        {
            var rent = await rentRepo.GetAllAsync();
            if (rent.Any(r => r.Makh == clientid))
            {
                TempData["ErrorMessage"] = "Không thể xóa khách hàng đã có trong danh sách thuê.";
            }
            else
            {
                await clientRepo.DeleteAsync(clientid);
            }

            return RedirectToAction("ClientList", new { manager = manager, rentid = value1 });
        }



        public async Task<IActionResult> Details(int clientid, int manager)
        {

            TempData["Manager"] = manager;
            var client = await clientRepo.GetByIdAsync(clientid);

            if (client == null)
            {
                return NotFound(); // Handle not found client
            }

            return View(client);
        }
    }
}

