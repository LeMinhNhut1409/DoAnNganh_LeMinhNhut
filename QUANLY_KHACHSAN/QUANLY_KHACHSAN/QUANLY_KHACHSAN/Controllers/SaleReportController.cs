using Microsoft.AspNetCore.Mvc;
using QUANLY_KHACHSAN.ViewModels;
using QUANLY_KHACHSAN.InterfacesRepositories;
using OfficeOpenXml; // Để xuất Excel
using iText.Kernel.Pdf; // Để xuất PDF
using iText.Layout; // Để xuất PDF
using iText.Layout.Element; // Để xuất PDF
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;


namespace QUANLY_KHACHSAN.Controllers
{
    public class SaleReportController : Controller
    {

        private readonly ISaleReportRepository _dbsalereport;
        public SaleReportController(ISaleReportRepository _context)
        {
            _dbsalereport = _context;
        }
        public async Task<IActionResult> Index(string sortOrder, int manager)
        {
            TempData["Manager"] = manager;
            ViewBag.Month = DateTime.Now.Month;

            List<SaleReportViewModel> salerp = await _dbsalereport.GetSaleReportForMonthYear(ViewBag.Month);

            // Sorting logic for 'Tỷ lệ'
            ViewData["TyleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "tyle_desc" : "";

            switch (sortOrder)
            {
                case "tyle_desc":
                    salerp = salerp.OrderByDescending(s => s.tyle).ToList();
                    break;
                default:
                    salerp = salerp.OrderBy(s => s.tyle).ToList();
                    break;
            }

            return View(salerp);
        }


        [HttpPost]
        public async Task<IActionResult> Index(int month, string sortOrder, int manager)
        {
            TempData["Manager"] = manager;
            ViewBag.Month = month;

            List<SaleReportViewModel> salerp = await _dbsalereport.GetSaleReportForMonthYear(ViewBag.Month);

            // Sorting logic for 'Tỷ lệ'
            ViewData["TyleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "tyle_desc" : "";

            switch (sortOrder)
            {
                case "tyle_desc":
                    salerp = salerp.OrderByDescending(s => s.tyle).ToList();
                    break;
                default:
                    salerp = salerp.OrderBy(s => s.tyle).ToList();
                    break;
            }

            return View(salerp);
        }


        // Xuất PDF
        public IActionResult ExportToPdf()
        {
            var salerp = _dbsalereport.GetSaleReportForMonthYear(DateTime.Now.Month).Result;

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);


                // Thêm tiêu đề
                document.Add(new Paragraph($"Báo Cáo Doanh Số - Tháng {DateTime.Now.Month}").SetFontSize(20));

                // Thêm bảng
                var table = new Table(3); // Số cột tùy theo mô hình dữ liệu của bạn
                table.AddHeaderCell("Loại phòng");
                table.AddHeaderCell("Doanh thu");
                table.AddHeaderCell("Tỷ Lệ");


                foreach (var item in salerp)
                {
                    table.AddCell(item.loaiphongNavigation.Tenloai.ToString());
                    table.AddCell(item.doanhThu.ToString());
                    table.AddCell(item.tyle.ToString());
                }

                document.Add(table);
                document.Close();

                var fileContent = stream.ToArray();
                return File(fileContent, "application/pdf", "SaleReport.pdf");
            }
        }

        // Xuất Excel
        public IActionResult ExportToExcel()
        {
            var salerp = _dbsalereport.GetSaleReportForMonthYear(DateTime.Now.Month).Result;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add($"Báo Cáo Doanh Số - Tháng {DateTime.Now.Month}");
                // Thêm tiêu đề cho báo cáo
                worksheet.Cells[1, 1].Value = "Báo Cáo Doanh Số - Tháng " + DateTime.Now.Month;
                worksheet.Cells[1, 1, 1, 3].Merge = true; // Gộp ô từ cột 1 đến cột 3
                worksheet.Cells[1, 1].Style.Font.Bold = true; // Đặt chữ in đậm
                worksheet.Cells[1, 1].Style.Font.Size = 20; // Kích thước font
                // Thêm tiêu đề cột
                worksheet.Cells[2, 1].Value = "Loại phòng";
                worksheet.Cells[2, 2].Value = "Doanh thu";
                worksheet.Cells[2, 3].Value = "Tỷ Lệ";

                int row = 2;
                foreach (var item in salerp)
                {
                    worksheet.Cells[row, 1].Value = item.loaiphongNavigation.Tenloai;
                    worksheet.Cells[row, 2].Value = item.doanhThu;
                    worksheet.Cells[row, 3].Value = item.tyle;
                    row++;
                }

                var fileContent = package.GetAsByteArray();
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SaleReport.xlsx");
            }
        }
    }
}
