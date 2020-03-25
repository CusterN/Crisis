using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;
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
        public IList<SupplierDetail> SupplierDetails { get; set; }

        public async Task OnGetAsync(bool Visible = true)
        {
            await SetModel(Visible);
        }
                
        private async Task SetModel(bool Visible)
        {
            List<Supplier> Suppliers = new List<Supplier>();
            List<Models.ExternalData.VwSrmAhmSupplier> VwSrmAhmSuppliers = new List< Models.ExternalData.VwSrmAhmSupplier> ();
            List<Models.ExternalData.VwSrmDeliveryPrimary> VwSrmDeliveryPrimaries = new List<Models.ExternalData.VwSrmDeliveryPrimary>();

            Suppliers = await _context.Supplier
                .Include(s => s.Status)
                .Include(c => c.Category)
                .Include(c => c.Comments)
                .OrderByDescending(c => c.CreateDate)
                .Where(m => m.Visible == Visible)
                .ToListAsync();

            var supplierNos = Suppliers.Select(s => s.SupplierNo).ToList();

            VwSrmAhmSuppliers = await _externalcontext.VwSrmAhmSuppliers
                .Where(v => supplierNos
                    .Any(s => s == v.AhmSupplierNo)).ToListAsync();
            VwSrmDeliveryPrimaries = await _externalcontext.VwSrmDeliveryPrimaries
                .Where(v => supplierNos
                    .Any(s => s == v.AhmSupplierNo)).ToListAsync();

            SupplierDetails = new List<SupplierDetail>();

            foreach (Supplier supplier in Suppliers)
            {
                SupplierDetail supplierDetail = new SupplierDetail
                {
                    Supplier = supplier,
                    VwSrmAhmSupplier = VwSrmAhmSuppliers.Where(s => s.AhmSupplierNo == supplier.SupplierNo).FirstOrDefault(),
                    VwSrmDeliveryPrimary = VwSrmDeliveryPrimaries.Where(s => s.AhmSupplierNo == supplier.SupplierNo).FirstOrDefault()
                };
                SupplierDetails.Add(supplierDetail);
            }
        }
    }
}
