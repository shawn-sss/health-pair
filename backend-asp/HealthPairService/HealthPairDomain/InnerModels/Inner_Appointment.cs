using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Appointment
    {
        private int _id;
        private DateTime _appointmentDate;

        public int AppointmentId
        {
            get
            {
                return _id;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("There can not be a negative id!");
                }
                _id = value;
            }
        }

        public DateTime AppointmentDate
        {
            get
            {
                return _appointmentDate;
            }
            set
            {
                if (AppointmentDate >= AppointmentDate.AddMonths(6))
                {
                    throw new ArgumentException("You cannot make an appointment 6 months in advance");
                }
                _appointmentDate = value;
            }
        }

        public Inner_Patient Patient { get; set; }
        public Inner_Provider Provider { get; set; }
    }
}
