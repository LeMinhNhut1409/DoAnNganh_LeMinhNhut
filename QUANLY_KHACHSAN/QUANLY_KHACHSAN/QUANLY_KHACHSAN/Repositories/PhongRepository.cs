using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using System;


namespace QUANLY_KHACHSAN.Repositories
{
    public class PhongRepository : IPhongRepository
    {
        private readonly QUANLY_KHACHSANContext _dbContext;
        private readonly ILoaiphongRepository _loaiphongRepo;
        public PhongRepository(QUANLY_KHACHSANContext dbContext, ILoaiphongRepository lprepo)
        {
            _dbContext = dbContext;
            _loaiphongRepo = lprepo;
        }
        public async Task AddAsync(Phong phong)
        {
            await _dbContext.Phongs.AddAsync(phong);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Phong phong = await _dbContext.Phongs.FindAsync(Id);
            _dbContext.Phongs.Remove(phong);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Phong> GetAllAsync()
        {
            var phongs = _dbContext.Phongs
            .Select(phong => new Phong
            {
                Map = phong.Map,
                Tenphong = phong.Tenphong,
                MaloaiphongNavigation = phong.MaloaiphongNavigation,
                Tinhtrang = phong.Tinhtrang,
                Ghichu = phong.Ghichu,
                Phieuthues = phong.Phieuthues,
                Soluongkhachtoida = phong.Soluongkhachtoida
            });

            return phongs;
        }

        public async Task<Phong> GetByIdAsync(int id)
        {
            var phong = await _dbContext.Phongs.FindAsync(id);
            return phong;
        }

        public async Task UpdateAsync(Phong phongUpdate)
        {
            phongUpdate.MaloaiphongNavigation = await _loaiphongRepo.GetByIdAsync(phongUpdate.Maloaiphong);
            _dbContext.Phongs.Update(phongUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Loaiphong>> GetAllLoaiphong()
        {
            return await _dbContext.Loaiphongs.ToListAsync();
        }
        public async Task<Loaiphong> GetRoomTypeById(int id)
        {
            return await _dbContext.Loaiphongs.FindAsync(id); // Assuming _context is your DbContext
        }
        public async Task<List<string>> GetDistinctRoomTypeAsync()
        {
            return await _loaiphongRepo.GetDistinctRoomTypesAsync();
        }

        public async Task<List<Phong>> GetRoomUsageReportAsync(int? thang)
        {
            try
            {
                var phongs = await _dbContext.Phongs.ToListAsync();

                foreach (var phong in phongs)
                {
                    var roomUsageReport = await _dbContext.Hoadons
                        .Where(hd => hd.Ngaylaphd.Month == thang && hd.Ngaylaphd.Year == DateTime.Now.Year && hd.Tenphong == phong.Tenphong)
                        .GroupBy(hd => hd.Tenphong)
                        .Select(group => new
                        {
                            TenPhong = group.Key,
                            TotalDaysInMonth = group.Sum(hd => (hd.Ngaylaphd - hd.Ngaydat).Days + 1)
                        })
                        .FirstOrDefaultAsync();

                    if (roomUsageReport != null)
                    {
                        phong.Songayo = roomUsageReport.TotalDaysInMonth;
                    }
                    else
                    {
                        phong.Songayo = 0;
                    }
                }

                return phongs;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (ghi log hoặc ném lại nếu cần)
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                throw;
            }
        }
        public async Task<List<Phong>> GetRoomsByTinhtrangAsync(int tinhtrang, int roomId = 0)
        {
            var roomList = await _dbContext.Phongs
                .Where(r => r.Tinhtrang == tinhtrang || r.Map == roomId)
                .ToListAsync();

            return roomList;
        }

        public async Task<Phong> GetRoomByNameAsync(string tenphong)
        {
            return await _dbContext.Phongs
                .FirstOrDefaultAsync(l => l.Tenphong == tenphong);
        }
        public async Task<List<Phong>> GetRoomUsageReportAsync(int thang)
        {
            try
            {
                var phongs = await _dbContext.Phongs.ToListAsync();

                foreach (var phong in phongs)
                {
                    var roomUsage = _dbContext.Hoadons
                        .Where(hd => hd.Ngaylaphd.Month == thang &&
                                     hd.Ngaylaphd.Year == DateTime.Now.Year &&
                                     hd.Tenphong == phong.Tenphong)
                        .AsEnumerable()  // Execute the query on the client side
                        .Sum(hd => (hd.Ngaylaphd - hd.Ngaydat).Days + 1);

                    phong.Songayo = roomUsage;
                }

                return phongs;
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                throw;
            }
        }

        public async Task<List<int>> GetSoluongkhachtoidaList()
        {
            // Replace this with your actual logic to retrieve Soluongkhachtoida values for each room
            List<int> soluongkhachtoidaList = await _dbContext.Phongs.Select(room => room.Soluongkhachtoida).ToListAsync();

            return soluongkhachtoidaList;
        }

    }
}
