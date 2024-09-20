using System;
using System.Collections.Generic;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Hoadon
    {
        public int Mahd { get; set; }
        public int Songayo { get; set; }
        public int Manv { get; set; }
        public int Tongtien { get; set; }
        public string Tenkh { get; set; } = null!;
        public string Tenphong { get; set; } = null!;
        public DateTime Ngaylaphd { get; set; }
        public DateTime Ngaydat { get; set; }
        public double Tylephuthu { get; set; }
        public int? IdphuThu { get; set; }
        public string Cccd { get; set; } = null!;

        public virtual Phuthu IdphuThuNavigation { get; set; } = null!;
        public virtual Nhanvien ManvNavigation { get; set; } = null!;
    }
}
