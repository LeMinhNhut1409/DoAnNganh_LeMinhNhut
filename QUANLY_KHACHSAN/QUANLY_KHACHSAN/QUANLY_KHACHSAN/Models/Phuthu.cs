using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Phuthu
    {
        public Phuthu()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public int Idphuthu { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá trị phụ thu")]
        public double? Giatriphuthu { get; set; }

        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
