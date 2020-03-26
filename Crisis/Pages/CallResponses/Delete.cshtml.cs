using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.CallResponses
{
    public class DeleteModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public DeleteModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CallResponse CallResponse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CallResponse = await _context.CallResponse.FirstOrDefaultAsync(m => m.Id == id);

            if (CallResponse == null)
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

            CallResponse = await _context.CallResponse.FindAsync(id);

            if (CallResponse != null)
            {
                _context.CallResponse.Remove(CallResponse);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
