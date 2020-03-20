using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crisis.Models
{
    public class SupplierDetail
    {
        public Supplier Supplier { get; set; }
        public Models.ExternalData.VwSrmAhmSupplier VwSrmAhmSupplier { get;set;}
    }
}
