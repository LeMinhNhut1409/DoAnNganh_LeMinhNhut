using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface ILoaiKhachRepository
    {
        Task<Loaikhach> GetByIdAsync(int id);
        IQueryable<Loaikhach> GetAllAsync();
        Task AddAsync(Loaikhach loaikhach);
        Task UpdateAsync(Loaikhach loaikhach);
        Task DeleteAsync(int Id);
        Task<List<string>> GetDistinctClientTypeAsync();

    }
}