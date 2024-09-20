using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;


public class BillRepository : IBillRepository
{
    private readonly QUANLY_KHACHSANContext _context;

    public BillRepository(QUANLY_KHACHSANContext context)
    {
        _context = context;
    }

    public async Task<List<Hoadon>> GetAllBills()
    {
        return await _context.Hoadons

            .Include(h => h.ManvNavigation)
            .Include(h => h.IdphuThuNavigation)
            .ToListAsync();
    }

    public async Task<Hoadon> GetBillById(int id)
    {
        return await _context.Hoadons

            .Include(h => h.ManvNavigation)
            .Include(h => h.IdphuThuNavigation)
            .FirstOrDefaultAsync(m => m.Mahd == id);
    }

    public async Task CreateBill(Hoadon hoadon)
    {
        _context.Add(hoadon);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBill(Hoadon hoadon)
    {
        _context.Update(hoadon);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBill(int id)
    {
        var hoadon = await _context.Hoadons.FindAsync(id);
        if (hoadon != null)
        {
            _context.Hoadons.Remove(hoadon);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> BillExists(int id)
    {
        return await _context.Hoadons.AnyAsync(e => e.Mahd == id);
    }
}
