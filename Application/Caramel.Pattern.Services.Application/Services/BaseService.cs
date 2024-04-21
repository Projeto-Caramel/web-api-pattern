using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Repositories.UnitOfWork;
using FluentValidation;
using System.Net;

namespace Caramel.Pattern.Services.Application.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected void ValidateEntity<T, TEntity>(TEntity entity)
            where T : AbstractValidator<TEntity>, new()
        {
            var validatorInstance = new T();
            var result = validatorInstance.Validate(entity);

            if (!result.IsValid)
                throw new BusinessException(
                    result.Errors.Select(x => x.ErrorMessage).ToArray(),
                    StatusProcess.InvalidRequest,
                    HttpStatusCode.UnprocessableEntity);
        }
    }
}
