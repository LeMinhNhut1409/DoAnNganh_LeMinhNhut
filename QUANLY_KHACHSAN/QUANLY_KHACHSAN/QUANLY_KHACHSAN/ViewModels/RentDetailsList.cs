using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.ViewModels
{
    public class RentDetailsList
    {
        public Phieuthue phieuthue { get; set; }
        public List<Khachhang> khachhangs { get; set; }
        public string TenNhanVien { get; set; }
    }

}
