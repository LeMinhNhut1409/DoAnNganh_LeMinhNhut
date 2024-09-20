using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Repositories;

using QUANLY_KHACHSAN.Models;


namespace QUANLY_KHACHSAN.Controllers
{
    public class PhuthuController : Controller
    {
        private readonly QUANLY_KHACHSANContext _context;
        private readonly IPhuthuRepository _rpphuthu;
        public PhuthuController(QUANLY_KHACHSANContext context, IPhuthuRepository rppt)
        {
            _rpphuthu = rppt;
            _context = context;
        }

        // GET: Phuthu

        // GET: Phuthu/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var phuthu = await _rpphuthu.GetPhuthuByIdAsync(id);
            if (phuthu == null)
            {
                return NotFound();
            }
            return View(phuthu);
        }

        // POST: Phuthu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idphuthu,Giatriphuthu")] Phuthu phuthu)
        {
            if (id == 0)
            {
                return NotFound();
            }
            phuthu.Idphuthu = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phuthu);
                    ViewBag.success = "Lưu thành công";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhuthuExists(phuthu.Idphuthu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(phuthu);
        }



        private bool PhuthuExists(int id)
        {
            return (_context.Phuthus?.Any(e => e.Idphuthu == id)).GetValueOrDefault();
        }
    }
}
