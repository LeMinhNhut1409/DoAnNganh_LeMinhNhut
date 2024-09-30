using System.Threading.Tasks;
using QUANLY_KHACHSAN.Models;


namespace QUANLY_KHACHSAN.Repositories
{
    public interface IPhongRepository
    {
        // Phương thức bất đồng bộ để lấy phòng theo ID
        Task<Phong> GetByIdAsync(int id);

        // Phương thức để lấy tất cả phòng dưới dạng IQueryable (cho phép truy vấn LINQ)
        IQueryable<Phong> GetAllAsync();

        // Phương thức bất đồng bộ để thêm một phòng mới
        Task AddAsync(Phong phong);

        // Phương thức bất đồng bộ để cập nhật thông tin phòng
        Task UpdateAsync(Phong phong);

        // Phương thức bất đồng bộ để xóa phòng theo ID
        Task DeleteAsync(int Id);

        // Phương thức bất đồng bộ để lấy tất cả loại phòng
        Task<List<Loaiphong>> GetAllLoaiphong();

        // Phương thức bất đồng bộ để lấy loại phòng theo ID
        Task<Loaiphong> GetRoomTypeById(int id);

        // Phương thức bất đồng bộ để lấy danh sách các loại phòng khác nhau
        Task<List<string>> GetDistinctRoomTypeAsync();

        // Phương thức bất đồng bộ để lấy báo cáo sử dụng phòng theo tháng
        Task<List<Phong>> GetRoomUsageReportAsync(int? thang);

        // Phương thức bất đồng bộ để lấy danh sách phòng theo tình trạng
        Task<List<Phong>> GetRoomsByTinhtrangAsync(int tinhtrang, int roomid = 0);

        // Phương thức bất đồng bộ để lấy phòng theo tên
        Task<Phong> GetRoomByNameAsync(string tenphong);

        // Phương thức bất đồng bộ để lấy báo cáo sử dụng phòng theo tháng (trùng với phương thức trên)
        Task<List<Phong>> GetRoomUsageReportAsync(int thang);

        // Phương thức bất đồng bộ để lấy danh sách số lượng khách tối đa cho các phòng
        Task<List<int>> GetSoluongkhachtoidaList();
    }
}
