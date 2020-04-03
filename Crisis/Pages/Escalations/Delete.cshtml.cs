using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.Escalations
{
    public class DeleteModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public DeleteModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Escalation Escalation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Escalation = await _context.Escalation.FirstOrDefaultAsync(m => m.Id == id);

            if (Escalation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Escalation = await _context.Escalation.FindAsync(id);

            if (Escalation != null)
            {
                _context.Escalation.Remove(Escalation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
