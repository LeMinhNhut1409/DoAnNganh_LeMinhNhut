using System;
using System.Collections.Generic;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Loaikhach
    {
        public Loaikhach()
        {
            Khachhangs = new HashSet<Khachhang>();
        }

        public int Maloaikhach { get; set; }
        public string? Tenloaikhach { get; set; }

        public virtual ICollection<Khachhang> Khachhangs { get; set; }
    }
}
