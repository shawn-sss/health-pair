using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HealthPairDataAccess.DataModels
{
    public class Data_Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public int FacilityId { get; set; }
        public int SpecialtyId { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public long ProviderPhoneNumber { get; set; }

        public Data_Facility Facility { get; set; }
        public Data_Specialty Specialty { get; set; }
        public ICollection<Data_Appointment> Appointments { get; set; } = new List<Data_Appointment>();
        public ICollection<Data_InsuranceProvider> InsuranceProviders { get; set; } = new List<Data_InsuranceProvider>();
    }
}
