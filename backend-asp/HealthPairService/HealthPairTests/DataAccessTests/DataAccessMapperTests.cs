using System;
using System.Collections.Generic;
using Xunit;
using HealthPairDataAccess.DataModels;
using HealthPairDomain.InnerModels;
using HealthPairDataAccess.Logic;

namespace HealthPairTests.DataAccessTests
{
    public class DataAccessMapperTests
    {

 // ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// !  ------ APPOINTMENTS --------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        [Fact]
        public void MapAppointmentTest()
        {
            //this is the sample input
            //It looks pretty complicated, but only because both the data object and the inner object have sub-objects inside them
            //Notice when filling the dummy objects I only fill the info that the mapper will need (the id and any sub-objects)
            Data_Appointment sampleAppointmentD = new Data_Appointment
            {
                AppointmentId = 100,
                AppointmentDate = new DateTime(2000,1,1),
                Patient = new Data_Patient
                {
                    PatientId = 100,
                    Insurance = new Data_Insurance
                    {
                        InsuranceId = 100
                    }
                },
                Provider = new Data_Provider
                {
                    ProviderId = 100,
                    Facility = new Data_Facility
                    {
                        FacilityId = 100
                    },
                    Specialty = new Data_Specialty
                    {
                        SpecialtyId = 100
                    }
                }
            };

            //this is the predicted output. notice I only filled in the field that matter when making the new object
            Inner_Appointment sampleAppointmentL = new Inner_Appointment
            {
                AppointmentId = 100,
                AppointmentDate = new DateTime(2000,1,1),
                Patient = new Inner_Patient
                {
                    PatientId = 100,
                    Insurance = new Inner_Insurance
                    {
                        InsuranceId = 100
                    }
                },
                Provider = new Inner_Provider
                {
                    ProviderId = 100,
                    Facility = new Inner_Facility
                    {
                        FacilityId = 100
                    },
                    Specialty = new Inner_Specialty
                    {
                        SpecialtyId = 100
                    }
                }
            };

            // this is the actual coverage part. It runs our input object through the real mapper
            Inner_Appointment resultAppointmentL = Mapper.MapAppointment(sampleAppointmentD);

            // this uses a 2nd method to make sure that the real mapper output the same thing as our predicted result
            Assert.True(compareAppointmentL(resultAppointmentL,sampleAppointmentL));
        }

        [Fact]
        public void UnMapAppointmentTest()
        {
            // same method, but reverse.

            Inner_Appointment sampleAppointmentL = new Inner_Appointment
            {
                AppointmentId = 200,
                AppointmentDate = new DateTime(2000,1,1),
                Patient = new Inner_Patient
                {
                    PatientId = 200
                },
                Provider = new Inner_Provider
                {
                    ProviderId = 200
                }
            };

            Data_Appointment sampleAppointmentD = new Data_Appointment
            {
                AppointmentId = 200,
                AppointmentDate = new DateTime(2000,1,1),
                PatientId = 200,
                ProviderId = 200
            };

            Data_Appointment resultAppointmentD = Mapper.UnMapAppointment(sampleAppointmentL);

            Assert.True(compareAppointmentD(resultAppointmentD,sampleAppointmentD));
        }

        private bool compareAppointmentL(Inner_Appointment x, Inner_Appointment y)
        {
            // checks to make sure all the field of the two input object match
            // returns false (fails test) if they don't match
            if(
                x.AppointmentDate != y.AppointmentDate ||
                x.AppointmentId != y.AppointmentId ||
                x.Patient.PatientId != y.Patient.PatientId ||
                x.Provider.ProviderId != y.Provider.ProviderId
            )
            {
                return false;
            }
            return true;
        }

        private bool compareAppointmentD(Data_Appointment x, Data_Appointment y)
        {
            // same thing with the data type of the object
            // you DO have to be careful about which values are the important ones.
            // the inner object can have sub-object inside of them
            // we dont care about the sub-objects inside of the data objects (because those are other tables)
            if(
                x.AppointmentDate != y.AppointmentDate ||
                x.AppointmentId != y.AppointmentId ||
                x.PatientId != y.PatientId ||
                x.ProviderId != y.ProviderId
            )
            {
                return false;
            }
            return true;
        }
    }
}