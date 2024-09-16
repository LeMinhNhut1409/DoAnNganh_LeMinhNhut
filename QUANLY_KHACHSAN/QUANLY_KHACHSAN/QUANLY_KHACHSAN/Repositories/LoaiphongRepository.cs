using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using System;
using System.Diagnostics;


namespace QUANLY_KHACHSAN.Repositories
{
    public class LoaiphongRepository : ILoaiphongRepository
    {
        private readonly QUANLY_KHACHSANContext _dbContext;
        //Dependency Injection
        public LoaiphongRepository(QUANLY_KHACHSANContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Loaiphong> GetByIdAsync(int id)
        {
            var loaiphong = await _dbContext.Loaiphongs.FindAsync(id);
            return loaiphong;
        }

        public IQueryable<Loaiphong> GetAllAsync()
        {
            var loaiphongs = _dbContext.Loaiphongs
            .Select(loaiphong => new Loaiphong
            {
                Maloaiphong = loaiphong.Maloaiphong,
                Tenloai = loaiphong.Tenloai,
                Dongia = loaiphong.Dongia
            });

            return loaiphongs;

        }

        public async Task AddAsync(Loaiphong loaiphong)
        {

            await _dbContext.Loaiphongs.AddAsync(loaiphong);
            await _dbContext.SaveChangesAsync();
        }



        public async Task UpdateAsync(Loaiphong loaiphongmoi, int id)
        {
            var loaiphong = await _dbContext.Loaiphongs.FindAsync(id);

            loaiphong.Tenloai = loaiphongmoi.Tenloai;
            loaiphong.Dongia = loaiphongmoi.Dongia;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Loaiphong loaiphong = await _dbContext.Loaiphongs
                .Include(lp => lp.Phongs)
                .FirstOrDefaultAsync(lp => lp.Maloaiphong == Id);

            foreach (var phong in loaiphong.Phongs.ToList())
            {
                _dbContext.Phongs.Remove(phong);
            }

            _dbContext.Loaiphongs.Remove(loaiphong);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<string>> GetDistinctRoomTypesAsync()
        {
            return await _dbContext.Loaiphongs.Select(r => r.Tenloai).Distinct().ToListAsync();
        }

    }
}
