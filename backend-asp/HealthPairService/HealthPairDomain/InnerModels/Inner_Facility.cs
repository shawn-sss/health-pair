using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Facility
    {
        public int FacilityId { get; set; }

        [Required(ErrorMessage = "You Must Provide a Facility")]
        [StringLength(80, ErrorMessage = "Too Many Characters, You Must Enter the Correct Format")]
        public string FacilityName { get; set; }

        [Required(ErrorMessage = "You Must Provide an Address")]
        public string FacilityAddress1 { get; set; }

        [Required(ErrorMessage = "You Must Provide a City")]
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }

        [Required(ErrorMessage = "You Must Enter the A ZipCode")]
        [DataType(DataType.PostalCode)]
        public int FacilityZipcode { get; set; }

        [Required(ErrorMessage = "You Must Enter a Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public long FacilityPhoneNumber { get; set; }

        public List<Inner_Provider> Providers { get; set; } = new List<Inner_Provider>();
    }
}
