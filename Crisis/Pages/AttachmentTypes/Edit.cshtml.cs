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

namespace Crisis.Pages.AttachmentTypes
{
    public class EditModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public EditModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AttachmentType AttachmentType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AttachmentType = await _context.AttachmentType.FirstOrDefaultAsync(m => m.Id == id);

            if (AttachmentType == null)
            {
                return NotFound();
            }
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

            _context.Attach(AttachmentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttachmentTypeExists(AttachmentType.Id))
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

        private bool AttachmentTypeExists(int id)
        {
            return _context.AttachmentType.Any(e => e.Id == id);
        }
    }
}
