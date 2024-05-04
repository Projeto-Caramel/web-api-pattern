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

            // Faz a chamada GET para a API
            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/partners?page={pagination.Page}&size={pagination.Size}");

            Assert.True(response.IsSuccessStatusCode);
            // Validar StatusProccess
        }

        [Fact]
        public async Task PostPartner_SuccessAsync()
        {
            var partnerRequest = new PartnerRequest
            {
                Name = "Teste",
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

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/v1/partners", partnerRequest, options);

            var body = await response.Content.ReadAsStringAsync();

            var customResponse = JsonSerializer.Deserialize<CustomResponse<Partner>>(body);

            Assert.True(response.IsSuccessStatusCode);
            // Validar StatusProcces
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
            // Validar BusinessException
            Assert.Equal(StatusProcess.InvalidRequest, exception.Status);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async Task PutPartner_SuccessAsync()
        {
            var partnerRequest = new PartnerRequest
            {            
                Name = "TestePedroGay",
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

            int id = 4;

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/v1/partners?partnerId={id}", partnerRequest, options);

            var body = await response.Content.ReadAsStringAsync();

            var customResponse = JsonSerializer.Deserialize<CustomResponse<Partner>>(body);

            Assert.True(response.IsSuccessStatusCode);
            // Validar StatusProcces
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal("Processado com Sucesso", customResponse.Description);

            HttpResponseMessage responseGet = await _httpClient.GetAsync($"api/v1/partners/{id}");

            var bodyTest = await responseGet.Content.ReadAsStringAsync();

            var customResponseGet = JsonSerializer.Deserialize<CustomResponse<Partner>>(bodyTest);
            var partner = customResponseGet.Data;


            Assert.True(responseGet.IsSuccessStatusCode);
            Assert.Equal(partnerRequest.Name, partner.Name);
        }
    }
}