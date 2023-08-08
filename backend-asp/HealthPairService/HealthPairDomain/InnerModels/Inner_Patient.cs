using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Patient
    {
        public int PatientId { get; set; }

        [Required(ErrorMessage = "You Must Enter your First Name")]
        public string PatientFirstName { get; set; }

        [Required(ErrorMessage = "You Must Enter Your Last Name")]
        public string PatientLastName { get; set; }

        [Required(ErrorMessage = "You Must Provide an Address")]
        public string PatientAddress1 { get; set; }

        [Required(ErrorMessage = "You Must Provide a Password")]
        [DataType(DataType.Password)]
        public string PatientPassword { get; set; }

        [Required(ErrorMessage = "You Must Provide a City")]
        public string PatientCity { get; set; }

        [Required(ErrorMessage = "You Must Provide a State")]
        [StringLength(50, ErrorMessage = "Too Many Characters, Please Enter the Correct Format")]
        public string PatientState { get; set; }

        [Required(ErrorMessage = "You Must Enter Your ZipCode")]
        [DataType(DataType.PostalCode)]
        public int PatientZipcode { get; set; }

        [Required(ErrorMessage ="You Must Enter Your Birthday")]
        [DataType(DataType.Date)]
        public DateTime PatientBirthDay { get; set; }
        [Required(ErrorMessage = "You Must Enter Your Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public long PatientPhoneNumber { get; set; }
        public string PatientEmail { get; set; }
        public bool IsAdmin { get; set; }

        public Inner_Insurance Insurance { get; set; }
        public List<Inner_Appointment> Appointments { get; set; } = new List<Inner_Appointment>();
    }
}
