using HealthPairDataAccess.DataModels;
using HealthPairDataAccess.Logic;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HealthPairDataAccess.Repositories
{
    /// <summary> Data Access methods for Patient </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly HealthPairContext _context;

        public PatientRepository(HealthPairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary> Fetches all patients related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All patients related to input string </returns>
        /// </summary>
        public async Task<List<Inner_Patient>> GetPatientsAsync(string search = null)
        {
            var patient = await _context.Patients
                .Include(p => p.Insurance)
                .ToListAsync();
            if(search == null)
            {
                return patient.Select(Mapper.MapPatient).ToList();
            }
            return (patient.FindAll(p =>
                p.PatientFirstName.ToLower().Contains(search.ToLower()) ||
                p.PatientLastName.ToLower().Contains(search.ToLower())
            )).Select(Mapper.MapPatient).ToList();
        }

        /// <summary> Fetches one patient related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One patient related to input string </returns>
        /// </summary>
        public async Task<Inner_Patient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Insurance)
                .FirstOrDefaultAsync(a => a.PatientId == id);
            return Mapper.MapPatient(patient);
        }

        /// <summary> Fetches one patient related to input string.
        /// <param name="email"> string - search email is looked for in email field of database </param>
        /// <returns> One patient related to input string </returns>
        /// </summary>
        public async Task<Inner_Patient> GetPatientByEmailAsync(string email)
        {
            var patient = await _context.Patients
                .Include(p => p.Insurance)
                .FirstOrDefaultAsync(a => a.PatientEmail.ToLower() == email.ToLower());
            return Mapper.MapPatient(patient);
        }

        /// <summary> Checks if one patient exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        public async Task<bool> PatientExistAsync(int id)
        {
            return await _context.Patients.AnyAsync(a => a.PatientId == id);
        }

        /// <summary> Add one patient to the database
        /// <param name="patient"> Inner_Patient Object - represents the fields of a Patient in the database </param>
        /// <returns> Returns inputted (and formatted) Patient </returns>
        /// </summary>
        public async Task<Inner_Patient> AddPatientAsync(Inner_Patient patient)
        {
            var newPatient = Mapper.UnMapPatient(patient);
            newPatient.PatientId = (await GetPatientsAsync()).Max(p => p.PatientId)+1;
            _context.Patients.Add(newPatient);
            await Save();

            return Mapper.MapPatient(newPatient);
        }

        /// <summary> Updates one existing patient in the database
        /// <param name="patient"> Inner_Patient Object - represents the fields of a Patient in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task UpdatePatientAsync(Inner_Patient patient)
        {
            Data_Patient currentEntity = await _context.Patients.FindAsync(patient.PatientId);
            Data_Patient newEntity = Mapper.UnMapPatient(patient);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        /// <summary> Removes one existing patient in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task RemovePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient is null)
            {
                return;
            }

            _context.Patients.Remove(patient);
            await Save();
        }

        /// <summary> An internal save method when changes are made to the database
        /// <returns> no return </returns>
        /// </summary>
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
