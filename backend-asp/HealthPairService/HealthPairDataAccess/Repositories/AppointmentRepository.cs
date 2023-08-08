using HealthPairDataAccess.DataModels;
using HealthPairDataAccess.Logic;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace HealthPairDataAccess.Repositories
{
    /// <summary> Data Access methods for Appointment </summary>
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HealthPairContext _context;
        private readonly ILogger<AppointmentRepository> _logger;

        public AppointmentRepository(HealthPairContext context, ILogger<AppointmentRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        /// <summary> Fetches all appointments related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All appointments related to input string </returns>
        /// </summary>
        public async Task<List<Inner_Appointment>> GetAppointmentAsync(string search = null)
        {
            var appointment = await _context.Appointments
                .Include(p => p.Patient)
                .ThenInclude(p => p.Insurance)
                .Include(p => p.Provider)
                .Include(p => p.Provider.Specialty)
                .Include( p => p.Provider.Facility)
                .ToListAsync();
            if(search == null)
            {
                return appointment.Select(Mapper.MapAppointment).ToList();
            }
            return (appointment.FindAll(p =>
                p.Patient.PatientFirstName.ToLower().Contains(search.ToLower()) ||
                p.Patient.PatientLastName.ToLower().Contains(search.ToLower()) ||
                p.Provider.ProviderFirstName.ToLower().Contains(search.ToLower()) ||
                p.Provider.ProviderLastName.ToLower().Contains(search.ToLower())
            )).Select(Mapper.MapAppointment).ToList();
        }

        /// <summary> Fetches one appointment related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One appointment related to input string </returns>
        /// </summary>
        public async Task<Inner_Appointment> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _context.Appointments
                .Include(p => p.Patient)
                .ThenInclude(p => p.Insurance)
                .Include(p => p.Provider)
                .Include(p => p.Provider.Specialty)
                .Include( p => p.Provider.Facility)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
            return Mapper.MapAppointment(appointment);
        }

        /// <summary> Checks if one appointment exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        public async Task<bool> AppointmentExistAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.AppointmentId == id);
        }

        /// <summary> Add one appointment to the database
        /// <param name="appointment"> Inner_Appointment Object - represents the fields of a Appointment in the database </param>
        /// <returns> Returns inputted (and formatted) Appointment </returns>
        /// </summary>
        public async Task<Inner_Appointment> AddAppointmentAsync(Inner_Appointment appointment)
        {
            try
            {
                var newAppointment = Mapper.UnMapAppointment(appointment);
                newAppointment.AppointmentId = (await GetAppointmentAsync()).Max(p => p.AppointmentId)+1;
                _context.Appointments.Add(newAppointment);
                await Save();
                return Mapper.MapAppointment(newAppointment);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw ex;
            }
        }

        /// <summary> Updates one existing appointment in the database
        /// <param name="appointment"> Inner_Appointment Object - represents the fields of a Appointment in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task UpdateAppointmentAsync(Inner_Appointment appointment)
        {
            Data_Appointment currentEntity = await _context.Appointments.FindAsync(appointment.AppointmentId);
            Data_Appointment newEntity = Mapper.UnMapAppointment(appointment);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        /// <summary> Removes one existing appointment in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        public async Task RemoveAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
            if(appointment == null)
            {
                return;
            }
            _context.Remove(appointment);
            await Save();
        }


        /// <summary> An internal save method when changes are made to the database
        /// <returns> no return </returns>
        /// </summary>
        private async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Error Saving!");
                _logger.LogCritical(ex.Message);
                throw ex;
            }
        }
    }
}
