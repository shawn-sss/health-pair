using HealthPairDomain.InnerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    /// <summary> Interface for the Insurance Repository </summary>
    public interface IInsuranceRepository
    {

        /// <summary> Fetches all insurance related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All insurance related to input string </returns>
        /// </summary>
        Task<List<Inner_Insurance>> GetInsuranceAsync(string search = null);

        /// <summary> Fetches one insurance related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One insurance related to input string </returns>
        /// </summary>
        Task<Inner_Insurance> GetInsuranceByIdAsync(int id);

        /// <summary> Checks if one insurance exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        Task<bool> InsuranceExistAsync(int id);

        /// <summary> Add one insurance to the database
        /// <param name="insurance"> Inner_Insurance Object - represents the fields of a Insurance in the database </param>
        /// <returns> Returns inputted (and formatted) Insurance </returns>
        /// </summary>
        Task<Inner_Insurance> AddInsuranceAsync(Inner_Insurance insurance);

        /// <summary> Updates one existing insurance in the database
        /// <param name="insurance"> Inner_Insurance Object - represents the fields of a Insurance in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task UpdateInsuranceAsync(Inner_Insurance insurance);

        /// <summary> Removes one existing insurance in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task RemoveInsuranceAsync(int id);
    }
}
