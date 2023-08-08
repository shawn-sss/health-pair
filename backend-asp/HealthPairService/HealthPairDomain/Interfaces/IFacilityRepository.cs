using HealthPairDomain.InnerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    /// <summary> Interface for the Facility Repository </summary>
    public interface IFacilityRepository
    {

        /// <summary> Fetches all facilities related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All facilities related to input string </returns>
        /// </summary>
        Task<List<Inner_Facility>> GetFacilityAsync(string search = null);

        /// <summary> Fetches one facility related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One facility related to input string </returns>
        /// </summary>
        Task<Inner_Facility> GetFacilityByIdAsync(int id);

        /// <summary> Checks if one facility exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        Task<bool> FacilityExistAsync(int id);

        /// <summary> Add one facility to the database
        /// <param name="facility"> Inner_Facility Object - represents the fields of a Facility in the database </param>
        /// <returns> Returns inputted (and formatted) Facility </returns>
        /// </summary>
        Task<Inner_Facility> AddFacilityAsync(Inner_Facility facility);

        /// <summary> Updates one existing facility in the database
        /// <param name="facility"> Inner_Facility Object - represents the fields of a Facility in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task UpdateFacilityAsync(Inner_Facility facility);

        /// <summary> Removes one existing facility in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task RemoveFacilityAsync(int id);
    }
}
