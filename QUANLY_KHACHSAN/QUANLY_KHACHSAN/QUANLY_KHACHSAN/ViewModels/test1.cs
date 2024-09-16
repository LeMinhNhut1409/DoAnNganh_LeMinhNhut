using System.ComponentModel.DataAnnotations;

namespace QUANLY_KHACHSAN.ViewModels
{
    public class test1
    {
        [DataType(DataType.Date)]
        public DateTime Ngaylaphd { get; set; }

        public string? CCCD { get; set; }

    }
}
