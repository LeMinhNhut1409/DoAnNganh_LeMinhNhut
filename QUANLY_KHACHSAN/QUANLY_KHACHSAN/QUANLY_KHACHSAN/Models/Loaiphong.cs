using System;
using System.Collections.Generic;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Loaiphong
    {
        public Loaiphong()
        {
            Phongs = new HashSet<Phong>();
        }

        public int Maloaiphong { get; set; }
        public string Tenloai { get; set; } = null!;
        public int Dongia { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
