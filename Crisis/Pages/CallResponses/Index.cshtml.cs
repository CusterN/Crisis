﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public IndexModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public IList<CallResponse> CallResponse { get;set; }

        public async Task OnGetAsync()
        {
            CallResponse = await _context.CallResponse.ToListAsync();
        }
    }
}