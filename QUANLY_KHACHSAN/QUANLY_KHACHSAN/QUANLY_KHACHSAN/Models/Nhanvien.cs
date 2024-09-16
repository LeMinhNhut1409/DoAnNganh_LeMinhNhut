using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Hoadons = new HashSet<Hoadon>();
            Phieuthues = new HashSet<Phieuthue>();
            Taikhoans = new HashSet<Taikhoan>();
        }

        public int Manv { get; set; }
        [Required(ErrorMessage = "Họ tên không thể thiếu")]
        public string Hoten { get; set; } = null!;
        [Required(ErrorMessage = "Giới tính không thể thiếu")]
        public string Gioitinh { get; set; } = null!;
        [Required(ErrorMessage = "Ngày sinh không thể thiếu")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateTime? Ngaysinh { get; set; }
        [Required(ErrorMessage = "Số điện thoại không thể thiếu")]
        [RegularExpression(@"^(?:[0-9]|\+){1,}[0-9]{9,}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Sdt { get; set; } = null!;
        [Required(ErrorMessage = "Email không thể thiếu")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = null!;
        public string? Diachi { get; set; }

        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
    }
}
