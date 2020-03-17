using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.Statuses
{
    public class IndexModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public IndexModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public IList<Status> Status { get;set; }

        public async Task OnGetAsync(Boolean? All)
        {
            if(All == true)
            {
                Status = await _context.Status.ToListAsync();
            } 
            else
            {
                Status = await _context.Status
                    .Where(m => m.Visible == true)
                    .ToListAsync();
            }
            
        }
    }
}
