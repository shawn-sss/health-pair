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
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace HealthPairAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly ILogger<SpecialtyController> _logger;

        public SpecialtyController(ISpecialtyRepository specialtyRepository, ILogger<SpecialtyController> logger)
        {
            _specialtyRepository = specialtyRepository ?? throw new ArgumentException(nameof(specialtyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed SpecialtyController");
        }

        // GET: api/specialty
        /// <summary> Fetches all specialties in the database. Can add a search parameter to narrow search. Null returns all.
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to specialty. </param>
        /// <returns> A content result.
        /// 200 with A list of specialties, depending on input search
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Specialty>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Specialty> SpecialtyAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all specialties");
                SpecialtyAll = (await _specialtyRepository.GetSpecialtyAsync()).Select(Mapper.MapSpecialty).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving specialties with parameters {search}.");
                SpecialtyAll = (await _specialtyRepository.GetSpecialtyAsync(search)).Select(Mapper.MapSpecialty).ToList();
            }
            try
            {
                _logger.LogInformation($"Sending {SpecialtyAll.Count} Specialties.");
                return Ok(SpecialtyAll);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error! {e.Message}.");
                return StatusCode(500);
            }
        }

        // GET: api/specialty/5
        /// <summary> Fetches one specialty from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to specialty. </param>
        /// <returns> A content result.
        /// 200 with A specialty, depending on input id
        /// 404 if no specialty with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Specialty), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Specialty>> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Retrieving specialties with id {id}.");
            if (await _specialtyRepository.GetSpecialtyByIdAsync(id) is Inner_Specialty specialty)
            {
                return Ok(specialty);
            }
            _logger.LogInformation($"No specialties found with id {id}.");
            return NotFound();
        }

        // POST: api/specialty
        /// <summary> Adds a specialty to the database.
        /// <param name="specialty"> Transfer_Specialty Object - This object represents all the input fields of a specialty. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Specialty), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Transfer_Specialty specialty)
        {
            _logger.LogInformation($"Adding new specialty.");
            Inner_Specialty transformedSpecialty = new Inner_Specialty
            {
                SpecialtyId = 0,
                Specialty = specialty.Specialty

            };
            await _specialtyRepository.AddSpecialtyAsync(transformedSpecialty);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = specialty.SpecialtyId }, specialty);
        }

        // PUT: api/specialty/5
        /// <summary> Edits one specialty based on input specialty object.
        /// <param name="specialty"> Transfer_Specialty Object - This object represents all the input fields of a specialty. </param>
        /// <returns> A content result.
        /// 204 upon a successful edit
        /// 404 if input object's id was not found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Transfer_Specialty specialty)
        {
            _logger.LogInformation($"Editing specialty with id {id}.");
            var entity = await _specialtyRepository.GetSpecialtyByIdAsync(id);
            if (entity is Inner_Specialty)
            {
                entity.Specialty = specialty.Specialty;

                return NoContent();
            }
            _logger.LogInformation($"No specialties found with id {id}.");
            return NotFound();
        }

        // DELETE: api/specialty/5
        /// <summary> Edits one specialty based on input specialty object.
        /// <param name="id"> int - This int is searched for in the id related to specialty. </param>
        /// <returns> A content result.
        /// 204 upon a successful delete
        /// 404 if input id was not found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting specialty with id {id}.");
            if (await _specialtyRepository.GetSpecialtyByIdAsync(id) is Inner_Specialty specialty)
            {
                await _specialtyRepository.RemoveSpecialtyAsync(specialty.SpecialtyId);
                return NoContent();
            }
            _logger.LogInformation($"No specialties found with id {id}.");
            return NotFound();
        }
    }
}