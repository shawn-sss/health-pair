using HealthPairAPI.TransferModels;
using System;
using HealthPairDataAccess.Repositories;
using HealthPairDomain.Interfaces;
using HealthPairDomain.InnerModels;
using HealthPairAPI.Helpers;

namespace HealthPairAPI.Logic
{
    public class CheckerClass
    {
        //this is creating an instance
        private IPatientRepository _patientRepo;
        private ISpecialtyRepository _specialtyRepo;
        private IProviderRepository _providerRepo;
        private IFacilityRepository _facilityRepo;
        private IAppointmentRepository _appointmentRepo;
        private IInsuranceRepository _insuranceRepo;

        public CheckerClass(IPatientRepository patientRepository)
        {
            _patientRepo = patientRepository;
        }

        public CheckerClass(IPatientRepository patientRepository, IInsuranceRepository insuranceRepository)
        {
            _patientRepo = patientRepository;
            _insuranceRepo = insuranceRepository;
        }

        public CheckerClass(IFacilityRepository facilityRepository)
        {
            _facilityRepo = facilityRepository;
        }

        public CheckerClass(IFacilityRepository facilityRepository, ISpecialtyRepository specialtyRepository)
        {
            _facilityRepo = facilityRepository;
            _specialtyRepo = specialtyRepository;
        }


        // this is injecting the Interface in order for us to access the database
        public CheckerClass(IPatientRepository patientRepo, IProviderRepository provRepo, IAppointmentRepository appointmentRepo)
        {
            //Making the local repository instance that we created equal to the overall repository that we are injecting so that its consistent.
            _patientRepo = patientRepo;
            _providerRepo = provRepo;
            _appointmentRepo = appointmentRepo;

        }


        //This is checking to see if the id's exist
        public void CheckAppointment(Transfer_Appointment appointment)
        {
            //We add the ".Result" because we want the final result of the async methods, we odont want the tasks.
            //checking to see if the Patient exists in the database
            if (!(_patientRepo.PatientExistAsync(appointment.PatientId).Result))
            {
                throw new Exception("The Patient does not exist");
            }
            //Checking to see if the user exists in the database
            if (!(_providerRepo.ProviderExistAsync(appointment.ProviderId).Result))
            {
                throw new HealthPairAppException("The Provider does not exist");
            }

        }

        public void CheckFacility(Transfer_Facility facility)
        {
            if(!(_facilityRepo.FacilityExistAsync(facility.FacilityId).Result))
            {
                throw new HealthPairAppException("This Facility does not exist, please choose the correct facility");
            }
        }

        public void CheckInsurance(Transfer_Insurance insurance)
        {

        }

        public void CheckPatient(Transfer_Patient patient)
        {
            if(!(_insuranceRepo.InsuranceExistAsync(patient.InsuranceId).Result))
            {
                throw new HealthPairAppException("The insurance you chose does not exist, please choose the correct insurance");
            }
        }

        public void CheckProvider(Transfer_Provider provider)
        {

            if (!(_facilityRepo.FacilityExistAsync(provider.FacilityId).Result))
            {
                throw new HealthPairAppException("This Facility Does not Exist");
            }

            if(!(_specialtyRepo.SpecialtyExistAsync(provider.SpecialtyId).Result))
            {
                throw new HealthPairAppException("The Specialty does not exist, please choose the correct specialty");
            }

        }

    }
}