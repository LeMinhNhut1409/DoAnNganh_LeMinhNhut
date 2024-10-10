using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.ViewModels;
using QUANLY_KHACHSAN.InterfacesRepositories;

public class TapvuController : Controller
{
    private readonly ITapvuRepository _tapvuRepo;
    private readonly IPhongRepository _phongRepo;
    private readonly INhanvienRepository _nhanvienRepo;

    public TapvuController(ITapvuRepository tapvuRepo, IPhongRepository phongRepo, INhanvienRepository nhanvienRepo)
    {
        _tapvuRepo = tapvuRepo;
        _phongRepo = phongRepo;
        _nhanvienRepo = nhanvienRepo;
    }

    public async Task<IActionResult> Index(string searchString, string SortOrder, string sortColumn, int pageNumber = 1, string currentFilter = null)
    {
        ViewData["sortColumn"] = sortColumn;
        ViewData["sortOrder"] = SortOrder;
        ViewData["MaSortParam"] = sortColumn == "Matapvu" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
        ViewData["DaDonDepSortParam"] = sortColumn == "DaDonDep" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
        ViewData["DaThemDoDungSortParam"] = sortColumn == "DaThemDoDung" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
        ViewData["SoLuongKhanSortParam"] = sortColumn == "SoLuongKhan" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
        ViewData["SoLuongGaGiuongSortParam"] = sortColumn == "SoLuongGaGiuong" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
        ViewData["SoLuongDungCuVeSinhSortParam"] = sortColumn == "SoLuongDungCuVeSinh" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";

        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        ViewData["CurrentFilter"] = searchString;
        ViewData["CurrentPageNumber"] = pageNumber;

        var tapvus = _tapvuRepo.GetAllAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            tapvus = tapvus.Where(t => t.MapNavigation.Tenphong != null && t.MapNavigation.Tenphong.ToLower().Contains(searchString.ToLower()));
        }

        switch (sortColumn)
        {
            case "Matapvu":
                tapvus = SortOrder == "desc" ? tapvus.OrderByDescending(t => t.Matapvu) : tapvus.OrderBy(t => t.Matapvu);
                break;
            case "DaDonDep":
                tapvus = SortOrder == "desc" ? tapvus.OrderByDescending(t => t.Dadondep) : tapvus.OrderBy(t => t.Dadondep);
                break;
            case "DaThemDoDung":
                tapvus = SortOrder == "desc" ? tapvus.OrderByDescending(t => t.Dathemdodung) : tapvus.OrderBy(t => t.Dathemdodung);
                break;
            case "SoLuongKhan":
                tapvus = SortOrder == "desc" ? tapvus.OrderByDescending(t => t.Soluongkhan) : tapvus.OrderBy(t => t.Soluongkhan);
                break;
            case "SoLuongGaGiuong":
                tapvus = SortOrder == "desc" ? tapvus.OrderByDescending(t => t.Soluonggagiuong) : tapvus.OrderBy(t => t.Soluonggagiuong);
                break;
            case "SoLuongDungCuVeSinh":
                tapvus = SortOrder == "desc" ? tapvus.OrderByDescending(t => t.Soluongdungcuvesinh) : tapvus.OrderBy(t => t.Soluongdungcuvesinh);
                break;
            default:
                tapvus = tapvus.OrderBy(t => t.Matapvu);
                break;
        }

        int pageSize = 7;
        return View(await PaginatedList<Tapvu>.CreateAsync(tapvus, pageNumber, pageSize));
    }


    // Phương thức GET 
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var tapvu = await _tapvuRepo.GetByIdAsync(id);
        if (tapvu == null)
        {
            return NotFound();
        }

        var nhanvienList = await _nhanvienRepo.GetAllAsync(); // Lấy danh sách nhân viên
        ViewBag.NhanVienList = nhanvienList; // Lưu danh sách vào ViewBag
        var phongList = await _phongRepo.GetAllAsync().ToListAsync(); // Lấy danh sách phòng
        ViewBag.PhongList = phongList; // Lưu danh sách vào ViewBag
        return View(tapvu);
    }

    // Phương thức POST 
    [HttpPost]
    public async Task<IActionResult> Update(int id, Tapvu tapvu)
    {
        if (tapvu.Manv == 0 || tapvu.Map == 0) // Kiểm tra mã nhân viên
        {
            ModelState.AddModelError("Manv", "Mã nhân viên không hợp lệ.");
            ModelState.AddModelError("Map", "Mã phòng không hợp lệ.");
            return View(tapvu);
        }

        var existingTapvu = await _tapvuRepo.GetByIdAsync(id);
        if (existingTapvu == null)
        {
            return NotFound();
        }

        // Ghi đè các thuộc tính cần thiết
        existingTapvu.Dadondep = tapvu.Dadondep;
        existingTapvu.Dathemdodung = tapvu.Dathemdodung;
        existingTapvu.Soluongkhan = tapvu.Soluongkhan;
        existingTapvu.Soluonggagiuong = tapvu.Soluonggagiuong;
        existingTapvu.Soluongdungcuvesinh = tapvu.Soluongdungcuvesinh;
        await _tapvuRepo.UpdateAsync(existingTapvu);
        return RedirectToAction("Index");
    }


}

