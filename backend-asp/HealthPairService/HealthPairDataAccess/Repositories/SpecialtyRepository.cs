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
    /// <summary> Data Access methods for Specialty </summary>
    public class SpecialtyRepository : ISpecialtyRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public SpecialtyRepository(HealthPairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary> Fetches all specialties related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All specialties related to input string </returns>
        /// </summary>
        public async Task<List<Inner_Specialty>> GetSpecialtyAsync(string search = null)
        {
            var specialty = await _context.Specialties
                .ToListAsync();
            if(search == null)
            {
                return specialty.Select(Mapper.MapSpecialty).ToList();
            }
            return (specialty.FindAll(p =>
                p.Specialty.ToLower().Contains(search.ToLower())
            )).Select(Mapper.MapSpecialty).ToList();;
        }

       /// <summary> Fetches one specialty related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One specialty related to input string </returns>
        /// </summary>
        public async Task<Inner_Specialty> GetSpecialtyByIdAsync(int id)
        {
            var specialty = await _context.Specialties
                .FirstOrDefaultAsync(a => a.SpecialtyId == id);
            return Mapper.MapSpecialty(specialty);
        }

        /// <summary> Checks if one specialty exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        public async Task<bool> SpecialtyExistAsync(int id)
        {
            return await _context.Specialties.AnyAsync(a => a.SpecialtyId == id);
        }

        /// <summary> Add one specialty to the database
        /// <param name="specialty"> Inner_Specialty Object - represents the fields of a Specialty in the database </param>
        /// <returns> Returns inputted (and formatted) Specialty </returns>
        /// </summary>
        public async Task<Inner_Specialty> AddSpecialtyAsync(Inner_Specialty specialty)
        {
            var newSpecialty = Mapper.UnMapSpecialty(specialty);
            newSpecialty.SpecialtyId = (await GetSpecialtyAsync()).Max(p => p.SpecialtyId)+1;
            _context.Specialties.Add(newSpecialty);
            await Save();

            return Mapper.MapSpecialty(newSpecialty);
        }

        /// <summary> Updates one existing specialty in the database
        /// <param name="specialty"> Inner_Specialty Object - represents the fields of a Specialty in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task UpdateSpecialtyAsync(Inner_Specialty specialty)
        {
            Data_Specialty currentEntity = await _context.Specialties.FindAsync(specialty.SpecialtyId);
            Data_Specialty newEntity = Mapper.UnMapSpecialty(specialty);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        /// <summary> Removes one existing specialty in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task RemoveSpecialtyAsync(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty is null)
            {
                return;
            }

            _context.Specialties.Remove(specialty);
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
