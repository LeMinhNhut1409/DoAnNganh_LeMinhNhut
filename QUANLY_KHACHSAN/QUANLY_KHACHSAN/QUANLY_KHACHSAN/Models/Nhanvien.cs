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
            Baoves = new HashSet<Baove>();
            Monans = new HashSet<Monan>();
            Tapvus = new HashSet<Tapvu>();
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

        // Thêm thuộc tính ChucVu
        [Required(ErrorMessage = "Chức vụ không thể thiếu")]
        public string Chucvu { get; set; } = null!;// Lễ tân, bảo vệ, nhà bếp, tạp vụ

        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
        public virtual ICollection<Baove> Baoves { get; set; }
        public virtual ICollection<Monan> Monans { get; set; }
        public virtual ICollection<Tapvu> Tapvus { get; set; }

    }
}
