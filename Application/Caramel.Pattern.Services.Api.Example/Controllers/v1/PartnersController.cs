using Caramel.Pattern.Services.Api.Example.Models.Responses;
using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caramel.Pattern.Services.Api.Example.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PartnersController : BaseController
    {
        private readonly ILogger<PartnersController> _logger;
        private readonly IPartnerService _service;

        public PartnersController(
            ILogger<PartnersController> logger,
            IPartnerService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Recupera uma lista de todos os Parceiros.
        /// </summary>
        /// <param name="page">Página. Se for igual a 0, por padrão será considerada a primeira página.</param>
        /// <param name="pageSize">Quantidade de Pets da Página. Se for igual a 0, por padrão será considerada os 10 primeiros Parceiros.</param>
        /// <returns>Lista de Parceiros, Status do Processo e Descrição</returns>
        [HttpGet("/api/partners")]
        [ProducesResponseType(typeof(CustomResponse<IEnumerable<Partner>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPartners(int page, int pageSize)
        {
            var partners = await _service.FetchAsync();

            var paginatedPartners = ReturnPaginated<Partner>(partners, page, pageSize);

            var response = new CustomResponse<IEnumerable<Partner>>(paginatedPartners, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Recupera um parceiro específico por ID.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro a ser recuperado.</param>
        /// <returns>Parceiro, Status do Processo e Descrição</returns>
        [HttpGet("/api/partner/{partnerId}")]
        [ProducesResponseType(typeof(CustomResponse<Partner>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPartner(int partnerId)
        {
            var partner = await _service.GetSingleAsync(partnerId);

            var response = new CustomResponse<Partner>(partner, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Cria um novo parceiro.
        /// </summary>
        /// <param name="partner">Dados do novo Parceiro.</param>
        /// <returns>Parceiro Criado, Status do Processo e Descrição</returns>
        [HttpPost("/api/partner")]
        [ProducesResponseType(typeof(CustomResponse<Partner>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPartner(Partner partner)
        {
            var addedPartner = await _service.AddAsync(partner);

            var response = new CustomResponse<Partner>(addedPartner, StatusProcess.Success);

            return StatusCode(201, response);
        }

        /// <summary>
        /// Atualiza um parceiro existente.
        /// </summary>
        /// <param name="partner">Dados Atualizados do Parceiro.</param>
        /// <returns>Parceiro Atualizado, Status do Processo e Descrição</returns>
        [HttpPut("/api/partner")]
        [ProducesResponseType(typeof(CustomResponse<Partner>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult PutPartner(Partner partner)
        {
            var updatedPartner = _service.Update(partner);

            var response = new CustomResponse<Partner>(updatedPartner, StatusProcess.Success);

            return StatusCode(200, response);
        }

        /// <summary>
        /// Exclui um parceiro por ID.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro a ser excluído.</param>
        /// <returns>Parceiro Deletado, Status do Processo e Descrição</returns>
        [HttpDelete("/api/partner")]
        [ProducesResponseType(typeof(CustomResponse<Partner>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePartner(int partnerId)
        {
            var deletedPartner = await _service.DeleteAsync(partnerId);

            var response = new CustomResponse<bool>(deletedPartner, StatusProcess.Success);

            return StatusCode(201, response);
        }
    }
}
