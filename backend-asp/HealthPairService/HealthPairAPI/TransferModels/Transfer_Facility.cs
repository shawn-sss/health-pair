using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthPairAPI.TransferModels
{
    public class Transfer_Facility
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress1 { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public int FacilityZipcode { get; set; }
        public long FacilityPhoneNumber { get; set; }
    }
}
