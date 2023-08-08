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
    /// <summary> Data Access methods for Facility </summary>
    public class FacilityRepository : IFacilityRepository
    {
        private readonly HealthPairContext _context;

        public FacilityRepository(HealthPairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary> Fetches all facilities related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All facilities related to input string </returns>
        /// </summary>
        public async Task<List<Inner_Facility>> GetFacilityAsync(string search = null)
        {
            var facility = await _context.Facilities
                .ToListAsync();
            if(search == null)
            {
                return facility.Select(Mapper.MapFacility).ToList();
            }
            return (facility.FindAll(p =>
                p.FacilityName.ToLower().Contains(search.ToLower()) ||
                p.FacilityAddress1.ToLower().Contains(search.ToLower()) ||
                p.FacilityCity.ToLower().Contains(search.ToLower()) ||
                p.FacilityState.ToLower().Contains(search.ToLower())
            )).Select(Mapper.MapFacility).ToList();
        }

        /// <summary> Fetches one facility related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One facility related to input string </returns>
        /// </summary>
        public async Task<Inner_Facility> GetFacilityByIdAsync(int id)
        {
            var facility = await _context.Facilities
                .FirstOrDefaultAsync(a => a.FacilityId == id);
            return Mapper.MapFacility(facility);
        }

        /// <summary> Checks if one facility exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        public async Task<bool> FacilityExistAsync(int id)
        {
            return await _context.Facilities.AnyAsync(a => a.FacilityId == id);
        }

        /// <summary> Add one facility to the database
        /// <param name="facility"> Inner_Facility Object - represents the fields of a Facility in the database </param>
        /// <returns> Returns inputted (and formatted) Facility </returns>
        /// </summary>
        public async Task<Inner_Facility> AddFacilityAsync(Inner_Facility facility)
        {
            var newFacility = Mapper.UnMapFacility(facility);
            newFacility.FacilityId = (await GetFacilityAsync()).Max(p => p.FacilityId)+1;
            _context.Facilities.Add(newFacility);
            await Save();

            return Mapper.MapFacility(newFacility);
        }

        /// <summary> Updates one existing facility in the database
        /// <param name="facility"> Inner_Facility Object - represents the fields of a Facility in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task UpdateFacilityAsync(Inner_Facility facility)
        {
            Data_Facility currentEntity = await _context.Facilities.FindAsync(facility.FacilityId);
            Data_Facility newEntity = Mapper.UnMapFacility(facility);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        /// <summary> Removes one existing facility in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task RemoveFacilityAsync(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility is null)
            {
                return;
            }

            _context.Facilities.Remove(facility);
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
