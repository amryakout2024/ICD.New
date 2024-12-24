using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICD.Models
{
	public class Drug
	{
		[PrimaryKey]

        public int DrugId { get; set; }

        public string? DrugName { get; set; }

        public string? Indication { get; set; }

        public string? DiagnosisCode { get; set; }

        public bool IsCheckboxChecked { get; set; }=false;

		public bool IsButtonVisible { get; set; }=true;
	}
}
