using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICD.Models
{
    public class TradeDrug
    {
        [Key]

        public int TradeDrugId { get; set; }

        public string? TradeDrugName { get; set; }

        public string? DrugName { get; set; }

        public string? DrugForm { get; set; }

        public string? Gtin { get; set; }
    }
}
