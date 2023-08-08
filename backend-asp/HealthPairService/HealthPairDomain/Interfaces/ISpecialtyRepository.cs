using HealthPairDomain.InnerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    /// <summary> Interface for the Specialty Repository </summary>
    public interface ISpecialtyRepository
    {

        /// <summary> Fetches all specialties related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All specialties related to input string </returns>
        /// </summary>
        Task<List<Inner_Specialty>> GetSpecialtyAsync(string search = null);

        /// <summary> Fetches one specialty related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One specialty related to input string </returns>
        /// </summary>
        Task<Inner_Specialty> GetSpecialtyByIdAsync(int id);

        /// <summary> Checks if one specialty exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        Task<bool> SpecialtyExistAsync(int id);

        /// <summary> Add one specialty to the database
        /// <param name="specialty"> Inner_Specialty Object - represents the fields of a Specialty in the database </param>
        /// <returns> Returns inputted (and formatted) Specialty </returns>
        /// </summary>
        Task<Inner_Specialty> AddSpecialtyAsync(Inner_Specialty specialty);

        /// <summary> Updates one existing specialty in the database
        /// <param name="specialty"> Inner_Specialty Object - represents the fields of a Specialty in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task UpdateSpecialtyAsync(Inner_Specialty specialty);

        /// <summary> Removes one existing specialty in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task RemoveSpecialtyAsync(int id);
    }
}
