using System.ComponentModel.DataAnnotations;

namespace HealthPairDataAccess.DataModels
{
    public class Data_InsuranceProvider
    {
        [Key]
        public int IPId { get; set; }

        public int InsuranceId { get; set; }
        public Data_Insurance Insurance { get; set; }

        public int ProviderId { get; set; }
        public Data_Provider Provider { get; set; }
    }
}