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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Crisis
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;
        private readonly Crisis.Data.ExternalContext _externalcontext;

        public IndexModel(Crisis.Data.CrisisContext context, Crisis.Data.ExternalContext externalcontext)
        {
            _context = context;
            _externalcontext = externalcontext;
        }

        public string StatusMessage { get; set; }
        public IList<SupplierDetail> SupplierDetails { get; set; }

        //Attachment Fields
        public Attachment Attachment { get; set; }
        public IFormFile FileAttached { get; set; }
        //Comment
        public Comment Comment { get; set; }
        //Call
        public Call Call { get; set; }
        

        public async Task OnGetAsync()
        {
            ViewData["AttachmentType"] = new SelectList(_context.AttachmentType, "Id", "Description");
            ViewData["CallResponse"] = new SelectList(_context.CallResponse, "Id", "Description");
            ViewData["Escalation"] = new SelectList(_context.Escalation, "Id", "Description");
            await SetModel();
        }

        public async Task OnPostCreateAttachment()
        {
            // Check if a) a file is attached and b) if it's an xlsx or not, before processing. Otherwise it will throw error.
            if (FileAttached != null)
            {
                // Name the file
                Attachment.FilePath = Path.Combine(@"\\ahmtroy03\sppcsharedfiles\6290 Procurement Operations\_Public\Data\Crisis\COVID-19\Attachments",
                 Attachment.SupplierId + "_" + DateTime.Now.ToString("yyyy-MM-dd_hhmmsstt") + "_" + FileAttached.FileName);

                // Save the file and update models
                using var fileStream = new FileStream(Attachment.FilePath, FileMode.Create, FileAccess.ReadWrite);
                try
                {
                    await FileAttached.CopyToAsync(fileStream);
                    fileStream.Dispose();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Object reference not set to an instance of an object.")
                    {
                        StatusMessage = "Error: Check your file and try again";
                    }
                    else
                    {
                        StatusMessage = "Error: An error occured while importing. " + ex.Message;
                    }
                }
            }
            else
            {
                StatusMessage = "Error: Please attaching file.";
            }

            if (!ModelState.IsValid)
            {
                //This is here just in case I figure this out but for some reason it doesn't acknowledge the FilePath even though it's there.
            }

            _context.Attachment.Add(Attachment);
            await _context.SaveChangesAsync();

            StatusMessage = "Attachment added!";
            await OnGetAsync();
        }

        public async Task OnPostCreateCall()
        {
            _context.Call.Add(Call);
            await _context.SaveChangesAsync();
            StatusMessage = "Call added!";
            await OnGetAsync();
        }

        public async Task OnPostCreateComment()
        {
            _context.Comment.Add(Comment);
            await _context.SaveChangesAsync();
            StatusMessage = "Comment added!";
            await OnGetAsync();
        }

        private async Task SetModel()
        {
            List<Supplier> Suppliers = new List<Supplier>();
            List<Models.ExternalData.VwSrmAhmSupplier> VwSrmAhmSuppliers = new List< Models.ExternalData.VwSrmAhmSupplier> ();
            List<Models.ExternalData.VwSrmDeliveryPrimary> VwSrmDeliveryPrimaries = new List<Models.ExternalData.VwSrmDeliveryPrimary>();

            Suppliers = await _context.Supplier
                .Include(s => s.Status)
                .Include(s => s.Category)
                .Include(s => s.Comments)
                .Include(s => s.Calls)
                .Include("Calls.CallResponse")
                .OrderByDescending(c => c.CreateDate)
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
