namespace QUANLY_KHACHSAN.Models
{
    public class Monan
    {
        public int Mamonan { get; set; }
        public string Tenmon { get; set; }
        public string Mota { get; set; }
        public decimal Gia { get; set; }
        public int Thoigianchebien { get; set; } // Thời gian chế biến (phút)

        // Khóa ngoại đến nhân viên nhà bếp
        public int Manv { get; set; }
        public virtual Nhanvien ManvNavigation { get; set; } = null!;
    }
}