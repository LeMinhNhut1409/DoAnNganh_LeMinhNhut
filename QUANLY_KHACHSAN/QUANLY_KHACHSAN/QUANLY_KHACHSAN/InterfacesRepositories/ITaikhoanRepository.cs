using System.Threading.Tasks;
using System.Collections.Generic;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface ITaikhoanRepository
    {
        Task<Taikhoan> GetByUsernameAndPasswordAsync(string tentknv, string mktk);
        Task AddAsync(Taikhoan taikhoan);
        Task DeleteByManv(int manv);
        Task UpdateByNv(int manv, string newEmail);
        Task CreateAccountForAllEmployee(IEnumerable<Nhanvien> employeesWithoutAccounts);
        // Các phương thức khác liên quan đến tài khoản có thể được thêm vào tùy theo yêu cầu.

    }
}
