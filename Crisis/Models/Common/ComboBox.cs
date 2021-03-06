﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crisis.Models.Common
{
    public class ComboBox
    {
        [Key] public int Id { get; set; }
        [Required] [MaxLength(20)] public string Description { get; set; }
        [Required] [MaxLength(450)] public string Hint { get; set; }
    }
}
