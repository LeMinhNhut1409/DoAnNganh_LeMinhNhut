using System;
using System.Collections.Generic;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Phong
    {
        public Phong()
        {
            Khachhangs = new HashSet<Khachhang>();
            Phieuthues = new HashSet<Phieuthue>();
            Tapvus = new HashSet<Tapvu>();
        }

        public int Map { get; set; }
        public string Tenphong { get; set; } = null!;
        public int Tinhtrang { get; set; }
        public int Soluongkhachtoida { get; set; }
        public string? Ghichu { get; set; }
        public int Maloaiphong { get; set; }
        public int Songayo { get; set; }
        public virtual Loaiphong MaloaiphongNavigation { get; set; } = null!;
        public virtual ICollection<Khachhang> Khachhangs { get; set; }
        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
        public virtual ICollection<Tapvu> Tapvus { get; set; }
    }
}
