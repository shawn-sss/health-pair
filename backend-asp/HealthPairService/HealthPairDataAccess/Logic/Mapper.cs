using HealthPairDataAccess.DataModels;
using HealthPairDomain.InnerModels;


namespace HealthPairDataAccess.Logic
{
    public static class Mapper
    {

// ! ***********************************
// ! ********* Appointments ************
// ! ***********************************
        public static Inner_Appointment MapAppointment(Data_Appointment appointment)
        {
            return new Inner_Appointment
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                Patient = MapPatient(appointment.Patient),
                Provider = MapProvider(appointment.Provider)
            };
        }

        public static Data_Appointment UnMapAppointment(Inner_Appointment appointment)
        {
            return new Data_Appointment
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                PatientId = appointment.Patient.PatientId,
                ProviderId = appointment.Provider.ProviderId
            };
        }

// ! ***********************************
// ! *********** Facility **************
// ! ***********************************
        public static Inner_Facility MapFacility(Data_Facility facility)
        {
            return new Inner_Facility
            {
                FacilityId = facility.FacilityId,
                FacilityAddress1 = facility.FacilityAddress1,
                FacilityCity = facility.FacilityCity,
                FacilityName = facility.FacilityName,
                FacilityPhoneNumber = facility.FacilityPhoneNumber,
                FacilityState = facility.FacilityState,
                FacilityZipcode = facility.FacilityZipcode,
            };
        }

        public static Data_Facility UnMapFacility(Inner_Facility facility)
        {
            return new Data_Facility
            {
                FacilityId = facility.FacilityId,
                FacilityAddress1 = facility.FacilityAddress1,
                FacilityCity = facility.FacilityCity,
                FacilityName = facility.FacilityName,
                FacilityPhoneNumber = facility.FacilityPhoneNumber,
                FacilityState = facility.FacilityState,
                FacilityZipcode = facility.FacilityZipcode,
            };
        }

// ! ***********************************
// ! *********** Insurance **************
// ! ***********************************
        public static Inner_Insurance MapInsurance(Data_Insurance insurance)
        {
            return new Inner_Insurance
            {
                InsuranceId = insurance.InsuranceId,
                InsuranceName = insurance.InsuranceName,
            };
        }

        public static Data_Insurance UnMapInsurance(Inner_Insurance insurance)
        {
            return new Data_Insurance
            {
                InsuranceId = insurance.InsuranceId,
                InsuranceName = insurance.InsuranceName,
            };
        }

// ! ***********************************
// ! *********** Patient **************
// ! ***********************************
        public static Inner_Patient MapPatient(Data_Patient patient)
        {
            return new Inner_Patient
            {
                PatientId = patient.PatientId,
                PatientAddress1 = patient.PatientAddress1,
                PatientBirthDay = patient.PatientBirthDay,
                PatientCity = patient.PatientCity,
                PatientFirstName = patient.PatientFirstName,
                PatientZipcode = patient.PatientZipcode,
                PatientLastName = patient.PatientLastName,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                PatientPassword = patient.PatientPassword,
                PatientState = patient.PatientState,
                PatientEmail = patient.PatientEmail,
                IsAdmin = patient.IsAdmin,
                Insurance = MapInsurance(patient.Insurance)
            };
        }

        public static Data_Patient UnMapPatient(Inner_Patient patient)
        {
            return new Data_Patient
            {
                PatientId = patient.PatientId,
                PatientAddress1 = patient.PatientAddress1,
                PatientBirthDay = patient.PatientBirthDay,
                PatientCity = patient.PatientCity,
                PatientFirstName = patient.PatientFirstName,
                PatientPassword = patient.PatientPassword,
                PatientZipcode = patient.PatientZipcode,
                PatientLastName = patient.PatientLastName,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                PatientState = patient.PatientState,
                PatientEmail = patient.PatientEmail,
                InsuranceId = patient.Insurance.InsuranceId,
                IsAdmin = patient.IsAdmin
            };
        }

// ! ***********************************
// ! *********** Provider **************
// ! ***********************************
        public static Inner_Provider MapProvider(Data_Provider provider)
        {
            return new Inner_Provider
            {
                ProviderId = provider.ProviderId,
                ProviderFirstName = provider.ProviderFirstName,
                ProviderLastName = provider.ProviderLastName,
                ProviderPhoneNumber = provider.ProviderPhoneNumber,
                Facility = MapFacility(provider.Facility),
                Specialty = MapSpecialty(provider.Specialty)
            };
        }

        public static Data_Provider UnMapProvider(Inner_Provider provider)
        {
            return new Data_Provider
            {
                ProviderId = provider.ProviderId,
                ProviderFirstName = provider.ProviderFirstName,
                ProviderLastName = provider.ProviderLastName,
                ProviderPhoneNumber = provider.ProviderPhoneNumber,
                FacilityId = provider.Facility.FacilityId,
                SpecialtyId = provider.Specialty.SpecialtyId
            };
        }

// ! ***********************************
// ! ********** Specialty **************
// ! ***********************************
        public static Inner_Specialty MapSpecialty(Data_Specialty specialty)
        {
            return new Inner_Specialty
            {
                SpecialtyId = specialty.SpecialtyId,
                Specialty = specialty.Specialty
            };
        }

        public static Data_Specialty UnMapSpecialty(Inner_Specialty specialty)
        {
            return new Data_Specialty
            {
                SpecialtyId = specialty.SpecialtyId,
                Specialty = specialty.Specialty
            };
        }
    }
}
