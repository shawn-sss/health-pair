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
    /// <summary> Data Access methods for Provider </summary>
    public class ProviderRepository : IProviderRepository
    {
        private readonly HealthPairContext _context;

        public ProviderRepository(HealthPairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary> Fetches all providers related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All providers related to input string </returns>
        /// </summary>
        public async Task<List<Inner_Provider>> GetProvidersAsync(string search = null)
        {
            var provider = await _context.Providers
                .Include(p => p.Facility)
                .Include(p => p.Specialty)
                .ToListAsync();
            if(search == null)
            {
                return provider.Select(Mapper.MapProvider).ToList();
            }
            return (provider.FindAll(p =>
                p.ProviderFirstName.ToLower().Contains(search.ToLower()) ||
                p.ProviderLastName.ToLower().Contains(search.ToLower()) ||
                p.Facility.FacilityAddress1.ToLower().Contains(search.ToLower()) ||
                p.Facility.FacilityCity.ToLower().Contains(search.ToLower()) ||
                p.Facility.FacilityName.ToLower().Contains(search.ToLower()) ||
                p.Facility.FacilityState.ToLower().Contains(search.ToLower()) ||
                p.Specialty.Specialty.ToLower().Contains(search.ToLower())
            )).Select(Mapper.MapProvider).ToList();
        }

        /// <summary> Fetches one provider related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One provider related to input string </returns>
        /// </summary>
        public async Task<Inner_Provider> GetProviderByIdAsync(int id)
        {
            var provider = await _context.Providers
                .Include(p => p.Facility)
                .Include(p => p.Specialty)
                .FirstOrDefaultAsync(a => a.ProviderId == id);
            return Mapper.MapProvider(provider);
        }

        /// <summary> Checks if one provider exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        public async Task<bool> ProviderExistAsync(int id)
        {
            return await _context.Providers.AnyAsync(a => a.ProviderId == id);
        }

        /// <summary> Add one provider to the database
        /// <param name="provider"> Inner_Provider Object - represents the fields of a Provider in the database </param>
        /// <returns> Returns inputted (and formatted) Provider </returns>
        /// </summary>
        public async Task<Inner_Provider> AddProviderAsync(Inner_Provider provider)
        {
            var newProvider = Mapper.UnMapProvider(provider);
            newProvider.ProviderId = (await GetProvidersAsync()).Max(p => p.ProviderId)+1;
            _context.Providers.Add(newProvider);
            await Save();

            return Mapper.MapProvider(newProvider);
        }

        /// <summary> Updates one existing provider in the database
        /// <param name="provider"> Inner_Provider Object - represents the fields of a Provider in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task UpdateProviderAsync(Inner_Provider provider)
        {
            Data_Provider currentEntity = await _context.Providers.FindAsync(provider.ProviderId);
            Data_Provider newEntity = Mapper.UnMapProvider(provider);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        /// <summary> Removes one existing provider in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task RemoveProviderAsync(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider is null)
            {
                return;
            }

            _context.Providers.Remove(provider);
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
