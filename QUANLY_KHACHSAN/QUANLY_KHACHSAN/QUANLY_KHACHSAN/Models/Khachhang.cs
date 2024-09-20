using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Phieuthues = new HashSet<Phieuthue>();
        }

        public int Makh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Tenkh { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tuổi")]
        public int Tuoi { get; set; }
        [Required(ErrorMessage = "Số điện thoại không thể thiếu")]
        [RegularExpression(@"^(?:[0-9]|\+){1,}[0-9]{9,}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Tel { get; set; } = null!;
        public string? Diachikh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập cccd")]
        public string Cccdkh { get; set; } = null!;
        public int Maloaikhach { get; set; }
        public int Map { get; set; }

        public virtual Loaikhach MaloaikhachNavigation { get; set; } = null!;
        public virtual Phong MapNavigation { get; set; } = null!;
        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
    }
}
