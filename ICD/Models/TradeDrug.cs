using SQLite;
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
        [PrimaryKey]

        public int TradeDrugId { get; set; }

        public string? TradeDrugName { get; set; }

        public string? DrugName { get; set; }
    }
}
