using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.ViewModels;


namespace QUANLY_KHACHSAN.Controllers
{
    public class SettingLoaiKhachController : Controller
    {
        private readonly ILoaiKhachRepository _lkrepo;
        public SettingLoaiKhachController(ILoaiKhachRepository repo)
        {
            _lkrepo = repo;
        }
        public async Task<IActionResult> ClientType(string searchString, string SortOrder, string sortColumn, int pageNumber, string currentFilter)
        {
            ViewData["sortColumn"] = sortColumn;
            ViewData["sortOrder"] = SortOrder;
            ViewData["MaSortParam"] = sortColumn == "Maloaikhach" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TenSortParam"] = sortColumn == "Tenloaikhach" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var loaikhachs = _lkrepo.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                loaikhachs = loaikhachs.Where(cl => cl.Tenloaikhach != null && cl.Tenloaikhach.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortColumn)
            {
                case "Maloaikhach":
                    loaikhachs = SortOrder == "desc" ? loaikhachs.OrderByDescending(cl => cl.Maloaikhach) : loaikhachs.OrderBy(cl => cl.Maloaikhach);
                    break;
                case "Tenloaikhach":
                    loaikhachs = SortOrder == "desc" ? loaikhachs.OrderByDescending(cl => cl.Tenloaikhach) : loaikhachs.OrderBy(cl => cl.Tenloaikhach);
                    break;
                default:
                    loaikhachs = loaikhachs.OrderBy(cl => cl.Maloaikhach);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 7;

            return View(await PaginatedList<Loaikhach>.CreateAsync(loaikhachs, pageNumber, pageSize));

        }
        public async Task<IActionResult> CreateClientType()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateClientType(Loaikhach loaikhach)
        {

            if (!ModelState.IsValid)
            {
                return View(loaikhach);
            }

            await _lkrepo.AddAsync(loaikhach);
            return RedirectToAction("ClientType");
        }


        public async Task<IActionResult> UpdateClientType(string clienttypeid)
        {
            var clientType = await _lkrepo.GetByIdAsync(int.Parse(clienttypeid));
            return View(clientType);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClientType(Loaikhach loaikhach)
        {

            if (!ModelState.IsValid)
            {
                return View(loaikhach);
            }

            await _lkrepo.UpdateAsync(loaikhach);
            return RedirectToAction("ClientType");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClientType(int clienttypeid)
        {
            await _lkrepo.DeleteAsync(clienttypeid);
            return RedirectToAction("ClientType");
        }
    }
}
