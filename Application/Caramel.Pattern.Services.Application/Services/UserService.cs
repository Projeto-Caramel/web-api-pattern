using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Repositories.UnitOfWork;
using Caramel.Pattern.Services.Domain.Services;
using Caramel.Pattern.Services.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Pattern.Services.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetSingleAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity);

            var pet = await _unitOfWork.Users.GetSingleAsync(id);

            return pet;
        }

        public async Task<IEnumerable<User>> FetchAsync()
        {
            var pets = await _unitOfWork.Users.FetchAsync();

            return pets;
        }

        public async Task<User> AddAsync(User entity)
        {
            BusinessException.ThrowIfNull(entity, "Usuário");

            ValidateEntity<UserValidator, User>(entity);

            await _unitOfWork.Users.AddAsync(entity);

            var result = _unitOfWork.Save();

            if (result > 0)
                return entity;
            else
                throw new BusinessException("Não foi possível Adicionar o Usuário.", StatusProcess.Failure, HttpStatusCode.BadRequest);
        }

        public User Update(User entity)
        {
            BusinessException.ThrowIfNull(entity, "Usuário");

            if (entity.Id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity);

            ValidateEntity<UserValidator, User>(entity);

            _unitOfWork.Users.Update(entity);

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
                throw new BusinessException("Não foi possível encontrar nenhum Usuário com essas informações.", StatusProcess.Failure, HttpStatusCode.UnprocessableEntity);

            _unitOfWork.Users.Delete(entity);

            var result = _unitOfWork.Save();

            return result > 0 ? true : throw new BusinessException("Não foi possível Deletar o Usuário.", StatusProcess.Failure, HttpStatusCode.BadRequest);

        }
    }
}
