using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Entities.Models.Request;
using Caramel.Pattern.Services.Domain.Entities.Models.Responses;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Caramel.Services.Pattern.Tests
{
    public class PartnersControllerTest
    {
        private readonly HttpClient _httpClient;

        public PartnersControllerTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7127/");
        }

        [Fact]
        public async Task GetPartners_SuccessAsync()
        {
            var pagination = new { Page = 1, Size = 10 };

            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/partners?page={pagination.Page}&size={pagination.Size}");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task PostPartner_SuccessAsync()
        {
            var partnerRequest = new PartnerRequest
            {
                Name = "Teste1",
                Description = "testeDesc1",
                Email = "testeEmail1",
                Phone = "testephone1",
                Cnpj = "testeCNPJ1",
                AdoptionRate = 1
            };
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/v1/partners", partnerRequest, options);

            var body = await response.Content.ReadAsStringAsync();

            var customResponse = JsonSerializer.Deserialize<CustomResponse<Partner>>(body);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal("Processado com Sucesso", customResponse.Description);
        }

        [Fact]
        public async Task PostPartner_ExceptionAsync()
        {
            var partnerRequest = new PartnerRequest();

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/v1/partners", partnerRequest, options);

            var body = await response.Content.ReadAsStringAsync();

            var exception = JsonSerializer.Deserialize<BusinessException>(body);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(StatusProcess.InvalidRequest, exception.Status);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async Task PutPartner_SuccessAsync()
        {
            var partnerRequest = new PartnerRequest
            {            
                Name = "TesteAlteracao",
                Description = "testeDesc",
                Email = "testeEmail",
                Phone = "testephone",
                Cnpj = "testeCNPJ",
                AdoptionRate = 2
            };

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            int id = 8;

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/v1/partners?partnerId={id}", partnerRequest, options);

            var body = await response.Content.ReadAsStringAsync();

            var customResponse = JsonSerializer.Deserialize<CustomResponse<Partner>>(body);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal("Processado com Sucesso", customResponse.Description);

            HttpResponseMessage responseGet = await _httpClient.GetAsync($"api/v1/partners/{id}");

            var bodyTest = await responseGet.Content.ReadAsStringAsync();

            var customResponseGet = JsonSerializer.Deserialize<CustomResponse<Partner>>(bodyTest);
            var partner = customResponseGet.Data;


            Assert.True(responseGet.IsSuccessStatusCode);
            Assert.Equal(partnerRequest.Name, partner.Name);
        }

        [Fact]
        public async Task PutPartner_ExceptionAsync()
        {
            var partnerRequest = new PartnerRequest
            {
                Name = "",
                Description = "Desc",
                Email = "email@test.com",
                Phone = "1234567890",
                Cnpj = "00.000.000/0001-00",
                AdoptionRate = 1
            };

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            int id = 8; 

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/v1/partners?partnerId={id}", partnerRequest, options);

            var body = await response.Content.ReadAsStringAsync();

            var exception = JsonSerializer.Deserialize<ExceptionResponse>(body);

            Assert.False(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.UnprocessableEntity);
            Assert.NotNull(exception);
            Assert.NotEmpty(exception.Description);
        }

        [Fact]
        public async Task DeletePartner_SuccessAsync()
        {
            int partnerId = 9;

            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/v1/partners/{partnerId}");

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeletePartner_NotFoundAsync()
        {
            int partnerId = 9999;
            
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/v1/partners/{partnerId}");

            //var body = await response.Content.ReadAsStringAsync();
            //var exception = JsonSerializer.Deserialize<ExceptionResponse>(body);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
           // Assert.Equal("Parceiro não encontrado", exception.Description);
        }
    }
}