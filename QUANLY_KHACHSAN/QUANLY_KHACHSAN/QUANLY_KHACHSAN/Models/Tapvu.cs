
using QUANLY_KHACHSAN.Models;

public class Tapvu
{
    public int Matapvu { get; set; } // Khóa chính
    public bool Dadondep { get; set; } // Trạng thái dọn dẹp
    public bool Dathemdodung { get; set; } // Trạng thái thêm đồ dùng
    public int? Soluongkhan { get; set; } // Số lượng khăn trong phòng
    public int? Soluonggagiuong { get; set; } // Số lượng ga giường trong phòng
    public int? Soluongdungcuvesinh { get; set; } // Số lượng dụng cụ vệ sinh trong phòng
    public int Map { get; set; } // Khóa ngoại
    public int Manv { get; set; } // Khóa ngoại

    public virtual Phong MapNavigation { get; set; } = null!;
    public virtual Nhanvien ManvNavigation { get; set; } = null!;
}