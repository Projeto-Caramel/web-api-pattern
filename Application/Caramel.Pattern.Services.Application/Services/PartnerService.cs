using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Repositories.UnitOfWork;
using Caramel.Pattern.Services.Domain.Services;
using Caramel.Pattern.Services.Domain.Validators;
using System.Net;

namespace Caramel.Pattern.Services.Application.Services
{
    public class PartnerService : BaseService, IPartnerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PartnerService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Partner> GetSingleAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity);

            var pet = await _unitOfWork.Partners.GetSingleAsync(id);

            return pet;
        }

        public async Task<IEnumerable<Partner>> FetchAsync()
        {
            var pets = await _unitOfWork.Partners.FetchAsync();

            return pets;
        }

        public async Task<Partner> AddAsync(Partner entity)
        {
            BusinessException.ThrowIfNull(entity, "Parceiro");

            ValidateEntity<PartnerValidator, Partner>(entity);

            await _unitOfWork.Partners.AddAsync(entity);

            var result = _unitOfWork.Save();

            if (result > 0)
                return entity;
            else
                throw new BusinessException("Não foi possível Adicionar o Parceiro.", StatusProcess.Failure, HttpStatusCode.BadRequest);
        }

        public Partner Update(Partner entity)
        {
            BusinessException.ThrowIfNull(entity, "Parceiro");

            if (entity.Id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity);

            ValidateEntity<PartnerValidator, Partner>(entity);

            _unitOfWork.Partners.Update(entity);

            var result = _unitOfWork.Save();

            if (result > 0)
                return entity;
            else
                throw new BusinessException("Não foi possível Alterar o Parceiro.", StatusProcess.Failure, HttpStatusCode.BadRequest);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity);

            var entity = await GetSingleAsync(id);

            if (entity == null)
                throw new BusinessException("Não foi possível encontrar nenhum Parceiro com essas informações.", StatusProcess.Failure, HttpStatusCode.UnprocessableEntity);

            _unitOfWork.Partners.Delete(entity);

            var result = _unitOfWork.Save();

            return result > 0 ? true : throw new BusinessException("Não foi possível Deletar o Parceiro.", StatusProcess.Failure, HttpStatusCode.BadRequest);

        }
    }
}
