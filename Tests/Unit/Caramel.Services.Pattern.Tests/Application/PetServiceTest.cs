using Caramel.Pattern.Services.Application.Services;
using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Repositories.UnitOfWork;
using Caramel.Services.Pattern.Tests.Mocks;
using Caramel.Services.Pattern.Tests.Mocks.Data;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace Caramel.Services.Pattern.Tests.Application
{
    public class PetServiceTest
    {
        [Fact]
        public async Task GetSingleAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PetData.Data["Basic"]);

            var service = new PetService(mock.Object);

            var pet = await service.GetSingleAsync(1);

            Assert.NotNull(pet);
            Assert.Equivalent(PetData.Data["Basic"], pet);
        }

        [Theory]
        [InlineData(0, "O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        public async Task GetSingleAsync_BusinessExceptions(int id, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PetData.Data["Basic"]);
            mock.Setup(x => x.Pets.GetSingleAsync(3)).ReturnsAsync(PetData.Data["Null"]);

            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
                service.GetSingleAsync(id));

            Assert.Equivalent(exception.ErrorDetails, message);
            Assert.Equal(exception.Status, process);
            Assert.Equal(exception.StatusCode, statusCode);
        }

        [Fact]
        public async Task GetPetStatusAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PetData.Data["Basic"]);

            var service = new PetService(mock.Object);

            var petStatus = await service.GetPetStatusAsync(1);

            Assert.Equivalent(PetData.Data["Basic"].Status, petStatus);
        }

        [Theory]
        [InlineData(0, "O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData(3, "Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public async Task GetPetStatusAsync_BusinessExceptions(int id, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(3)).ReturnsAsync(PetData.Data["Null"]);
            
            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
                service.GetPetStatusAsync(id));

            Assert.Equivalent(message, exception.ErrorDetails);
            Assert.Equal(process, exception.Status);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public async Task FetchAsyncAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.FetchAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(PetsData.Data["Basic"]);

            var service = new PetService(mock.Object);

            var pets = await service.FetchAsync(1);

            Assert.NotNull(pets);
            Assert.Equal(PetsData.Data["Basic"], pets);
        }

        [Theory]
        [InlineData(0, "O campo Partner ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        public async Task FetchAsync_BusinessExceptions(int id, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.FetchAsync(It.Is<Expression<Func<Pet, bool>>>(x =>
                x.Compile().Invoke(new Pet { PartnerId = 3 }))))
                .ReturnsAsync(PetsData.Data["Empty"]);

            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
                service.FetchAsync(id));

            Assert.Equivalent(message, exception.ErrorDetails);
            Assert.Equal(process, exception.Status);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public async Task FetchByFilterAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.FetchAsync(It.Is<Expression<Func<Pet, bool>>>(x =>
               x.Compile().Invoke(new Pet { PartnerId = 2 }))))
               .ReturnsAsync(PetsData.Data["Filtered"]);

            var service = new PetService(mock.Object);

            var pets = await service.FetchByFilterAsync(2, PetFilterData.Data["Basic"]);

            Assert.NotNull(pets);
            Assert.NotEqual(PetsData.Data["Filtered"], pets);
            Assert.Equal(2, pets.Count());
        }

        [Theory]
        [InlineData(0, "Null", "O parâmetro Pet Filter não pode ser nulo.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        public async Task FetchByFilterAsync_BusinessExceptions(int id, string key, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.FetchAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(PetsData.Data["Basic"]);

            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
               service.FetchByFilterAsync(id, PetFilterData.Data[key]));

            Assert.Equivalent(message, exception.ErrorDetails);
            Assert.Equal(process, exception.Status);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.FetchAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(PetsData.Data["Basic"]);
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PetService(mock.Object);

            var pet = await service.AddAsync(PetData.Data["Basic"]);

            Assert.NotNull(pet);
            Assert.Equivalent(PetData.Data["Basic"], pet);
        }

        [Theory]
        [InlineData("Null", "O parâmetro Pet não pode ser nulo.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData("Basic", "Não foi possível Adicionar o Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public async Task AddAsync_BusinessExceptions(string key, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.FetchAsync(It.IsAny<Expression<Func<Pet, bool>>>())).ReturnsAsync(PetsData.Data["Basic"]);
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
               service.AddAsync(PetData.Data[key]));

            Assert.Equivalent(message, exception.ErrorDetails);
            Assert.Equal(process, exception.Status);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public async Task AddAsync_InvalidModel_BusinessException()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.AddAsync(PetData.Data["Empty"])).ThrowsAsync(new Exception());
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
              service.AddAsync(PetData.Data["Empty"]));

            Assert.IsType<string[]>(exception.ErrorDetails);
            Assert.Equal(StatusProcess.InvalidRequest, exception.Status);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, exception.StatusCode);
        }

        [Fact]
        public void Update_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.Update(It.IsAny<Pet>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PetService(mock.Object);

            var pet = service.Update(PetData.Data["BasicUpdateSuccess"]);

            Assert.NotNull(pet);
            Assert.Equivalent(PetData.Data["BasicUpdateSuccess"], pet);
        }

        [Theory]
        [InlineData("Null", "O parâmetro Pet não pode ser nulo.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData("BasicUpdateException", "Não foi possível Alterar o Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public void Update_BusinessExceptions(string key, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.Update(It.IsAny<Pet>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PetService(mock.Object);

            var exception = Assert.Throws<BusinessException>(() =>
               service.Update(PetData.Data[key]));

            Assert.Equivalent(message, exception.ErrorDetails);
            Assert.Equal(process, exception.Status);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public void Update_InvalidModel_BusinessException()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.Update(PetData.Data["Empty"])).Throws(new Exception());
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PetService(mock.Object);

            var exception = Assert.Throws<BusinessException>(() =>
              service.Update(PetData.Data["Empty"]));

            Assert.IsType<string[]>(exception.ErrorDetails);
            Assert.Equal(StatusProcess.InvalidRequest, exception.Status);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateStatusAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PetData.Data["Basic"]);
            mock.Setup(x => x.Pets.Update(It.IsAny<Pet>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PetService(mock.Object);

            var petStatus = await service.UpdateStatusAsync(4, PetStatus.AdoptOng);

            Assert.NotEqual(PetStatus.Unavailable, petStatus);
        }

        [Theory]
        [InlineData(0, "O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData(3, "Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.UnprocessableEntity)]
        [InlineData(4, "Não foi possível Alterar o Status do Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public async Task UpdateStatusAsync_BusinessExceptions(int id, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PetData.Data["Basic"]);
            mock.Setup(x => x.Pets.GetSingleAsync(3)).ReturnsAsync(PetData.Data["Null"]);
            mock.Setup(x => x.Pets.GetSingleAsync(4)).ReturnsAsync(PetData.Data["BasicUpdateStatus"]);
            mock.Setup(x => x.Pets.Update(It.IsAny<Pet>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
                service.UpdateStatusAsync(id, PetStatus.AdoptOng));

            Assert.Equivalent(exception.ErrorDetails, message);
            Assert.Equal(exception.Status, process);
            Assert.Equal(exception.StatusCode, statusCode);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PetData.Data["Basic"]);
            mock.Setup(x => x.Pets.Delete(It.IsAny<Pet>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PetService(mock.Object);

            var deleted = await service.DeleteAsync(1);

            Assert.True(deleted);
        }

        [Theory]
        [InlineData(0, "O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData(3, "Não foi possível encontrar nenhum Pet com essas informações.", StatusProcess.Failure, HttpStatusCode.UnprocessableEntity)]
        [InlineData(1, "Não foi possível Deletar o Pet.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public async Task DeleteAsync_BusinessExceptions(int id, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Pets.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PetData.Data["Basic"]);
            mock.Setup(x => x.Pets.GetSingleAsync(3)).ReturnsAsync(PetData.Data["Null"]);
            mock.Setup(x => x.Pets.Delete(It.IsAny<Pet>())).Verifiable();
            mock.Setup(x => x.Pets.Delete(PetData.Data["Empty"])).Throws(new Exception());
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PetService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
                service.DeleteAsync(id));

            Assert.Equivalent(exception.ErrorDetails, message);
            Assert.Equal(exception.Status, process);
            Assert.Equal(exception.StatusCode, statusCode);
        }
    }
}
