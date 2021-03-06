﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Crisis.Models;

namespace Crisis.Data
{
    public class CrisisContext : DbContext
    {
        public CrisisContext (DbContextOptions<CrisisContext> options)
            : base(options)
        {
        }

        public DbSet<Crisis.Models.Supplier> Supplier { get; set; }

        public DbSet<Crisis.Models.Comment> Comment { get; set; }

        public DbSet<Crisis.Models.Status> Status { get; set; }

        public DbSet<Crisis.Models.Category> Category { get; set; }

        public DbSet<Crisis.Models.Call> Call { get; set; }

        public DbSet<Crisis.Models.Attachment> Attachment { get; set; }

        public DbSet<Crisis.Models.CallResponse> CallResponse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>()
                .HasAlternateKey(s => s.SupplierNo);
        }

        public DbSet<Crisis.Models.AttachmentType> AttachmentType { get; set; }

        public DbSet<Crisis.Models.Escalation> Escalation { get; set; }

        
    }
}
