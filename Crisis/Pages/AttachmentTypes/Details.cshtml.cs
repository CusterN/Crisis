﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.AttachmentTypes
{
    public class DetailsModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public DetailsModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

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
    }
}