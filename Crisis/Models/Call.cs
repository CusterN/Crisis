using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crisis.Models
{
    public class Call
    {
        [Key] public int Id { get; set; }
        [Required] [MaxLength(60)] public string Creator { get; set; }
        [Required] public DateTime CreateDate { get; set; }
        [Required] [MaxLength(60)] public string Contact { get; set; }
        [Required] [MaxLength(450)] public string Body { get; set; }

        [Required] public int CallResponseId { get; set; }
        public virtual CallResponse CallResponse { get; set; }

        [Required] public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
