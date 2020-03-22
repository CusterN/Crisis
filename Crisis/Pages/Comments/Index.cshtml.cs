using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public IndexModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; }

        public async Task OnGetAsync(bool Visible = true)
        {

            Comment = await _context.Comment
                .Include(c => c.Supplier)
                .Where(m => m.Visible == Visible && m.Supplier.Visible == Visible)
                .OrderByDescending(c => c.CreateDate)
                .ToListAsync();

        }
    }
}
