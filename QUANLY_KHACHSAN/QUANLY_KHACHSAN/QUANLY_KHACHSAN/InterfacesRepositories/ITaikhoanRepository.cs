using System.Threading.Tasks;
using System.Collections.Generic;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface ITaikhoanRepository
    {
        // Phương thức bất đồng bộ để lấy tài khoản theo tên đăng nhập và mật khẩu
        Task<Taikhoan> GetByUsernameAndPasswordAsync(string tentknv, string mktk);

        // Phương thức bất đồng bộ để thêm một tài khoản mới
        Task AddAsync(Taikhoan taikhoan);

        // Phương thức bất đồng bộ để xóa tài khoản theo mã nhân viên
        Task DeleteByManv(int manv);

        // Phương thức bất đồng bộ để cập nhật email của tài khoản theo mã nhân viên
        Task UpdateByNv(int manv, string newEmail);

        // Phương thức bất đồng bộ để tạo tài khoản cho tất cả nhân viên không có tài khoản
        Task CreateAccountForAllEmployee(IEnumerable<Nhanvien> employeesWithoutAccounts);

        
    }
}
