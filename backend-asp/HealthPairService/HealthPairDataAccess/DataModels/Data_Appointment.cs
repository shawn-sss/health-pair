using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HealthPairDataAccess.DataModels
{
    public class Data_Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Data_Patient Patient { get; set; }
        public Data_Provider Provider { get; set; }
    }
}
