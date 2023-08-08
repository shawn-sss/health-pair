using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairAPI.TransferModels
{
    public class Transfer_Patient
    {
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientPassword { get; set; }
        public string PatientAddress1 { get; set; }
        public string PatientCity { get; set; }
        public string PatientState { get; set; }
        public int PatientZipcode { get; set; }
        public DateTime PatientBirthDay { get; set; }
        public long PatientPhoneNumber { get; set; }
        public string PatientEmail { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }


        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
    }
}
