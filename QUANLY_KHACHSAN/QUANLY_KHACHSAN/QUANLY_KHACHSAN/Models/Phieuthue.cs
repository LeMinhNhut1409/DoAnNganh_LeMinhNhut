using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Phieuthue
    {
        public int Mapt { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày lập phiếu thuê")]
        public DateTime Ngaylappt { get; set; }
        public int Makh { get; set; }
        public int Map { get; set; }
        public int Manv { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập cccd")]
        public string Cccd { get; set; } = null!;

        public virtual Khachhang MakhNavigation { get; set; } = null!;
        public virtual Nhanvien ManvNavigation { get; set; } = null!;
        public virtual Phong MapNavigation { get; set; } = null!;
    }
}
