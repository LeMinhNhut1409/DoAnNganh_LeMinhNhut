using QUANLY_KHACHSAN.ViewModels;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface ISaleReportRepository
    {
        // Phương thức bất đồng bộ để lấy báo cáo doanh thu cho một tháng cụ thể
        Task<List<SaleReportViewModel>> GetSaleReportForMonthYear(int month);
    }
}
