using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.ViewModels
{
    public class Bill
    {
        public Khachhang khachhang { get; set; }
        public List<Phieuthue> phieuthues { get; set; }
        public Phuthu phuthu { get; set; }
        public int PhieuThueId { get; set; }
        public string TenPhong { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal DonGia { get; set; }
    }

}
