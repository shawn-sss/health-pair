using HealthPairDomain.InnerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    /// <summary> Interface for the Appointment Repository </summary>
    public interface IAppointmentRepository
    {

        /// <summary> Fetches all appointments related to input string. Null fetches all.
        /// <param name="search"> string - search params are looked for in multiple fields in database </param>
        /// <returns> All appointments related to input string </returns>
        /// </summary>
        Task<List<Inner_Appointment>> GetAppointmentAsync(string search = null);

        /// <summary> Fetches one appointment related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> One appointment related to input string </returns>
        /// </summary>
        Task<Inner_Appointment> GetAppointmentByIdAsync(int id);

        /// <summary> Checks if one appointment exists related to input id.
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> Yes/No Id is related to a value in the database </returns>
        /// </summary>
        Task<bool> AppointmentExistAsync(int id);

        /// <summary> Add one appointment to the database
        /// <param name="appointment"> Inner_Appointment Object - represents the fields of a Appointment in the database </param>
        /// <returns> Returns inputted (and formatted) Appointment </returns>
        /// </summary>
        Task<Inner_Appointment> AddAppointmentAsync(Inner_Appointment appointment);

        /// <summary> Updates one existing appointment in the database
        /// <param name="appointment"> Inner_Appointment Object - represents the fields of a Appointment in the database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task UpdateAppointmentAsync(Inner_Appointment appointment);

        /// <summary> Removes one existing appointment in the database
        /// <param name="id"> int - search id is looked for in id field of database </param>
        /// <returns> no return </returns>
        /// </summary>
        Task RemoveAppointmentAsync(int id);
    }
}
