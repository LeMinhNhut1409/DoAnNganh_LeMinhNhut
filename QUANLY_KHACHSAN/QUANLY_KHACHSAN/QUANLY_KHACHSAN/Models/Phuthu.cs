using System;
using System.Collections.Generic;

namespace QUANLY_KHACHSAN.Models
{
    public partial class Phuthu
    {
        public Phuthu()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public int Idphuthu { get; set; }
        public double Giatriphuthu { get; set; }

        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
