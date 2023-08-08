using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Insurance
    {
        public int InsuranceId { get; set; }
        [Required(ErrorMessage = "You Must Choose an Insurance Provider")]
        public string InsuranceName { get; set; }

        public List<Inner_Patient> Patients { get; set; } = new List<Inner_Patient>();
        public List<Inner_Provider> Providers { get; set; } = new List<Inner_Provider>();
    }
}
