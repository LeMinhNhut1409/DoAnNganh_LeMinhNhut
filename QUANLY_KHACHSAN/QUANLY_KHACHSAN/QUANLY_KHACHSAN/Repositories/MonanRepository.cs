using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.Repositories
{
    public class MonanRepository : IMonanRepository
    {
        private readonly QUANLY_KHACHSANContext _context;
        private readonly INhanvienRepository _nhanvienRepo;
        public MonanRepository(QUANLY_KHACHSANContext context, INhanvienRepository nhanvienRepo)
        {
            _context = context;
            _nhanvienRepo = nhanvienRepo;

        }

        // Lấy tất cả món ăn
        public async Task<List<Monan>> GetAllAsync()
        {
            return await _context.Monans.Include(m => m.ManvNavigation).ToListAsync();
        }

        // Lấy món ăn theo ID
        public async Task<Monan> GetByIdAsync(int id)
        {
            return await _context.Monans
                .Include(m => m.ManvNavigation) // Bao gồm thông tin điều hướng
                .FirstOrDefaultAsync(m => m.Mamonan == id);
        }

        // Thêm món ăn
        public async Task AddAsync(Monan monan)
        {
            await _context.Monans.AddAsync(monan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Monan monan)
        {
            _context.Monans.Update(monan);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var monan = await GetByIdAsync(id);
            if (monan != null)
            {
                _context.Monans.Remove(monan);
                await _context.SaveChangesAsync();
            }
        }

        // Kiểm tra món ăn có tồn tại
        public async Task<bool> MonanExists(int id)
        {
            return await _context.Monans.AnyAsync(m => m.Mamonan == id);
        }
    }
}