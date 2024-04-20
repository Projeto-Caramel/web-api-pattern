﻿using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Repositories.UnitOfWork;
using Caramel.Pattern.Services.Domain.Services;
using Caramel.Pattern.Services.Domain.Validations;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Pattern.Services.Application.Services
{
    public class PetService : BaseService, IPetService
    {
        public PetService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Pet> GetSingleAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.BadRequest);

            var pet = await _unitOfWork.Pets.GetSingleAsync(id);

            if (pet == null)
                throw new BusinessException("Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.NoContent);

            return pet;
        }

        public async Task<PetStatus> GetPetStatusAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.BadRequest);

            var pet = await _unitOfWork.Pets.GetSingleAsync(id);

            if (pet == null)
                throw new BusinessException("Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.NoContent);

            return pet.Status;
        }
        
        public async Task<IEnumerable<Pet>> FetchAsync(int partnerId)
        {
            if (partnerId <= 0)
                throw new BusinessException("O campo Partner ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.BadRequest);

            var pets = await _unitOfWork.Pets.FetchAsync(x => x.PartnerId == partnerId);

            if (!pets.Any())
                throw new BusinessException("Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.NoContent);
            
            return pets;
        }

        public async Task<IEnumerable<Pet>> FetchByFilterAsync(int partnerId, PetFilter filter)
        {
            BusinessException.ThrowIfNul(filter, "Pet Filter");

            var pets = await _unitOfWork.Pets.FetchAsync(x => x.PartnerId == partnerId);
            var petsFiltered = FilterPets(pets, filter);

            if (!petsFiltered.Any())
                throw new BusinessException("Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.NoContent);
            
            return petsFiltered;
        }

        public async Task<Pet> AddAsync(Pet entity)
        {
            ValidateEntity<PetValidator, Pet>(entity, "Pet");

            await _unitOfWork.Pets.AddAsync(entity);

            var result = _unitOfWork.Save();

            if (result > 0)
                return entity;
            else
                throw new BusinessException("Não foi possível Adicionar o Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest);
        }

        public Pet Update(Pet entity)
        {
            ValidateEntity<PetValidator, Pet>(entity, "Pet");

            _unitOfWork.Pets.Update(entity);

            var result = _unitOfWork.Save();

            if (result > 0)
                return entity;
            else
                throw new BusinessException("Não foi possível Alterar o Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest);
        }

        public async Task<PetStatus> UpdateStatusAsync(int id, PetStatus status)
        {
            if (id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.BadRequest);

            var entity = await GetSingleAsync(id);

            if (entity == null)
                throw new BusinessException("Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.NoContent);

            entity.Status = status;

            _unitOfWork.Pets.Update(entity);

            var result = _unitOfWork.Save();

            return result > 0 ? status : throw new BusinessException("Não foi possível Alterar o Status do Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.BadRequest);

            var entity = await GetSingleAsync(id);

            if (entity == null)
                throw new BusinessException("Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.NoContent);

            await _unitOfWork.Pets.AddAsync(entity);

            var result = _unitOfWork.Save();

            return result > 0 ? true : throw new BusinessException("Não foi possível Deletar o Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest);
        }

        private IEnumerable<Pet> FilterPets(IEnumerable<Pet> pets, PetFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                pets = pets.Where(x => x.Name == filter.Name);
            if (filter.Castrated != 0)
                pets = pets.Where(x => x.Castrated == ((int)filter.Castrated == 1 ? true : false));
            if (filter.Vaccinated != 0)
                pets = pets.Where(x => x.Vaccinated == ((int)filter.Castrated == 1 ? true : false));
            if (filter.Age != 0)
                pets = pets.Where(x => x.Age == filter.Age);
            if (filter.Status != 0)
                pets = pets.Where(x => (int)x.Status == (int)filter.Status);

            return pets;
        }
    }
}