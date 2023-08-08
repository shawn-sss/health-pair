using HealthPairAPI.Logic;
using HealthPairAPI.TransferModels;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace HealthPairAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentRepository appointmentRepository, IProviderRepository providerRepository, IPatientRepository patientRepository, ILogger<AppointmentController> logger)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed AppointmentController");
        }

        // GET: api/appointment
        /// <summary> Fetches all appointments in the database. Can add a search parameter to narrow search. Null returns all.
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to appointment. </param>
        /// <returns> A content result.
        /// 200 with A list of appointments, depending on input search
        /// 401 if not authorized
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Transfer_Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Appointment> AppointmentAll = new List<Transfer_Appointment>();
            if (search == null)
            {
                try
                {
                    _logger.LogInformation($"Retrieving all appointments");
                    AppointmentAll = (await _appointmentRepository.GetAppointmentAsync()).Select(Mapper.MapAppointments).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                }
            }
            else
            {
                _logger.LogInformation($"Retrieving appointments with parameters {search}.");
                AppointmentAll = (await _appointmentRepository.GetAppointmentAsync(search)).Select(Mapper.MapAppointments).ToList();
            }
            try
            {
                _logger.LogInformation($"Sending {AppointmentAll.Count} Appointments.");
                return Ok(AppointmentAll);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error! {e.Message}.");
                return StatusCode(500);
            }
        }

        // GET: api/appointment/5
        /// <summary> Fetches one appointment from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to appointment. </param>
        /// <returns> A content result.
        /// 200 with A appointment, depending on input id
        /// 401 if not authorized
        /// 404 if no appointment with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Appointment>> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Retrieving appointments with id {id}.");
            if (await _appointmentRepository.GetAppointmentByIdAsync(id) is Inner_Appointment appointment)
            {
                return Ok(appointment);
            }
            _logger.LogInformation($"No appointments found with id {id}.");
            return NotFound();
        }

        // POST: api/appointment
        /// <summary> Adds a appointment to the database.
        /// <param name="appointment"> Transfer_Appointment Object - This object represents all the input fields of a appointment. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 401 is not authenticated
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Appointment), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Transfer_Appointment appointment)
        {
            try
            {
                _logger.LogInformation($"Adding new appointment.");
                var myChecker = new CheckerClass(_patientRepository, _providerRepository, _appointmentRepository);
                myChecker.CheckAppointment(appointment);
                Inner_Appointment transformedAppointment = new Inner_Appointment
                {
                    AppointmentId = 0,
                    AppointmentDate = (DateTime)appointment.AppointmentDate,
                    Patient = await _patientRepository.GetPatientByIdAsync(appointment.PatientId),
                    Provider = await _providerRepository.GetProviderByIdAsync(appointment.ProviderId)
                };
                await _appointmentRepository.AddAppointmentAsync(transformedAppointment);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = appointment.AppointmentId }, appointment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/appointment/5
        /// <summary> Edits one appointment based on input appointment object.
        /// <param name="appointment"> Transfer_Appointment Object - This object represents all the input fields of a appointment. </param>
        /// <returns> A content result.
        /// 204 upon a successful edit
        /// 404 if input object's id was not found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Transfer_Appointment appointment)
        {
            _logger.LogInformation($"Editing appointment with id {id}.");
            var entity = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (entity is Inner_Appointment)
            {
                entity.AppointmentDate = (DateTime)appointment.AppointmentDate;

                return NoContent();
            }
            _logger.LogInformation($"No appointments found with id {id}.");
            return NotFound();
        }

        // DELETE: api/appointment/5
        /// <summary> Edits one appointment based on input appointment object.
        /// <param name="id"> int - This int is searched for in the id related to appointment. </param>
        /// <returns> A content result.
        /// 204 upon a successful delete
        /// 404 if input id was not found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting appointment with id {id}.");
            if (await _appointmentRepository.GetAppointmentByIdAsync(id) is Inner_Appointment appointment)
            {
                await _appointmentRepository.RemoveAppointmentAsync(appointment.AppointmentId);
                return NoContent();
            }
            _logger.LogInformation($"No appointments found with id {id}.");
            return NotFound();
        }
    }
}