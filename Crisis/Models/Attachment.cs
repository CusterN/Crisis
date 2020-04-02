using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crisis.Models
{
    public class Attachment
    {
        [Key] public int Id { get; set; }
        [Required] [MaxLength(200)] public string FilePath { get; set; }

        [Required] public int AttachmentTypeId { get; set; }
        public virtual AttachmentType AttachmentType { get; set; }

        [Required] public int SupplierId { get; set;}
        public virtual Supplier Supplier { get; set; }
    }
}
