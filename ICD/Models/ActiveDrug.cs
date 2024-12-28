using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICD.Models
{
    public class ActiveDrug
    {
        [PrimaryKey,AutoIncrement]

        public int DrugId { get; set; }

        public string? DrugName { get; set; }

    }
}
