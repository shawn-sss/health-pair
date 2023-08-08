using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HealthPairDataAccess.DataModels
{
    public class Data_Specialty
    {
        [Key]
        public int SpecialtyId { get; set; }
        public string Specialty { get; set; }

        public ICollection<Data_Provider> Providers { get; set; } = new List<Data_Provider>();
    }
}
