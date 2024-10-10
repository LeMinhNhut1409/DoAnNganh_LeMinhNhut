using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QUANLY_KHACHSAN.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly QUANLY_KHACHSANContext _context;
        private readonly INhanvienRepository _nvrepo;

        public VehicleRepository(QUANLY_KHACHSANContext context, INhanvienRepository nvrepo)
        {
            _context = context;
            _nvrepo = nvrepo;
        }
        public IQueryable<Baove> GetAllAsync()
        {
            var baoves = _context.Baoves
                .Select(baove => new Baove
                {
                    Mabv = baove.Mabv,
                    LicensePlate = baove.LicensePlate,
                    CheckInDate = baove.CheckInDate,
                    CheckOutDate = baove.CheckOutDate,
                    ManvNavigation = baove.ManvNavigation
                });

            return baoves;
        }
        public async Task<Baove> GetByIdAsync(int id)
        {
            return await _context.Baoves
                .Include(b => b.ManvNavigation)
                .FirstOrDefaultAsync(b => b.Mabv == id);
        }

        public async Task AddAsync(Baove baove)
        {
            await _context.Baoves.AddAsync(baove);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Baove baoveUpdate)
        {
            _context.Baoves.Update(baoveUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var baove = await _context.Baoves.FindAsync(id);
            if (baove != null)
            {
                _context.Baoves.Remove(baove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> VehicleExists(int id)
        {
            return await _context.Baoves.AnyAsync(b => b.Mabv == id);
        }

        public async Task<List<Nhanvien>> GetAllEmployeesAsync()
        {
            return await _context.Nhanviens.ToListAsync();
        }
    
}
}