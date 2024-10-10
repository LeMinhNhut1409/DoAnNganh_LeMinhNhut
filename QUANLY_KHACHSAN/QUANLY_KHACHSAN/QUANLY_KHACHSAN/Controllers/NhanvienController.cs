using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.ViewModels;
using System.Linq; // Add this using statement
using System.Threading.Tasks;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;

namespace QUANLY_KHACHSAN.Controllers
{
    public class NhanvienController : Controller
    {
        private readonly INhanvienRepository nhanvienRepo;

        public NhanvienController(INhanvienRepository nhanvienRepo_)
        {
            this.nhanvienRepo = nhanvienRepo_;
        }

        public async Task<IActionResult> NhanvienList(string searchString, string SortOrder, string sortColumn, int pageNumber, string currentFilter)
        {
            ViewData["sortColumn"] = sortColumn;
            ViewData["sortOrder"] = SortOrder;
            ViewData["ManvSortParam"] = sortColumn == "Manv" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["HotenSortParam"] = sortColumn == "Hoten" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["PhaiSortParam"] = sortColumn == "Gioitinh" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["NgaysinhSortParam"] = sortColumn == "Ngaysinh" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var nhanviensList = await nhanvienRepo.GetAllAsync();
            var nhanviens = nhanviensList.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                nhanviens = nhanviens.Where(n => n.Hoten != null && n.Hoten.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortColumn)
            {
                case "Manv":
                    nhanviens = SortOrder == "desc" ? nhanviens.OrderByDescending(n => n.Manv) : nhanviens.OrderBy(n => n.Manv);
                    break;
                case "Hoten":
                    nhanviens = SortOrder == "desc" ? nhanviens.OrderByDescending(n => n.Hoten) : nhanviens.OrderBy(n => n.Hoten);
                    break;
                case "Gioitinh":
                    nhanviens = SortOrder == "desc" ? nhanviens.OrderByDescending(n => n.Gioitinh) : nhanviens.OrderBy(n => n.Gioitinh);
                    break;
                case "Ngaysinh":
                    nhanviens = SortOrder == "desc" ? nhanviens.OrderByDescending(n => n.Ngaysinh) : nhanviens.OrderBy(n => n.Ngaysinh);
                    break;
                default:
                    nhanviens = nhanviens.OrderBy(n => n.Manv);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            int pageSize = 7;
            return View(await PaginatedList<Nhanvien>.CreateAsync(nhanviens, pageNumber, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Nhanvien nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return View(nhanvien);
            }
            var existingNhanvien = await nhanvienRepo.GetByEmailAsync(nhanvien.Email);

            if (existingNhanvien != null)
            {
                ModelState.AddModelError("Email", "Email này đã được sử dụng");
                return View(nhanvien);
            }
            await nhanvienRepo.AddAsync(nhanvien);
            return RedirectToAction("NhanvienList");
        }


        public async Task<IActionResult> Update(string nhanvienid)
        {
            var nhanvien = await nhanvienRepo.GetByIdAsync(int.Parse(nhanvienid));

            return View(nhanvien);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Nhanvien nhanvien, string nhanvienid)
        {

            if (!ModelState.IsValid)
            {

                return View(nhanvien);
            }
            int id = int.Parse(nhanvienid);

            await nhanvienRepo.UpdateAsync(nhanvien, id);
            return RedirectToAction("nhanvienList");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string nhanvienid)
        {
            await nhanvienRepo.DeleteAsync(int.Parse(nhanvienid));
            return RedirectToAction("NhanvienList");
        }
    }
}

