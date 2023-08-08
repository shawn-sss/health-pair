using HealthPairDomain.InnerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    /// <summary> Interface for the Provider Repository </summary>
    public interface IProviderRepository
    {

        /// <summary> Fetches all providers related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All providers related to input string </returns>
        /// </summary>
        Task<List<Inner_Provider>> GetProvidersAsync(string search = null);

        /// <summary> Fetches one provider related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One provider related to input string </returns>
        /// </summary>
        Task<Inner_Provider> GetProviderByIdAsync(int id);

        /// <summary> Checks if one provider exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        Task<bool> ProviderExistAsync(int id);

        /// <summary> Add one provider to the database
        /// <param name="provider"> Inner_Provider Object - represents the fields of a Provider in the database </param>
        /// <returns> Returns inputted (and formatted) Provider </returns>
        /// </summary>
        Task<Inner_Provider> AddProviderAsync(Inner_Provider provider);

        /// <summary> Updates one existing provider in the database
        /// <param name="provider"> Inner_Provider Object - represents the fields of a Provider in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task UpdateProviderAsync(Inner_Provider provider);

        /// <summary> Removes one existing provider in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task RemoveProviderAsync(int id);
    }
}
