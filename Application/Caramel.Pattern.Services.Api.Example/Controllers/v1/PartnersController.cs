using AutoMapper;
using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Entities.Models.Request;
using Caramel.Pattern.Services.Domain.Entities.Models.Responses;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Caramel.Pattern.Services.Api.Example.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PartnersController : BaseController
    {
        private readonly ILogger<PartnersController> _logger;
        private readonly IPartnerService _service;
        private readonly IMapper _mapper;

        public PartnersController(
            ILogger<PartnersController> logger,
            IPartnerService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Recupera uma lista de todos os Parceiros.
        /// </summary>
        /// <param name="pagination">Página e Total de dados a serem trazidos. Default: Page = 1 e Size = 10</param>
        /// <returns>Lista de Parceiros, Status do Processo e Descrição</returns>
        [HttpGet("/api/v1/partners")]
        [ProducesResponseType(typeof(CustomResponse<IEnumerable<Partner>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPartners(Pagination pagination)
        {
            var partners = await _service.FetchAsync();
            
            var paginatedPartners = ReturnPaginated(partners, pagination);

            var response = new CustomResponse<IEnumerable<Partner>>(paginatedPartners, StatusProcess.Success); 

            return Ok(response);
        }

        /// <summary>
        /// Recupera um parceiro específico por ID.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro a ser recuperado.</param>
        /// <returns>Parceiro, Status do Processo e Descrição</returns>
        [HttpGet("/api/v1/partners/{partnerId}")]
        [ProducesResponseType(typeof(CustomResponse<Partner>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPartner(int partnerId)
        {
            var partner = await _service.GetSingleAsync(partnerId);

            var response = new CustomResponse<Partner>(partner, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Cria um novo parceiro.
        /// </summary>
        /// <param name="partner">Dados do novo Parceiro.</param>
        /// <returns>Parceiro Criado, Status do Processo e Descrição</returns>
        [HttpPost("/api/v1/partners")]
        [ProducesResponseType(typeof(CustomResponse<Partner>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPartner(PartnerRequest partnerRequest)
        {
            var partner = _mapper.Map<Partner>(partnerRequest);

            var addedPartner = await _service.AddAsync(partner);

            var response = new CustomResponse<Partner>(addedPartner, StatusProcess.Success);

            var uri = Url.Action("GetPartner", "PartnerController", new { id = addedPartner.Id });

            return Created(uri, response);
        }

        /// <summary>
        /// Atualiza um parceiro existente.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro a ser Atualizado.</param>
        /// <param name="partnerRequest">Dados Atualizados do Parceiro.</param>
        /// <returns>Parceiro Atualizado, Status do Processo e Descrição</returns>
        [HttpPut("/api/v1/partners")]
        [ProducesResponseType(typeof(CustomResponse<Partner>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult PutPartner(int partnerId, Partner partnerRequest)
        {
            var partner = _mapper.Map<Partner>(partnerRequest);
            partner.Id = partnerId;

            var updatedPartner = _service.Update(partner);

            var response = new CustomResponse<Partner>(updatedPartner, StatusProcess.Success);

            return Ok(response);
        }

        /// <summary>
        /// Exclui um parceiro por ID.
        /// </summary>
        /// <param name="partnerId">O ID do parceiro a ser excluído.</param>
        /// <returns>Parceiro Deletado, Status do Processo e Descrição</returns>
        [HttpDelete("/api/v1/partners/{partnerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePartner(int partnerId)
        {
            await _service.DeleteAsync(partnerId);

            return NoContent();
        }
    }
}
