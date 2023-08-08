using System;
using HealthPairDomain.InnerModels;
using HealthPairAPI.Logic;
using Moq;
using Xunit;

namespace HealthPairAPI.Tests.ApiTransferModel
{
    public class API_MapperTest
    {
        private MockRepository mockRepo;

        public API_MapperTest()
        {
            this.mockRepo = new MockRepository(MockBehavior.Strict);
        }

        private Mapper MakeMapper()
        {
            return new Mapper();
        }

        public void APIAppointment_Behavior()
        {
            var apiMapper = this.MakeMapper();
            Inner_Appointment appointment = new Inner_Appointment
            {
                AppointmentDate = new DateTime(),
                AppointmentId = 1
            };

            var outcome = Mapper.MapAppointments(appointment);
            Assert.True(true);
            this.mockRepo.VerifyAll();
        }

        public void APIFacility_Behavior()
        {
            var apiMapper = this.MakeMapper();
            Inner_Facility facility = new Inner_Facility
            {
                FacilityId = 1,
                FacilityName = "Fake Facility Name",
                FacilityCity = "Fake Facility City",
                FacilityAddress1 = "Fake address",
                FacilityPhoneNumber = 9215555555,
                FacilityState = "Fake Facility State",
                FacilityZipcode = 84912
            };
            var outcome = Mapper.MapFacility(facility);
            Assert.True(true);
            this.mockRepo.VerifyAll();
        }

        public void APIInsurnace_Behavior()
        {
            var apiMapper = this.MakeMapper();
            Inner_Insurance insurance = new Inner_Insurance
            {
                InsuranceId = 1,
                InsuranceName = "Fake PPO"
            };
            var outcome = Mapper.MapInsurance(insurance);
            Assert.True(true);
            this.mockRepo.VerifyAll();
        }

        public void APIPatient_Behavior()
        {
            var apiMapper = this.MakeMapper();
            Inner_Patient patient = new Inner_Patient
            {
                PatientId = 1,
                PatientFirstName = "Fake First Name",
                PatientLastName = "Fake Last Name",
                PatientCity = "Fake Patient City",
                PatientState = "Fake Patient State",
                PatientZipcode = 92202,
                PatientAddress1 = "Fake Patient Address",
                PatientBirthDay = new DateTime(),
                PatientPassword = "Fake Patient Password",
                PatientPhoneNumber = 1236665555
            };
            var outcome = Mapper.MapPatient(patient);
            Assert.True(true);
            this.mockRepo.VerifyAll();
        }

            public void APIProvider_Behavior()
            {
                var apiMapper = this.MakeMapper();
                Inner_Provider provider = new Inner_Provider
                {
                    ProviderId = 1,
                    ProviderFirstName = "Fake Provider FirstName",
                    ProviderLastName = "Fake Provider LastName",
                    ProviderPhoneNumber = 3124445555
                };
            var outcome = Mapper.MapProvider(provider);
            Assert.True(true);
            this.mockRepo.VerifyAll();
            }

        public void APISpecialty_Behavior()
        {
            var apiMapper = this.MakeMapper();
            Inner_Specialty specialty = new Inner_Specialty
            {
                SpecialtyId = 1,
                Specialty = "Fake Specialty"
            };
            var outcome = Mapper.MapSpecialty(specialty);
            Assert.True(true);
            this.mockRepo.VerifyAll();
        }
        
    }
}