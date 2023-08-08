using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Specialty
    {
        public int SpecialtyId { get; set; }

        [Required(ErrorMessage = "You Must Choose a Specialty")]
        public string Specialty { get; set; }

        public List<Inner_Provider> Providers { get; set; } = new List<Inner_Provider>();
    }
}
