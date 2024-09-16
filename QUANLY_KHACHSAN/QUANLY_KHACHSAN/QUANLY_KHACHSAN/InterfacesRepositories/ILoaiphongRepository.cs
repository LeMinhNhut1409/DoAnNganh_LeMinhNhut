using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;


namespace QUANLY_KHACHSAN.Repositories
{
    public interface ILoaiphongRepository
    {
        Task<Loaiphong> GetByIdAsync(int id);
        IQueryable<Loaiphong> GetAllAsync();
        Task AddAsync(Loaiphong loaiphong);
        Task UpdateAsync(Loaiphong loaiphong, int id);
        Task DeleteAsync(int Id);
        Task<List<string>> GetDistinctRoomTypesAsync();

    }
}
