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
using System.Collections;

namespace Crisis
{
    public class IndexModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;
        private readonly Crisis.Data.ExternalContext _externalcontext;

        public IndexModel(Crisis.Data.CrisisContext context, Crisis.Data.ExternalContext externalcontext)
        {
            _context = context;
            _externalcontext = externalcontext;
        }
        //public IList<Supplier> Suppliers { get;set; }
        //public IList<Models.ExternalData.VwSrmAhmSupplier> VwSrmAhmSuppliers { get; set; }
        public IList<SupplierDetail> SupplierDetails { get; set; }

        public async Task OnGetAsync(Boolean? All)
        {
            await SetModel(All);
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
                await SetModel(true);

                // Header of excel file
                ws.Cells[1, 1].Value = "SupplierNo";
                ws.Cells[1, 2].Value = "AhmSupplierNm";
                ws.Cells[1, 3].Value = "CreateDate";
                ws.Cells[1, 4].Value = "Creator";
                ws.Cells[1, 5].Value = "Category";
                ws.Cells[1, 6].Value = "Status";
                ws.Cells[1, 7].Value = "Comment";

                // Body of excel file
                int recordIndex = 2;
                foreach (var item in SupplierDetails)
                {
                    ws.Cells[recordIndex, 1].Value = item.Supplier.SupplierNo;
                    ws.Cells[recordIndex, 2].Value = item.VwSrmAhmSupplier.AhmSupplierNm;
                    ws.Cells[recordIndex, 3].Value = item.Supplier.CreateDate.ToString("yyyy-MM-dd HH:mm:ss tt");
                    ws.Cells[recordIndex, 4].Value = item.Supplier.Creator;
                    ws.Cells[recordIndex, 5].Value = item.Supplier.Category.Description;
                    ws.Cells[recordIndex, 6].Value = item.Supplier.Status.Description;
                    ws.Cells[recordIndex, 7].Value = item.Supplier.Comments.Last().Body;
                    recordIndex++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();
                return File(pck.GetAsByteArray(), "application/octet-stream", sFileName);
            }
        }

        private async Task SetModel(bool? All)
        {
            List<Supplier> Suppliers = new List<Supplier>();
            List<Models.ExternalData.VwSrmAhmSupplier> VwSrmAhmSuppliers = new List< Models.ExternalData.VwSrmAhmSupplier> ();
            //don't show all records unless explicity asked to!
            if (All == true)
            {
                Suppliers = await _context.Supplier
                    .Include(s => s.Status)
                    .Include(c => c.Category)
                    .Include(c => c.Comments)
                    .OrderByDescending(c => c.CreateDate)
                    .ToListAsync();
            }
            else
            {
                Suppliers = await _context.Supplier
                    .Include(s => s.Status)
                    .Include(c => c.Category)
                    .Include(c => c.Comments)
                    .OrderByDescending(c => c.CreateDate)
                    .Where(m => m.Visible == true)
                    .ToListAsync();
            }
            var supplierNos = Suppliers.Select(s => s.SupplierNo).ToList();

            VwSrmAhmSuppliers = await _externalcontext.VwSrmAhmSuppliers
                .Where(v => supplierNos
                    .Any(s => s == v.AhmSupplierNo)).ToListAsync();

            SupplierDetails = new List<SupplierDetail>();

            foreach (Supplier supplier in Suppliers)
            {
                SupplierDetail supplierDetail = new SupplierDetail
                {
                    Supplier = supplier,
                    VwSrmAhmSupplier = VwSrmAhmSuppliers.Where(s => s.AhmSupplierNo == supplier.SupplierNo).First()
                };
                SupplierDetails.Add(supplierDetail);
            }
        }
    }
}
