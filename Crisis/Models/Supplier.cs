﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Crisis.Models
{
    public class Supplier
    {
        [Key] public int Id { get; set; }
        [Required][MinLength(6)][MaxLength(6)] public string SupplierNo { get; set; }
        [Required] public DateTime CreateDate { get; set;}
        [Required] [MaxLength(60)] public string Creator { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")] public DateTime? ReopenDate { get; set; }
        
        public List<Comment> Comments { get; set; }

        public List<Call> Calls { get; set; }

        public List<Attachment> Attachments { get; set; }

        [Required] public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Required] public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required] public int EscalationId { get; set; }
        public virtual Escalation Escalation { get; set; }

    }
}
