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
		[Key]

		public int DrugId { get; set; }

		public string? DrugName { get; set; }

		public string? DiagnosisCode { get; set; }

		public string? DiagnosisName1 { get; set; }

		public string? DiagnosisName2 { get; set; }

		public string? DiagnosisName3 { get; set; }

		public bool IsCheckboxChecked { get; set; }=false;

		public bool IsButtonVisible { get; set; }=true;
	}
}
