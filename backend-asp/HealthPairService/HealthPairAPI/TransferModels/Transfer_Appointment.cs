using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairAPI.TransferModels
{
    public class Transfer_Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDate{ get; set;}

        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public long PatientPhoneNumber { get; set; }
        public int ProviderId { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public long ProviderPhoneNumber { get; set; }
    }
}
