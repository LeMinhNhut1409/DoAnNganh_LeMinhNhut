using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface IKhachhangRepository
    {
        Task<Khachhang> GetByIdAsync(int id);
        IQueryable<Khachhang> GetAllAsync();
        Task AddAsync(Khachhang khach);
        Task UpdateAsync(Khachhang khach, int id);
        Task DeleteAsync(int Id);
        Task<List<Loaikhach>> GetAllLoaikhach();
        Task<Loaikhach> GetClientTypeById(int id);
        Task<List<string>> GetDistinctClientTypeAsync();
        Task<List<Khachhang>> GetCustomersByRoomIdAsync(int roomId);
        Task<Khachhang> GetClientByCCCDAsync(string cccd);
        Task<Khachhang> GetClientByIDAsync(int id);
        Task<int> GetSoLuongKhachByMapAsync(int map);
        Task<IEnumerable<Khachhang>> GetCustomersByRoomNameAsync(string tenPhong);
        Task DeleteCustomersByRoomAsync(string tenPhong, string CCCD);
        Task<int> GetClientIdByName(string tenKhachHang);
    }
}
