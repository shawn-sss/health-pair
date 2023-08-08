using HealthPairDomain.InnerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    /// <summary> Interface for the Patient Repository </summary>
    public interface IPatientRepository
    {

        /// <summary> Fetches all patients related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All patients related to input string </returns>
        /// </summary>
        Task<List<Inner_Patient>> GetPatientsAsync(string search = null);

        /// <summary> Fetches one patient related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One patient related to input string </returns>
        /// </summary>
        Task<Inner_Patient> GetPatientByIdAsync(int id);

        /// <summary> Fetches one patient related to input string.
        /// <param name="email"> string - search email is looked for in email field of database </param>
        /// <returns> One patient related to input string </returns>
        /// </summary>
        Task<Inner_Patient> GetPatientByEmailAsync(string email);

        /// <summary> Checks if one patient exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        Task<bool> PatientExistAsync(int id);

        /// <summary> Add one patient to the database
        /// <param name="patient"> Inner_Patient Object - represents the fields of a Patient in the database </param>
        /// <returns> Returns inputted (and formatted) Patient </returns>
        /// </summary>
        Task<Inner_Patient> AddPatientAsync(Inner_Patient patient);

        /// <summary> Updates one existing patient in the database
        /// <param name="patient"> Inner_Patient Object - represents the fields of a Patient in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task UpdatePatientAsync(Inner_Patient patient);

        /// <summary> Removes one existing patient in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task RemovePatientAsync(int id);
    }
}
