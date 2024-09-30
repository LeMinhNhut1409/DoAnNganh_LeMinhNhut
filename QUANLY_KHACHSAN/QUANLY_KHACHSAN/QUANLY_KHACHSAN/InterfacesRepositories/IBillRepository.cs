using QUANLY_KHACHSAN.Models;

public interface IBillRepository
{
    // Phương thức bất đồng bộ để lấy danh sách tất cả hóa đơn
    Task<List<Hoadon>> GetAllBills();
    // Phương thức bất đồng bộ để lấy hóa đơn theo ID
    Task<Hoadon> GetBillById(int id);
    // Phương thức bất đồng bộ để tạo một hóa đơn mới
    Task CreateBill(Hoadon hoadon);
    // Phương thức bất đồng bộ để cập nhật thông tin hóa đơn
    Task UpdateBill(Hoadon hoadon);
    // Phương thức bất đồng bộ để xóa hóa đơn theo ID
    Task DeleteBill(int id);
    // Phương thức bất đồng bộ để kiểm tra xem hóa đơn có tồn tại hay không theo ID
    Task<bool> BillExists(int id);

}