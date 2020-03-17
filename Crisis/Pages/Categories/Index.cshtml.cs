using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public IndexModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync(Boolean? All)
        {
            if(All == true)
            {
                Category = await _context.Category
                    .ToListAsync();
            }
            else
            {
                Category = await _context.Category
                    .Where(m=>m.Visible == true)
                    .ToListAsync();
            }
            
        }
    }
}
