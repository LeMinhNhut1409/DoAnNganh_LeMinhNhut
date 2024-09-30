using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface IKhachhangRepository
    {
        // Phương thức bất đồng bộ để lấy khách hàng theo ID
        Task<Khachhang> GetByIdAsync(int id);

        // Phương thức để lấy tất cả khách hàng dưới dạng IQueryable (cho phép truy vấn LINQ)
        IQueryable<Khachhang> GetAllAsync();

        // Phương thức bất đồng bộ để thêm một khách hàng mới
        Task AddAsync(Khachhang khach);

        // Phương thức bất đồng bộ để cập nhật thông tin khách hàng dựa trên ID
        Task UpdateAsync(Khachhang khach, int id);

        // Phương thức bất đồng bộ để xóa khách hàng theo ID
        Task DeleteAsync(int Id);

        // Phương thức bất đồng bộ để lấy tất cả loại khách hàng
        Task<List<Loaikhach>> GetAllLoaikhach();

        // Phương thức bất đồng bộ để lấy loại khách hàng theo ID
        Task<Loaikhach> GetClientTypeById(int id);

        // Phương thức bất đồng bộ để lấy danh sách các loại khách hàng khác nhau
        Task<List<string>> GetDistinctClientTypeAsync();

        // Phương thức bất đồng bộ để lấy khách hàng theo ID phòng
        Task<List<Khachhang>> GetCustomersByRoomIdAsync(int roomId);

        // Phương thức bất đồng bộ để lấy khách hàng theo số CCCD
        Task<Khachhang> GetClientByCCCDAsync(string cccd);

        // Phương thức bất đồng bộ để lấy khách hàng theo ID
        Task<Khachhang> GetClientByIDAsync(int id);

        // Phương thức bất đồng bộ để lấy số lượng khách hàng theo mã phòng
        Task<int> GetSoLuongKhachByMapAsync(int map);

        // Phương thức bất đồng bộ để lấy khách hàng theo tên phòng
        Task<IEnumerable<Khachhang>> GetCustomersByRoomNameAsync(string tenPhong);

        // Phương thức bất đồng bộ để xóa khách hàng theo tên phòng và CCCD
        Task DeleteCustomersByRoomAsync(string tenPhong, string CCCD);

        // Phương thức bất đồng bộ để lấy ID khách hàng theo tên
        Task<int> GetClientIdByName(string tenKhachHang);
    }
}
