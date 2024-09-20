using QUANLY_KHACHSAN.ViewModels;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface ISaleReportRepository
    {
        Task<List<SaleReportViewModel>> GetSaleReportForMonthYear(int month);
    }
}
