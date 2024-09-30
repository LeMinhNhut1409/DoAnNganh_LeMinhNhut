using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;


namespace QUANLY_KHACHSAN.Repositories
{
    public interface ILoaiphongRepository
    {
        // Phương thức bất đồng bộ để lấy loại phòng theo ID
        Task<Loaiphong> GetByIdAsync(int id);

        // Phương thức để lấy tất cả loại phòng dưới dạng IQueryable (cho phép truy vấn LINQ)
        IQueryable<Loaiphong> GetAllAsync();

        // Phương thức bất đồng bộ để thêm một loại phòng mới
        Task AddAsync(Loaiphong loaiphong);

        // Phương thức bất đồng bộ để cập nhật thông tin loại phòng theo ID
        Task UpdateAsync(Loaiphong loaiphong, int id);

        // Phương thức bất đồng bộ để xóa loại phòng theo ID
        Task DeleteAsync(int Id);

        // Phương thức bất đồng bộ để lấy danh sách các loại phòng khác nhau
        Task<List<string>> GetDistinctRoomTypesAsync();

    }
}
