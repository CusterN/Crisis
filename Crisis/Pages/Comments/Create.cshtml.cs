using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public CreateModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? SupplierId)
        {
            if (SupplierId == null)
            {
                ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "SupplierNo");
                return Page();
            }
            else
            {
                //limit the Supplier to be the one you came in with.
                ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "SupplierNo", SupplierId);
                return Page();
            }

        }

        [BindProperty]
        public Comment Comment { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comment.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Details", new { Id = Comment.SupplierId });
        }
    }
}
