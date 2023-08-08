using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Provider
    {
        public int ProviderId { get; set; }


        [Required(ErrorMessage = "You Must Provide Your First Name")]
        [StringLength(25, ErrorMessage = "Too Many Characters, Please Enter Correct Format")]
        public string ProviderFirstName { get; set; }

        [Required(ErrorMessage = "You Must Provide a Last Name")]
        [StringLength(35, ErrorMessage = "Too Many Characters, Please Enter the correct Format")]
        public string ProviderLastName { get; set; }

        [Required(ErrorMessage = "You Must Enter a Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public long ProviderPhoneNumber { get; set; }

        [Required(ErrorMessage = "You Must Choose a Facility")]
        public Inner_Facility Facility { get; set; }
        [Required(ErrorMessage = "You Must Choose a Specialty")]
        public Inner_Specialty Specialty { get; set; }
        public List<Inner_Appointment> Appointments { get; set; } = new List<Inner_Appointment>();
        public List<Inner_Insurance> Insurances { get; set; } = new List<Inner_Insurance>();
    }
}
