using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;

public class PhieuthueRepository : IPhieuthueRepository
{
    private readonly QUANLY_KHACHSANContext _context;

    public PhieuthueRepository(QUANLY_KHACHSANContext context)
    {
        _context = context;
    }

    public async Task<List<Phieuthue>> GetAllAsync()
    {
        return await _context.Phieuthues
            .Include(p => p.MakhNavigation)
            .Include(p => p.MapNavigation)
            .ToListAsync();
    }

    public async Task<Phieuthue> GetByIdAsync(int id)
    {
        return await _context.Phieuthues
            .Include(p => p.MakhNavigation)
            .Include(p => p.MapNavigation)
            .FirstOrDefaultAsync(m => m.Mapt == id);
    }

    public async Task AddAsync(Phieuthue phieuthue)
    {
        _context.Add(phieuthue);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Phieuthue phieuthue)
    {
        _context.Update(phieuthue);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var phieuthue = await _context.Phieuthues.FindAsync(id);
        if (phieuthue != null)
        {
            _context.Phieuthues.Remove(phieuthue);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> RentExists(int id)
    {
        return await _context.Phieuthues.AnyAsync(e => e.Mapt == id);
    }
    public async Task<List<int>> GetDistinctMakhAsync()
    {
        return await _context.Phieuthues
            .Select(r => r.Makh)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<int>> GetDistinctMapAsync()
    {
        return await _context.Phieuthues
            .Select(r => r.Map)
            .Distinct()
            .ToListAsync();
    }
    public async Task<List<Phieuthue>> GetRentsByCustomerIdAsync(int makh)
    {
        // Đây chỉ là một ví dụ, bạn cần điều chỉnh phương thức này dựa trên cách bạn tổ chức dữ liệu và tương tác với cơ sở dữ liệu của mình
        return await _context.Phieuthues
            .Where(p => p.Makh == makh)
            .ToListAsync();
    }
    public async Task<List<Phieuthue>> GetRentsCustomerIdAsync(int customerId)
    {
        // Logic to retrieve rentals from your data source
        // Replace the following line with your actual implementation
        var rentals = await _context.Phieuthues
                   .Where(pt => pt.Makh == customerId)
                   .Include(pt => pt.MapNavigation)
                   .Include(pt => pt.MapNavigation.MaloaiphongNavigation)
                   .ToListAsync();

        return rentals;
    }
    public async Task<List<int>> GetAllCustomerAsync()
    {
        return await _context.Phieuthues
            .Where(rent => rent.Makh != null)
            .Select(rent => rent.Makh)
            .Distinct()
            .ToListAsync();
    }
    public async Task<string> GetTenkhachhangByMakhAsync(int makh)
    {
        var tenkhachhang = await _context.Khachhangs
            .Where(kh => kh.Makh == makh)
            .Select(kh => kh.Tenkh)
            .FirstOrDefaultAsync();

        return tenkhachhang;
    }
    public async Task<Phieuthue> GetClientByCCCDAsync(string cccd)
    {
        return await _context.Phieuthues.FirstOrDefaultAsync(c => c.MakhNavigation.Cccdkh == cccd);
    }
    public async Task<Phieuthue> GetRentByIDAsync(int makh)
    {
        return await _context.Phieuthues.Include(pt => pt.MapNavigation.MaloaiphongNavigation).FirstOrDefaultAsync(c => c.Makh == makh);
    }
    public async Task<Phieuthue> GetRentAsync(int mapt)
    {
        return await _context.Phieuthues.Include(pt => pt.MapNavigation.MaloaiphongNavigation).FirstOrDefaultAsync(c => c.Mapt == mapt);
    }
    public async Task<List<Phieuthue>> GetRentByCCCDAsync(string cccd)
    {
        return await _context.Phieuthues.Where(c => c.Cccd == cccd)
            .Include(pt => pt.MapNavigation)
            .Include(pt => pt.MapNavigation.MaloaiphongNavigation)
            .ToListAsync();
    }
    public async Task<List<Phieuthue>> GetAllRentsByCCCDAsync(string cccd)
    {
        // Sử dụng Entity Framework để truy vấn cơ sở dữ liệu và lấy các phiếu thuê của khách hàng có CCCD tương ứng
        var rents = await _context.Phieuthues
            .Where(pt => pt.Cccd == cccd)
            .ToListAsync();

        return rents;
    }
    public async Task<bool> CheckCCCDExistsInPhieuthueAsync(string cccd)
    {
        // Lấy tất cả các phiếu thuê của khách hàng có CCCD tương ứng
        var allRents = await GetAllRentsByCCCDAsync(cccd);

        // Kiểm tra xem có phiếu thuê nào có CCCD đó không
        bool cccdExists = allRents.Any();

        return cccdExists;
    }

}
