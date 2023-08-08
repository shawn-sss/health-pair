using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using HealthPairAPI.TransferModels;
using HealthPairAPI.Logic;
using HealthPairAPI.Helpers;

namespace HealthPairAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExcludeFromCodeCoverage]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly ILogger<PatientController> _logger;
        private readonly AppSettings _appSettings;

        public PatientController(IPatientRepository patientRepository, IInsuranceRepository insuranceRepository, ILogger<PatientController> logger, IOptions<AppSettings> appSettings)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _insuranceRepository = insuranceRepository ?? throw new ArgumentNullException(nameof(insuranceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings.Value));
            _logger.LogInformation($"Accessed PatientController");
        }

        // GET: api/patient
        /// <summary> Fetches all patients in the database. Can add a search parameter to narrow search. Null returns all. </summary>
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to patient. </param>
        /// <returns> A content result.
        /// 200 with A list of patients, depending on input search
        /// 401 if you are not authenticated
        /// 500 if server error
        ///  </returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Patient> patientAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all patients");
                patientAll = (await _patientRepository.GetPatientsAsync()).Select(Mapper.MapPatient).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving patients with parameters {search}.");
                patientAll = (await _patientRepository.GetPatientsAsync(search)).Select(Mapper.MapPatient).ToList();
            }
            try
            {
                _logger.LogInformation($"Sending {patientAll.Count} Patients.");
                return Ok(patientAll);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error! {e.Message}.");
                return StatusCode(500);
            }
        }

        // GET: api/patient/5
        /// <summary> Fetches one patient from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to patient. </param>
        /// <returns> A content result.
        /// 200 with A patient, depending on input id
        /// 401 if you are not authenticated
        /// 404 if no patient with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Patient>> GetByIdAsync(int id)
        {
            var currentUser = HttpContext.User;
            _logger.LogInformation($"Retrieving patients with id {id}.");
            if (await _patientRepository.GetPatientByIdAsync(id) is Inner_Patient patient)
            {
                var transformedPatient = Mapper.MapPatient(patient);
                return Ok(transformedPatient);
            }
            _logger.LogInformation($"No patients found with id {id}.");
            return NotFound();
        }

        // POST: api/patient
        /// <summary> Adds a patient to the database.
        /// <param name="patient"> Transfer_Patient Object - This object represents all the input fields of a patient. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 401 if you are not authenticated
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Patient), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Transfer_Patient patient)
        {
            try
            {
                _logger.LogInformation($"Adding new patient.");
                var checkPatient = new CheckerClass(_patientRepository,_insuranceRepository);
                checkPatient.CheckPatient(patient);
                Inner_Patient transformedPatient = new Inner_Patient
                {
                    PatientId = 0,
                    PatientFirstName = patient.PatientFirstName,
                    PatientLastName = patient.PatientLastName,
                    PatientPassword = patient.PatientPassword,
                    PatientAddress1 = patient.PatientAddress1,
                    PatientCity = patient.PatientCity,
                    PatientState = patient.PatientState,
                    PatientZipcode = patient.PatientZipcode,
                    PatientBirthDay = patient.PatientBirthDay,
                    PatientPhoneNumber = patient.PatientPhoneNumber,
                    PatientEmail = patient.PatientEmail,
                    Insurance = await _insuranceRepository.GetInsuranceByIdAsync(patient.InsuranceId)
                };
                await _patientRepository.AddPatientAsync(transformedPatient);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = patient.PatientId }, patient);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/patient/5
        /// <summary> Edits one patient based on input patient object.
        /// <param name="patient"> Transfer_Patient Object - This object represents all the input fields of a patient. </param>
        /// <returns> A content result.
        /// 204 upon a successful edit
        /// 401 if you are not authenticated
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
        public async Task<IActionResult> PutAsync(int id, [FromBody] Transfer_Patient patient)
        {
            _logger.LogInformation($"Editing patient with id {id}.");
            var entity = await _patientRepository.GetPatientByIdAsync(id);
            if (entity is Inner_Patient)
            {
                entity.PatientAddress1 = patient.PatientAddress1;
                entity.PatientBirthDay = patient.PatientBirthDay;
                entity.PatientCity = patient.PatientCity;
                entity.PatientFirstName = patient.PatientFirstName;
                entity.PatientLastName = patient.PatientLastName;
                entity.PatientPassword = patient.PatientPassword;
                entity.PatientPhoneNumber = patient.PatientPhoneNumber;
                entity.PatientState = patient.PatientState;
                entity.PatientZipcode = patient.PatientZipcode;
                entity.Insurance = await _insuranceRepository.GetInsuranceByIdAsync(patient.InsuranceId);
                await _patientRepository.UpdatePatientAsync(entity);
                return NoContent();
            }
            _logger.LogInformation($"No patients found with id {id}.");
            return NotFound();
        }

        // DELETE: api/patient/5
        /// <summary> Edits one patient based on input patient object.
        /// <param name="id"> int - This int is searched for in the id related to patient. </param>
        /// <returns> A content result.
        /// 204 upon a successful delete
        /// 401 if you are not authenticated
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
            _logger.LogInformation($"Deleting patient with id {id}.");
            if (await _patientRepository.GetPatientByIdAsync(id) is Inner_Patient patient)
            {
                await _patientRepository.RemovePatientAsync(patient.PatientId);
                return NoContent();
            }
            _logger.LogInformation($"No patients found with id {id}.");
            return NotFound();
        }

        // POST: api/patient/authenticate
        /// <summary> Send data to the database to be checked for authentication purposes.
        /// <param name="model"> Object AuthenticateModel - This object represents the data required to authenticate a user. </param>
        /// <returns> A content result, and a user object (without the password) (with a authentification token) if successful
        /// 200 upon a successful authentification
        /// 400 if you are not authenticated
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(Transfer_Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticateModel model)
        {
            _logger.LogInformation($"Authenticating user");
            Inner_Patient user;
            try
            {
                user = await _patientRepository.GetPatientByEmailAsync(model.Email);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            if (user == null)
            {
                _logger.LogInformation($"User is null.");
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            if(user.PatientPassword != model.Password)
            {
                _logger.LogInformation($"Passwords do not match.");
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            _logger.LogInformation($"Creating token.");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.PatientId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new Transfer_Patient
            {
                PatientId = user.PatientId,
                PatientFirstName = user.PatientFirstName,
                PatientLastName = user.PatientLastName,
                PatientAddress1 = user.PatientAddress1,
                PatientCity = user.PatientCity,
                PatientState = user.PatientState,
                PatientZipcode = user.PatientZipcode,
                PatientBirthDay = user.PatientBirthDay,
                PatientPhoneNumber = user.PatientPhoneNumber,
                PatientEmail = user.PatientEmail,
                InsuranceId = user.Insurance.InsuranceId,
                InsuranceName = user.Insurance.InsuranceName,
                IsAdmin = user.IsAdmin,
                Token = tokenString
            });
        }
    }
}