using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crisis.Data;
using Crisis.Models;

namespace Crisis
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public CreateModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Status"] = new SelectList(_context.Set<Status>(), "Id", "Description");
            ViewData["Category"] = new SelectList(_context.Set<Category>(), "Id", "Description");
            ViewData["Escalation"] = new SelectList(_context.Set<Escalation>(), "Id", "Description");
            return Page();
        }

        public string StatusMessage { get; set; }
        public Supplier Supplier { get; set; }
        public string Comment { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //Check and make sure all the fields are filled in. Also make sure you aren't adding a duplicate.
            if (!ModelState.IsValid || _context.Supplier.Any(s => s.SupplierNo.Equals(Supplier.SupplierNo)) || Comment is null)
            {
                return RedirectToPage("./Index");
            }


            _context.Supplier.Add(Supplier);
            await _context.SaveChangesAsync();

            Comment newComment = new Comment
            {
                Id = 0,
                SupplierId = Supplier.Id,
                Supplier = Supplier,
                CreateDate = Supplier.CreateDate,
                Creator = Supplier.Creator,
                Body = Comment,
            };

            _context.Comment.Add(newComment);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
