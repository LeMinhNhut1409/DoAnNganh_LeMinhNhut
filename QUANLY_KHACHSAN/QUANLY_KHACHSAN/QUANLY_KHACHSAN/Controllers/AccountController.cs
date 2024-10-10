using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.Controllers
{
    public class AccountController : Controller
    {
        private readonly INhanvienRepository _nvrepo;
        private readonly ITaikhoanRepository _taikhoanRepo;

        public AccountController(INhanvienRepository nvrepo, ITaikhoanRepository taikhoanRepo)
        {
            _nvrepo = nvrepo;
            _taikhoanRepo = taikhoanRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var nhanVienId = await _nvrepo.GetEmployeeNoAccount();
            ViewBag.EmployeeIdList = new SelectList(nhanVienId, "Manv", "Manv");
            ViewBag.EmployeeEmailList = new SelectList(nhanVienId, "Manv", "Email");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Taikhoan taikhoan)
        {
            if (taikhoan.Manv == 0)
            {
                var nhanVienList = await _nvrepo.GetEmployeeNoAccount();
                if (nhanVienList.Count() == 0)
                {
                    ModelState.AddModelError("Manv", "Tất cả nhân viên đều đã có tài khoản");
                    return View();
                }

                else
                {
                    await _taikhoanRepo.CreateAccountForAllEmployee(nhanVienList);
                }
            }
            else
            {
                await _taikhoanRepo.AddAsync(taikhoan);
            }
            ViewBag.success = "Cấp tài khoản thành công";
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Taikhoan tk)
        {
            var check = await _taikhoanRepo.GetByUsernameAndPasswordAsync(tk.Tentknv, tk.Mktk);

            if (check != null)
            {
                // Set user role as a claim
                var claims = new List<Claim>
                {
                    new Claim("Role", check.Tentknv.Contains("@ou.edu.vn") ? "Staff" : "Manager")

                };

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Check the role and redirect accordingly
                if (check.Tentknv.Contains("@gmail.com"))
                {
                    // Quản Lý role
                    HttpContext.Session.SetString("accname", check.Tentknv);
                    var name = HttpContext.Session.GetString("accname");
                    ViewBag.accname = name;
                    return RedirectToAction("Index", "Manager");
                }
                else if (check.Tentknv.Contains(".letan@ou.edu.vn"))
                {
                    // Nhân viên role
                    HttpContext.Session.SetString("accname", check.Tentknv);
                    var name = HttpContext.Session.GetString("accname");
                    ViewBag.accname = name;
                    return RedirectToAction("Letan", "Staff");
                }
                else if (check.Tentknv.Contains(".baove@ou.edu.vn"))
                {
                    // Nhân viên role

                    HttpContext.Session.SetString("accname", check.Tentknv);
                    var name = HttpContext.Session.GetString("accname");
                    ViewBag.accname = name;
                    return RedirectToAction("Baove", "Staff");
                }
                else if (check.Tentknv.Contains(".nhabep@ou.edu.vn"))
                {
                    // Nhân viên role
                    HttpContext.Session.SetString("accname", check.Tentknv);
                    var name = HttpContext.Session.GetString("accname");
                    ViewBag.accname = name;
                    return RedirectToAction("Nhabep", "Staff");
                }
                else if (check.Tentknv.Contains(".tapvu@ou.edu.vn"))
                {
                    // Nhân viên role
                    HttpContext.Session.SetString("accname", check.Tentknv);
                    var name = HttpContext.Session.GetString("accname");
                    ViewBag.accname = name;
                    return RedirectToAction("Tapvu", "Staff");
                }
            }

            ViewBag.error = "Đăng nhập thất bại";
            return View();
        }

        public IActionResult Logout()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("accname")))
            {
                HttpContext.Session.SetString("accname", "");
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Login", "Account");
        }


    }
}
