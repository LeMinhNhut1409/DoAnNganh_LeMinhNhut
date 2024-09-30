using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface ILoaiKhachRepository
    {
        // Phương thức bất đồng bộ để lấy loại khách hàng theo ID
        Task<Loaikhach> GetByIdAsync(int id);

        // Phương thức để lấy tất cả loại khách hàng dưới dạng IQueryable (cho phép truy vấn LINQ)
        IQueryable<Loaikhach> GetAllAsync();

        // Phương thức bất đồng bộ để thêm một loại khách hàng mới
        Task AddAsync(Loaikhach loaikhach);

        // Phương thức bất đồng bộ để cập nhật thông tin loại khách hàng
        Task UpdateAsync(Loaikhach loaikhach);

        // Phương thức bất đồng bộ để xóa loại khách hàng theo ID
        Task DeleteAsync(int Id);

        // Phương thức bất đồng bộ để lấy danh sách các loại khách hàng khác nhau
        Task<List<string>> GetDistinctClientTypeAsync();

    }
}