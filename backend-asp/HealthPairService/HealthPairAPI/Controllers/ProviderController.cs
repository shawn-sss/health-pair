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
    public class ProviderController : ControllerBase
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IProvInsurRepository _provInsurRepository;
        private readonly ILogger<ProviderController> _logger;

        // GET: api/provider
        /// <summary> Fetches all providers in the database. Can add a search parameter to narrow search. Null returns all.
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to provider. </param>
        /// <returns> A content result.
        /// 200 with A list of providers, depending on input search
        /// 500 if server error
        ///  </returns>
        /// </summary>
        public ProviderController(IProviderRepository providerRepository, IFacilityRepository facilityRepository, ISpecialtyRepository specialtyRepository, IProvInsurRepository provInsurRepository, ILogger<ProviderController> logger)
        {
            _providerRepository = providerRepository ?? throw new ArgumentException(nameof(providerRepository));
            _facilityRepository = facilityRepository ?? throw new ArgumentException(nameof(facilityRepository));
            _specialtyRepository = specialtyRepository ?? throw new ArgumentException(nameof(specialtyRepository));
            _provInsurRepository = provInsurRepository ?? throw new ArgumentException(nameof(provInsurRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed ProviderController");
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Provider>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Provider> ProviderAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all providers");
                ProviderAll = (await _providerRepository.GetProvidersAsync()).Select(Mapper.MapProvider).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving providers with parameters {search}.");
                ProviderAll = (await _providerRepository.GetProvidersAsync(search)).Select(Mapper.MapProvider).ToList();
            }
            try
            {
                foreach(var val in ProviderAll)
                {
                    val.InsuranceIds = await _provInsurRepository.GetInsuranceCoverage(val.ProviderId);
                }
                _logger.LogInformation($"Sending {ProviderAll.Count} Providers.");
                return Ok(ProviderAll);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error! {e.Message}.");
                return StatusCode(500);
            }
        }

        // GET: api/provider/5
        /// <summary> Fetches one provider from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to provider. </param>
        /// <returns> A content result.
        /// 200 with A provider, depending on input id
        /// 404 if no provider with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Provider), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Provider>> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Retrieving providers with id {id}.");
            if (await _providerRepository.GetProviderByIdAsync(id) is Inner_Provider provider)
            {
                Transfer_Provider providerReal = Mapper.MapProvider(provider);
                providerReal.InsuranceIds = await _provInsurRepository.GetInsuranceCoverage(providerReal.ProviderId);
                return Ok(providerReal);
            }
            _logger.LogInformation($"No providers found with id {id}.");
            return NotFound();
        }

        // POST: api/provider
        /// <summary> Adds a provider to the database.
        /// <param name="provider"> Transfer_Provider Object - This object represents all the input fields of a provider. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Provider), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Transfer_Provider provider)
        {

            try
            {
                _logger.LogInformation($"Adding new provider.");
                var checkProvider = new CheckerClass(_facilityRepository, _specialtyRepository);
                checkProvider.CheckProvider(provider);
                Inner_Provider transformedProvider = new Inner_Provider
                {
                    ProviderId = 0,
                    ProviderFirstName = provider.ProviderFirstName,
                    ProviderLastName = provider.ProviderLastName,
                    ProviderPhoneNumber = provider.ProviderPhoneNumber,
                    Facility = await _facilityRepository.GetFacilityByIdAsync(provider.FacilityId),
                    Specialty = await _specialtyRepository.GetSpecialtyByIdAsync(provider.FacilityId)
                };
                await _providerRepository.AddProviderAsync(transformedProvider);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = provider.ProviderId }, provider);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/provider/5
        /// <summary> Edits one provider based on input provider object.
        /// <param name="provider"> Transfer_Provider Object - This object represents all the input fields of a provider. </param>
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
        public async Task<IActionResult> PutAsync(int id, [FromBody] Transfer_Provider provider)
        {
            _logger.LogInformation($"Editing provider with id {id}.");
            var entity = await _providerRepository.GetProviderByIdAsync(id);
            if (entity is Inner_Provider)
            {
                entity.ProviderFirstName = provider.ProviderFirstName;
                entity.ProviderLastName = provider.ProviderLastName;
                entity.ProviderPhoneNumber = provider.ProviderPhoneNumber;

                return NoContent();
            }
            _logger.LogInformation($"No providers found with id {id}.");
            return NotFound();
        }

        // DELETE: api/provider/5
        /// <summary> Edits one provider based on input provider object.
        /// <param name="id"> int - This int is searched for in the id related to provider. </param>
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
            _logger.LogInformation($"Deleting provider with id {id}.");
            if (await _providerRepository.GetProviderByIdAsync(id) is Inner_Provider provider)
            {
                await _providerRepository.RemoveProviderAsync(provider.ProviderId);
                return NoContent();
            }
            _logger.LogInformation($"No providers found with id {id}.");
            return NotFound();
        }
    }
}