using Caramel.Pattern.Services.Api.Example.Models.Responses;
using Caramel.Pattern.Services.Domain.Entities;
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

        public PetsController(
            ILogger<PetsController> logger,
            IPetService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Recupera uma lista de pets associados a um parceiro específico.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro.</param>
        /// <param name="page">Página. Se for igual a 0, por padrão será considerada a primeira página.</param>
        /// <param name="pageSize">Quantidade de Pets da Página. Se for igual a 0, por padrão será considerada os 10 primeiros Pets.</param>
        /// <returns>Lista de Pets, Status do Processo e Descrição</returns>
        [HttpGet("/api/pets")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPets(int partnerId, int page, int pageSize)
        {
            var pets = await _service.FetchAsync(partnerId);

            var paginetedPets = ReturnPaginated<Pet>(pets, page, pageSize);

            var response = new CustomResponse<IEnumerable<Pet>>(paginetedPets, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Recupera uma lista de pets filtrada por critérios específicos para um parceiro.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro.</param>
        /// <param name="page">Página. Se for igual a 0, por padrão será considerada a primeira página.</param>
        /// <param name="pageSize">Quantidade de Pets da Página. Se for igual a 0, por padrão será considerada os 10 primeiros Pets.</param>
        /// <param name="filter">O filtro a ser aplicado (objeto PetFilter).</param>
        /// <returns>Lista de Pets Filtrados, Status do Processo e Descrição.</returns>
        [HttpGet("/api/pets-filtered")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPetsFiltered(int partnerId, int page, int pageSize, PetFilter filter)
        {
            var pets = await _service.FetchByFilterAsync(partnerId, filter);

            var paginetedPets = ReturnPaginated<Pet>(pets, page, pageSize);

            var response = new CustomResponse<IEnumerable<Pet>>(paginetedPets, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Recupera um pet específico por ID.
        /// </summary>
        /// <param name="petId">O ID do pet a ser recuperado.</param>
        /// <returns>Pet, Status do Processo e Descrição.</returns>
        [HttpGet("/api/pet")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPet(int petId)
        {
            var pet = await _service.GetSingleAsync(petId);

            var response = new CustomResponse<Pet>(pet, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Recupera o status de um pet específico por ID.
        /// </summary>
        /// <param name="petId">O ID do pet a ser consultado.</param>
        /// <returns>Status do Pet, Status do Processo e Descrição.</returns>
        [HttpGet("/api/pet/status")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPetStatus(int petId)
        {
            var petStatus = await _service.GetPetStatusAsync(petId);

            var response = new CustomResponse<PetStatus>(petStatus, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Cria um novo pet.
        /// </summary>
        /// <param name="pet">O objeto Pet a ser criado.</param>
        /// <returns>Pet Adicionado, Status do Processo e Descrição.</returns>
        [HttpPost("/api/pet")]
        [ProducesResponseType(typeof(CustomResponse<Pet>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPet(Pet pet)
        {
            var addedPet = await _service.AddAsync(pet);

            var response = new CustomResponse<Pet>(addedPet, StatusProcess.Success);

            return StatusCode(201, response);
        }

        /// <summary>
        /// Atualiza um pet existente.
        /// </summary>
        /// <param name="pet">O objeto Pet com dados atualizados.</param>
        /// <returns>Pet Atualizado, Status do Processo e Descrição.</returns>
        [HttpPut("/api/pet")]
        [ProducesResponseType(typeof(CustomResponse<PetStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult PutPet(Pet pet)
        {
            var updatedPet = _service.Update(pet);

            var response = new CustomResponse<Pet>(updatedPet, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Atualiza o status de um pet existente.
        /// </summary>
        /// <param name="pet">O objeto Pet com dados atualizados.</param>
        /// <returns>Pet Atualizado, Status do Processo e Descrição.</returns>
        [HttpPatch("/api/pet/status")]
        [ProducesResponseType(typeof(CustomResponse<PetStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPet(int petId, PetStatus status)
        {
            var updatedStatus = await _service.UpdateStatusAsync(petId, status);

            var response = new CustomResponse<PetStatus>(updatedStatus, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Deleta um pet existente.
        /// </summary>
        /// <param name="pet">O objeto Pet com dados deletados.</param>
        /// <returns>Uma CustomResponse contendo o objeto Pet atualizado ou uma mensagem de erro.</returns>
        [HttpDelete("/api/pet")]
        [ProducesResponseType(typeof(CustomResponse<PetStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePet(int petId)
        {
            var deletedPet = await _service.DeleteAsync(petId);

            var response = new CustomResponse<bool>(deletedPet, StatusProcess.Success);

            return StatusCode(201, response);
        }
    }
}
