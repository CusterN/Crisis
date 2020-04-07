using Crisis.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crisis.Models
{
    public class Status : ComboBox
    {
        public List<Supplier> Suppliers { get; set; }
    }
}
