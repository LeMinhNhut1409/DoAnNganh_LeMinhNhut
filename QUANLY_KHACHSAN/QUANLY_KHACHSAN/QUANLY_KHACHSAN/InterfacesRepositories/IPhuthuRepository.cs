using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface IPhuthuRepository
    {
        // Phương thức bất đồng bộ để lấy tất cả phụ thu
        Task<List<Phuthu>> GetAllPhuthusAsync();

        // Phương thức bất đồng bộ để lấy phụ thu theo ID
        Task<Phuthu> GetPhuthuByIdAsync(double id);

        // Phương thức bất đồng bộ để thêm một phụ thu mới
        Task AddPhuthuAsync(Phuthu phuthu);

        // Phương thức bất đồng bộ để cập nhật thông tin phụ thu
        Task UpdatePhuthuAsync(Phuthu phuthu);

        // Phương thức bất đồng bộ để xóa phụ thu theo ID
        Task DeletePhuthuAsync(double id);

        // Phương thức bất đồng bộ để kiểm tra xem phụ thu có tồn tại hay không theo ID
        Task<bool> PhuthuExistsAsync(double id);

        // Phương thức bất đồng bộ để lấy phụ thu đầu tiên trong danh sách
        Task<Phuthu> GetFirstPhuthuAsync();
    }

}
