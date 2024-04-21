using Caramel.Pattern.Services.Application.Services;
using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Repositories.UnitOfWork;
using Caramel.Services.Pattern.Tests.Mocks.Data;
using Moq;
using System.Net;

namespace Caramel.Services.Pattern.Tests.Application
{
    public class PartnerServiceTest
    {
        [Fact]
        public async Task GetSingleAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PartnerData.Data["Basic"]);
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PartnerService(mock.Object);

            var partner = await service.GetSingleAsync(1);

            Assert.NotNull(partner);
            Assert.Equal(PartnerData.Data["Basic"], partner);
        }

        [Theory]
        [InlineData(0, "O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        public async Task GetSingleAsync_BusinessExceptions(int id, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PartnerData.Data["Basic"]);
            mock.Setup(x => x.Partners.GetSingleAsync(3)).ReturnsAsync(PartnerData.Data["Null"]);

            var service = new PartnerService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
                service.GetSingleAsync(id));

            Assert.Equivalent(exception.ErrorDetails, message);
            Assert.Equal(exception.Status, process);
            Assert.Equal(exception.StatusCode, statusCode);
        }

        [Fact]
        public async Task FetchAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.FetchAsync(null)).ReturnsAsync(PartnersData.Data["Basic"]);

            var service = new PartnerService(mock.Object);

            var partners = await service.FetchAsync();

            Assert.NotNull(partners);
            Assert.Equal(PartnersData.Data["Basic"], partners);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.AddAsync(It.IsAny<Partner>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PartnerService(mock.Object);

            var partner = await service.AddAsync(PartnerData.Data["Basic"]);

            Assert.NotNull(partner);
            Assert.Equal(PartnerData.Data["Basic"], partner);
        }

        [Theory]
        [InlineData("Null", "O parâmetro Parceiro não pode ser nulo.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData("Basic", "Não foi possível Adicionar o Parceiro.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public async Task AddAsync_BusinessExceptions(string key, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.AddAsync(It.IsAny<Partner>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PartnerService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
               service.AddAsync(PartnerData.Data[key]));

            Assert.Equivalent(message, exception.ErrorDetails);
            Assert.Equal(process, exception.Status);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public async Task AddAsync_InvalidModel_BusinessException()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.AddAsync(PartnerData.Data["Empty"])).ThrowsAsync(new Exception());
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PartnerService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
              service.AddAsync(PartnerData.Data["Empty"]));

            Assert.IsType<string[]>(exception.ErrorDetails);
            Assert.Equal(StatusProcess.InvalidRequest, exception.Status);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, exception.StatusCode);
        }

        [Fact]
        public void Update_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.Update(It.IsAny<Partner>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PartnerService(mock.Object);

            var partner = service.Update(PartnerData.Data["BasicUpdate"]);

            Assert.NotNull(partner);
            Assert.Equal(PartnerData.Data["BasicUpdate"], partner);
        }

        [Theory]
        [InlineData("WithoutId", "O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData("Null", "O parâmetro Parceiro não pode ser nulo.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData("BasicUpdateException", "Não foi possível Alterar o Parceiro.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public void Update_BusinessExceptions(string key, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.Update(It.IsAny<Partner>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PartnerService(mock.Object);

            var exception = Assert.Throws<BusinessException>(() =>
               service.Update(PartnerData.Data[key]));

            Assert.Equivalent(message, exception.ErrorDetails);
            Assert.Equal(process, exception.Status);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public void Update_InvalidModel_BusinessException()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.Update(PartnerData.Data["Empty"])).Throws(new Exception());
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PartnerService(mock.Object);

            var exception = Assert.Throws<BusinessException>(() =>
              service.Update(PartnerData.Data["Empty"]));

            Assert.IsType<string[]>(exception.ErrorDetails);
            Assert.Equal(StatusProcess.InvalidRequest, exception.Status);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, exception.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PartnerData.Data["Basic"]);
            mock.Setup(x => x.Partners.Delete(It.IsAny<Partner>())).Verifiable();
            mock.Setup(x => x.Save()).Returns(1);

            var service = new PartnerService(mock.Object);

            var deleted = await service.DeleteAsync(1);

            Assert.True(deleted);
        }

        [Theory]
        [InlineData(0, "O campo ID é obrigatório.", StatusProcess.InvalidRequest, HttpStatusCode.UnprocessableEntity)]
        [InlineData(3, "Não foi possível encontrar nenhum Parceiro com essas informações.", StatusProcess.Failure, HttpStatusCode.UnprocessableEntity)]
        [InlineData(1, "Não foi possível Deletar o Parceiro.", StatusProcess.Failure, HttpStatusCode.BadRequest)]
        public async Task DeleteAsync_BusinessExceptions(int id, string message, StatusProcess process, HttpStatusCode statusCode)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Partners.GetSingleAsync(It.IsAny<int>())).ReturnsAsync(PartnerData.Data["Basic"]);
            mock.Setup(x => x.Partners.GetSingleAsync(3)).ReturnsAsync(PartnerData.Data["Null"]);
            mock.Setup(x => x.Partners.Delete(PartnerData.Data["Empty"])).Throws(new Exception());
            mock.Setup(x => x.Save()).Returns(0);

            var service = new PartnerService(mock.Object);

            var exception = await Assert.ThrowsAsync<BusinessException>(() =>
                service.DeleteAsync(id));

            Assert.Equivalent(exception.ErrorDetails, message);
            Assert.Equal(exception.Status, process);
            Assert.Equal(exception.StatusCode, statusCode);
        }
    }
}
