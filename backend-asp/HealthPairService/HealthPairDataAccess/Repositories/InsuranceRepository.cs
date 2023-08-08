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
    /// <summary> Data Access methods for Insurance </summary>
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly HealthPairContext _context;

        public InsuranceRepository(HealthPairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary> Fetches all insurance related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All insurance related to input string </returns>
        /// </summary>
        public async Task<List<Inner_Insurance>> GetInsuranceAsync(string search = null)
        {
            var insurance = await _context.Insurances
                .ToListAsync();
            if(search == null)
            {
                return insurance.Select(Mapper.MapInsurance).ToList();
            }
            return (insurance.FindAll(p =>
                p.InsuranceName.ToLower().Contains(search.ToLower())
            )).Select(Mapper.MapInsurance).ToList();
        }

        /// <summary> Fetches one insurance related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One insurance related to input string </returns>
        /// </summary>
        public async Task<Inner_Insurance> GetInsuranceByIdAsync(int id)
        {
            var insurance = await _context.Insurances
                .FirstOrDefaultAsync(a => a.InsuranceId == id);
            return Mapper.MapInsurance(insurance);
        }

        /// <summary> Checks if one insurance exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        public async Task<bool> InsuranceExistAsync(int id)
        {
            return await _context.Insurances.AnyAsync(a => a.InsuranceId == id);
        }

        /// <summary> Add one insurance to the database
        /// <param name="insurance"> Inner_Insurance Object - represents the fields of a Insurance in the database </param>
        /// <returns> Returns inputted (and formatted) Insurance </returns>
        /// </summary>
        public async Task<Inner_Insurance> AddInsuranceAsync(Inner_Insurance insurance)
        {
            var newInsurance = Mapper.UnMapInsurance(insurance);
            newInsurance.InsuranceId = (await GetInsuranceAsync()).Max(p => p.InsuranceId)+1;
            _context.Insurances.Add(newInsurance);
            await _context.SaveChangesAsync();

            return Mapper.MapInsurance(newInsurance);
        }

        /// <summary> Updates one existing insurance in the database
        /// <param name="insurance"> Inner_Insurance Object - represents the fields of a Insurance in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task UpdateInsuranceAsync(Inner_Insurance insurance)
        {
            Data_Insurance currentEntity = await _context.Insurances.FindAsync(insurance.InsuranceId);
            Data_Insurance newEntity = Mapper.UnMapInsurance(insurance);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        /// <summary> Removes one existing insurance in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task RemoveInsuranceAsync(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance is null)
            {
                return;
            }

            _context.Insurances.Remove(insurance);
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
