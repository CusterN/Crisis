using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.Calls
{
    public class EditModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public EditModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Call Call { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Call = await _context.Call
                .Include(c => c.CallResponse)
                .Include(c => c.Supplier).FirstOrDefaultAsync(m => m.Id == id);

            if (Call == null)
            {
                return NotFound();
            }
           ViewData["CallResponseId"] = new SelectList(_context.CallResponse, "Id", "Description");
           ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Creator");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Call).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallExists(Call.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CallExists(int id)
        {
            return _context.Call.Any(e => e.Id == id);
        }
    }
}
