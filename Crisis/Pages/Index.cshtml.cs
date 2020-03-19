using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;
using OfficeOpenXml;

namespace Crisis
{
    public class IndexModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public IndexModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }
        public IList<Supplier> Supplier { get;set; }

        public async Task OnGetAsync(Boolean? All)
        {
            //don't show all records unless explicity asked to!
            if (All == true)
            {
                Supplier = await _context.Supplier
                    .Include(s => s.Status)
                    .Include(c => c.Category)
                    .Include(c => c.Comments)
                    .OrderByDescending(c => c.CreateDate)
                    .ToListAsync();
            }
            else
            {
                Supplier = await _context.Supplier
                    .Include(s => s.Status)
                    .Include(c => c.Category)
                    .Include(c => c.Comments)
                    .OrderByDescending(c => c.CreateDate)
                    .Where(m => m.Visible == true)
                    .ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostExport()
        {
            // Name of file
            string sFileName = @"PartQueueRequests.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                // Name of worksheet(s)
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("CrisisSuppliers");

                // Query table to export
                Supplier = await _context.Supplier
                    .Include(s => s.Status)
                    .Include(c => c.Category)
                    .Include(c => c.Comments)
                    .OrderByDescending(c => c.CreateDate)
                    .ToListAsync();

                // Header of excel file
                ws.Cells[1, 1].Value = "SupplierNo";
                ws.Cells[1, 2].Value = "CreateDate";
                ws.Cells[1, 3].Value = "Creator";
                ws.Cells[1, 4].Value = "Category";
                ws.Cells[1, 5].Value = "Status";
                ws.Cells[1, 5].Value = "Comment";

                // Body of excel file
                int recordIndex = 2;
                foreach (var item in Supplier)
                {
                    ws.Cells[recordIndex, 1].Value = item.SupplierNo;
                    ws.Cells[recordIndex, 2].Value = item.CreateDate.ToString("yyyy-MM-dd HH:mm:ss tt");
                    ws.Cells[recordIndex, 3].Value = item.Creator;
                    ws.Cells[recordIndex, 4].Value = item.Category.Description;
                    ws.Cells[recordIndex, 5].Value = item.Status.Description;
                    ws.Cells[recordIndex, 5].Value = item.Comments.Last().Body;
                    recordIndex++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();
                return File(pck.GetAsByteArray(), "application/octet-stream", sFileName);
            }
        }
    }
}
