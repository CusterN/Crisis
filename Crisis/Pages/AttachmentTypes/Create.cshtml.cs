﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crisis.Data;
using Crisis.Models;

namespace Crisis.Pages.AttachmentTypes
{
    public class CreateModel : PageModel
    {
        private readonly Crisis.Data.CrisisContext _context;

        public CreateModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AttachmentType AttachmentType { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AttachmentType.Add(AttachmentType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}