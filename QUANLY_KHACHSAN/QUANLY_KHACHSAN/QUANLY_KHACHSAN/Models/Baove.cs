using System;
namespace QUANLY_KHACHSAN.Models
{
	public class Baove
    {

            public int Mabv { get; set; }
            public string LicensePlate { get; set; }
            public DateTime CheckInDate { get; set; }
            public DateTime? CheckOutDate { get; set; }
            public int Manv { get; set; } // Khóa ngoại đến nhân viên

        public virtual Nhanvien ManvNavigation { get; set; } = null!;
        
    }
}

