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
        public const string FolderPath = "\\\\ahmtroy03\\sppcsharedfiles\\6290 Procurement Operations\\_Public\\Data\\COVID19\\AppAttachments\\";
        public string FielName { get; set; }

        [Required] public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
