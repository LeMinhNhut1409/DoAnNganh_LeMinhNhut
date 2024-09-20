using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.ViewModels;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;


namespace QUANLY_KHACHSAN.Repositories
{
    public class SaleReportRepository : ISaleReportRepository
    {
        private readonly QUANLY_KHACHSANContext _htDbContext;
        public SaleReportRepository(QUANLY_KHACHSANContext htDbContext)
        {
            _htDbContext = htDbContext;
        }

        public async Task<List<SaleReportViewModel>> GetSaleReportForMonthYear(int month)
        {
            var saleReports = _htDbContext.Hoadons
                .Where(hd => hd.Ngaylaphd.Month == month && hd.Ngaylaphd.Year == DateTime.Now.Year)
                .Select(hd => new { hd.Tenphong, hd.Tongtien })
                .ToList();

            var aggregatedSaleReports = saleReports
                .GroupBy(sr => sr.Tenphong)
                .Select(group => new SaleReportViewModel
                {
                    loaiphongNavigation = _htDbContext.Phongs
                        .Where(p => p.Tenphong == group.Key)
                        .Select(p => p.MaloaiphongNavigation)
                        .FirstOrDefault(),
                    doanhThu = group.Sum(item => item.Tongtien)
                })
                .GroupBy(sr => sr.loaiphongNavigation)
                .Select(group => new SaleReportViewModel
                {
                    loaiphongNavigation = group.Key,
                    doanhThu = group.Sum(item => item.doanhThu)
                })
                .ToList();

            var totalRevenue = aggregatedSaleReports.Sum(sr => sr.doanhThu);

            // Calculate percentages
            foreach (var saleReport in aggregatedSaleReports)
            {
                saleReport.tyle = totalRevenue != 0 ? (float)saleReport.doanhThu / totalRevenue : 0;
            }

            return aggregatedSaleReports;
        }


    }
}
