using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Loaiphong
    {
        public Loaiphong()
        {
            Phongs = new HashSet<Phong>();
        }

        public int Maloaiphong { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại phòng")]
        public string Tenloai { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập đơn giá")]
        public int? Dongia { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
