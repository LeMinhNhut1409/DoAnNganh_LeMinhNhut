using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QUANLY_KHACHSAN.Repositories
{ 
public interface IPhieuthueRepository
{
        // Phương thức bất đồng bộ để lấy tất cả phiếu thuê
        Task<List<Phieuthue>> GetAllAsync();

        // Phương thức bất đồng bộ để lấy phiếu thuê theo ID
        Task<Phieuthue> GetByIdAsync(int id);

        // Phương thức bất đồng bộ để thêm một phiếu thuê mới
        Task AddAsync(Phieuthue phieuthue);

        // Phương thức bất đồng bộ để cập nhật thông tin phiếu thuê
        Task UpdateAsync(Phieuthue phieuthue);

        // Phương thức bất đồng bộ để xóa phiếu thuê theo ID
        Task DeleteAsync(int id);

        // Phương thức bất đồng bộ để kiểm tra xem phiếu thuê có tồn tại hay không theo ID
        Task<bool> RentExists(int id);

        // Phương thức bất đồng bộ để lấy danh sách mã khách hàng khác nhau
        Task<List<int>> GetDistinctMakhAsync();

        // Phương thức bất đồng bộ để lấy danh sách mã phòng khác nhau
        Task<List<int>> GetDistinctMapAsync();

        // Phương thức bất đồng bộ để lấy danh sách mã nhân viên khác nhau
        Task<List<int>> GetDistinctManvAsync();

        // Phương thức bất đồng bộ để lấy danh sách phiếu thuê theo mã khách hàng
        Task<List<Phieuthue>> GetRentsByCustomerIdAsync(int makh);

        // Phương thức bất đồng bộ để lấy danh sách phiếu thuê theo ID khách hàng
        Task<List<Phieuthue>> GetRentsCustomerIdAsync(int customerId);

        // Phương thức bất đồng bộ để lấy tất cả khách hàng
        Task<List<int>> GetAllCustomerAsync();

        // Phương thức bất đồng bộ để lấy tên khách hàng theo mã khách hàng
        Task<string> GetTenkhachhangByMakhAsync(int makh);

        // Phương thức bất đồng bộ để lấy phiếu thuê theo số CCCD
        Task<Phieuthue> GetClientByCCCDAsync(string cccd);

        // Phương thức bất đồng bộ để lấy phiếu thuê theo mã khách hàng
        Task<Phieuthue> GetRentByIDAsync(int makh);

        // Phương thức bất đồng bộ để lấy phiếu thuê theo mã phòng
        Task<Phieuthue> GetRentAsync(int mapt);

        // Phương thức bất đồng bộ để lấy danh sách phiếu thuê theo số CCCD
        Task<List<Phieuthue>> GetRentByCCCDAsync(string cccd);

        // Phương thức bất đồng bộ để lấy tất cả phiếu thuê theo số CCCD
        Task<List<Phieuthue>> GetAllRentsByCCCDAsync(string cccd);

        // Phương thức bất đồng bộ để kiểm tra xem số CCCD đã tồn tại trong phiếu thuê hay chưa
        Task<bool> CheckCCCDExistsInPhieuthueAsync(string cccd);

        // Phương thức bất đồng bộ để lấy tất cả nhân viên
        Task<List<Nhanvien>> GetAllEmployeesAsync();

        // Phương thức bất đồng bộ để lấy danh sách phiếu thuê theo mã nhân viên
        Task<List<Phieuthue>> GetRentsByEmployeeIdAsync(int manv);

    }
}