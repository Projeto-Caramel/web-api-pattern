using Caramel.Pattern.Services.Domain.Entities;
using Caramel.Pattern.Services.Domain.Entities.Models.Request;
using Caramel.Pattern.Services.Domain.Entities.Models.Responses;
using Microsoft.Extensions.Options;
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

            var json = JsonSerializer.Serialize(partnerRequest, options);

            // Faz a chamada Post para a API
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/v1/partners", json);

            var teste = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}