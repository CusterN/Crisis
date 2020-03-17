using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

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
                    .OrderByDescending(c => c.CreateDate)
                    .ToListAsync();
            }
            else
            {
                Supplier = await _context.Supplier
                    .Include(s => s.Status)
                    .Include(c => c.Category)
                    .OrderByDescending(c => c.CreateDate)
                    .Where(m => m.Visible == true)
                    .ToListAsync();
            }
        }
    }
}
