using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.Repositories
{
    public interface INhanvienRepository
    {
        // Phương thức bất đồng bộ để lấy nhân viên theo ID
        Task<Nhanvien> GetByIdAsync(int id);

        // Phương thức bất đồng bộ để lấy tất cả nhân viên, trả về IQueryable (cho phép truy vấn LINQ)
        Task<IQueryable<Nhanvien>> GetAllAsync();

        // Phương thức bất đồng bộ để thêm một nhân viên mới
        Task AddAsync(Nhanvien nhanvien);

        // Phương thức bất đồng bộ để cập nhật thông tin nhân viên theo ID
        Task UpdateAsync(Nhanvien nhanvien, int nhanvienid);

        // Phương thức bất đồng bộ để xóa nhân viên theo ID
        Task DeleteAsync(int Id);

        // Phương thức bất đồng bộ để lấy nhân viên theo ID
        Task<Nhanvien> GetEmployeeByIdAsync(int id);

        // Phương thức bất đồng bộ để lấy tất cả nhân viên
        Task<IQueryable<Nhanvien>> GetAllEmployeesAsync();

        // Phương thức bất đồng bộ để lấy danh sách nhân viên không có tài khoản
        Task<IQueryable<Nhanvien>> GetEmployeeNoAccount();

        // Phương thức bất đồng bộ để lấy nhân viên theo email
        Task<Nhanvien> GetByEmailAsync(string email);

        // Phương thức bất đồng bộ để lấy tất cả nhân viên (có thể có điều kiện khác)
        Task<IQueryable<Nhanvien>> GetAllEmAsync();

        // Phương thức bất đồng bộ để kiểm tra sự tồn tại của email, trả về nhân viên nếu có
        Task<Nhanvien> CheckEmailExist(string email, int nhanvienid);

        Task<bool> NhanvienExists(int id);
        Task<List<string>> GetDistinctEmAsync();
        Task<int?> GetManvByEmailAsync(string email);
    }
}
