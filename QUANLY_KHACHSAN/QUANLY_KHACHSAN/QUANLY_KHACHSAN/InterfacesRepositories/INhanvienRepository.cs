using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.Repositories
{
    public interface INhanvienRepository
    {
        Task<Nhanvien> GetByIdAsync(int id);
        Task<IQueryable<Nhanvien>> GetAllAsync();
        Task AddAsync(Nhanvien nhanvien);
        Task UpdateAsync(Nhanvien nhanvien, int nhanvienid);
        Task DeleteAsync(int Id);
        Task<Nhanvien> GetEmployeeByIdAsync(int id);
        Task<IQueryable<Nhanvien>> GetAllEmployeesAsync();
        Task<IQueryable<Nhanvien>> GetEmployeeNoAccount();
        Task<Nhanvien> GetByEmailAsync(string email);
        Task<IQueryable<Nhanvien>> GetAllEmAsync();
        Task<Nhanvien> CheckEmailExist(string email, int nhanvienid);
    }
}
