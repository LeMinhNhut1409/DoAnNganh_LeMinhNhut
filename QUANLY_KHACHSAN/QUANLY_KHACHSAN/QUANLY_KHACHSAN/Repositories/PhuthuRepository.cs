using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;

public class PhuthuRepository : IPhuthuRepository
{
    private readonly QUANLY_KHACHSANContext _context;

    public PhuthuRepository(QUANLY_KHACHSANContext context)
    {
        _context = context;
    }

    public async Task<List<Phuthu>> GetAllPhuthusAsync()
    {
        return await _context.Phuthus.ToListAsync();
    }

    public async Task<Phuthu> GetPhuthuByIdAsync(double id)
    {
        return await _context.Phuthus.FirstOrDefaultAsync(m => m.Idphuthu == id);
    }

    public async Task AddPhuthuAsync(Phuthu phuthu)
    {
        _context.Add(phuthu);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePhuthuAsync(Phuthu phuthu)
    {
        _context.Update(phuthu);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePhuthuAsync(double id)
    {
        var phuthu = await _context.Phuthus.FindAsync(id);
        if (phuthu != null)
        {
            _context.Phuthus.Remove(phuthu);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> PhuthuExistsAsync(double id)
    {
        return (_context.Phuthus?.Any(e => e.Idphuthu == id)).GetValueOrDefault();
    }
    public async Task<Phuthu> GetFirstPhuthuAsync()
    {
        return await _context.Phuthus.FirstOrDefaultAsync();
    }
}
