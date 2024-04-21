using AutoMapper;
using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Entities.Models.Request;
using Caramel.Pattern.Services.Domain.Entities.Models.Responses;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caramel.Pattern.Services.Api.Example.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PetsController : BaseController
    {
        private readonly ILogger<PetsController> _logger;
        private readonly IPetService _service;
        private readonly IMapper _mapper;

        public PetsController(
            ILogger<PetsController> logger,
            IPetService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Recupera uma lista de pets associados a um parceiro específico.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro.</param>
        /// <param name="pagination">Página e Total de dados a serem trazidos. Default: Page = 1 e Size = 10</param>
        /// <returns>Lista de Pets, Status do Processo e Descrição</returns>
        [HttpGet("/api/v1/pets")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPets(int partnerId, Pagination pagination)
        {
            var pets = await _service.FetchAsync(partnerId);

            var paginetedPets = ReturnPaginated<Pet>(pets, pagination);

            var response = new CustomResponse<IEnumerable<Pet>>(paginetedPets, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Recupera uma lista de pets filtrada por critérios específicos para um parceiro.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro.</param>
        /// <param name="request">Página e Total de dados a serem trazidos e Filtro a ser realizado. Default: Page = 1 e Size = 10</param>
        /// <returns>Lista de Pets Filtrados, Status do Processo e Descrição.</returns>
        [HttpGet("/api/v1/pets/filtered")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPetsFiltered(int partnerId, GetPetsFilteredRequest request)
        {
            var pets = await _service.FetchByFilterAsync(partnerId, request.PetFilter);

            var paginetedPets = ReturnPaginated<Pet>(pets, request.Pagination);

            var response = new CustomResponse<IEnumerable<Pet>>(paginetedPets, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Recupera um pet específico por ID.
        /// </summary>
        /// <param name="petId">O ID do pet a ser recuperado.</param>
        /// <returns>Pet, Status do Processo e Descrição.</returns>
        [HttpGet("/api/v1/pets/{petId}")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPet(int petId)
        {
            var pet = await _service.GetSingleAsync(petId);

            var response = new CustomResponse<Pet>(pet, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Recupera o status de um pet específico por ID.
        /// </summary>
        /// <param name="petId">O ID do pet a ser consultado.</param>
        /// <returns>Status do Pet, Status do Processo e Descrição.</returns>
        [HttpGet("/api/v1/pets/{petId}/status")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPetStatus(int petId)
        {
            var petStatus = await _service.GetPetStatusAsync(petId);

            var response = new CustomResponse<PetStatus>(petStatus, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Cria um novo pet.
        /// </summary>
        /// <param name="petRequest">O objeto Pet a ser criado.</param>
        /// <returns>Pet Adicionado, Status do Processo e Descrição.</returns>
        [HttpPost("/api/v1/pets")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPet(PetRequest petRequest)
        {
            var pet = _mapper.Map<Pet>(petRequest);

            var addedPet = await _service.AddAsync(pet);

            var response = new CustomResponse<Pet>(addedPet, StatusProcess.Success);

            var uri = Url.Action("GetPet", "PetsController", new { id = addedPet.Id });

            return Created(uri, response);
        }

        /// <summary>
        /// Atualiza um pet existente.
        /// </summary>
        /// <param name="petId">O ID do pet a ser Alterado.</param>
        /// <param name="petRequest">O objeto Pet com dados atualizados.</param>
        /// <returns>Pet Atualizado, Status do Processo e Descrição.</returns>
        [HttpPut("/api/v1/pets")]
        [ProducesResponseType(typeof(CustomResponse<PetStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult PutPet(int petId, PetRequest petRequest)
        {
            var pet = _mapper.Map<Pet>(petRequest);
            pet.Id = petId;

            var updatedPet = _service.Update(pet);

            var response = new CustomResponse<Pet>(updatedPet, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Atualiza o status de um pet existente.
        /// </summary>
        /// <param name="petId">O ID do pet a ser Alterado.</param>
        /// <param name="status">O novo Status.</param>
        /// <returns>Pet Atualizado, Status do Processo e Descrição.</returns>
        [HttpPatch("/api/v1/pets/{petId}/status")]
        [ProducesResponseType(typeof(CustomResponse<PetStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPet(int petId, PetStatus status)
        {
            var updatedStatus = await _service.UpdateStatusAsync(petId, status);

            var response = new CustomResponse<PetStatus>(updatedStatus, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Deleta um pet existente.
        /// </summary>
        /// <param name="petId">O ID do pet a ser Deletado.</param>
        /// <returns>Uma CustomResponse contendo o objeto Pet atualizado ou uma mensagem de erro.</returns>
        [HttpDelete("/api/v1/pets/{petId}")]
        [ProducesResponseType(typeof(CustomResponse<PetStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePet(int petId)
        {
            await _service.DeleteAsync(petId);

            return NoContent();
        }
    }
}
