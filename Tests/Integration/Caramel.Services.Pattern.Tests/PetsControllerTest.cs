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
    public class PetsControllerTest
    {
        private readonly HttpClient _httpClient;

        public PetsControllerTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7127/");
        }

        [Fact]
        public async Task GetPets_SuccessAsync()
        {
            int partnerId = 5;
            var pagination = new { Page = 1, Size = 10 };

            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/pets?partnerId={partnerId}&page={pagination.Page}&size={pagination.Size}");

            var body = await response.Content.ReadAsStringAsync();
            var petsResponse = JsonSerializer.Deserialize<CustomResponse<IEnumerable<Pet>>>(body);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(StatusProcess.Success, petsResponse.Status);
            Assert.Equal("Processado com Sucesso", petsResponse.Description);
        }

        [Fact]
        public async Task GetPet_SuccessAsync()
        {
            int petId = 1;

            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/pets/{petId}");

            var body = await response.Content.ReadAsStringAsync();
            var customResponse = JsonSerializer.Deserialize<CustomResponse<Pet>>(body);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal("Processado com Sucesso", customResponse.Description);
        }

        [Fact]
        public async Task GetPetStatus_SuccessAsync()
        {
            int petId = 1;

            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/pets/{petId}/status");

            var body = await response.Content.ReadAsStringAsync();
            var customResponse = JsonSerializer.Deserialize<CustomResponse<PetStatus>>(body);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal("Processado com Sucesso", customResponse.Description);
        }

        [Fact]
        public async Task PostPet_SuccessAsync()
        {
            var petRequest = new PetRequest
            {
                PartnerId = 6,
                Name = "Ramone",
                Description = "Pitbull de raça",
                Castrated = true,
                Vaccinated = true,
                Age = 2,
                Status = PetStatus.Available
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/v1/pets", petRequest, options);

            var body = await response.Content.ReadAsStringAsync();
            var customResponse = JsonSerializer.Deserialize<CustomResponse<Pet>>(body);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal(petRequest.Name, customResponse.Data.Name);
        }

        [Fact]
        public async Task PostPet_ExceptionAsync()
        {
            var petRequest = new PetRequest
            {
                // Omitindo campos obrigatórios para provocar um erro
                PartnerId = 6,
                Name = "",
                Description = "",
                Castrated = true,
                Vaccinated = true,
                Age = 3,
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/v1/pets", petRequest, options);

            var body = await response.Content.ReadAsStringAsync();
            var exceptionResponse = JsonSerializer.Deserialize<ExceptionResponse>(body);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            Assert.Equal(StatusProcess.InvalidRequest, exceptionResponse.Status);
        }

        [Fact]
        public async Task PutPet_SuccessAsync()
        {
            var petRequest = new PetRequest
            {
                PartnerId = 6,
                Name = "Buddy Updated",
                Description = "Updated description of golden retriever",
                Castrated = true,
                Vaccinated = true,
                Age = 4,
                Status = PetStatus.Available
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            int petId = 1;

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/v1/pets?petId={petId}", petRequest, options);

            var body = await response.Content.ReadAsStringAsync();

            var customResponse = JsonSerializer.Deserialize<CustomResponse<Pet>>(body);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal("Processado com Sucesso", customResponse.Description);

            HttpResponseMessage responseGet = await _httpClient.GetAsync($"api/v1/pets/{petId}");

            var bodyTest = await responseGet.Content.ReadAsStringAsync();

            var customResponseGet = JsonSerializer.Deserialize<CustomResponse<Pet>>(bodyTest);
            var updatedPet = customResponseGet.Data;

            Assert.True(responseGet.IsSuccessStatusCode);
            Assert.Equal(petRequest.Name, updatedPet.Name);
            Assert.Equal(petRequest.Description, updatedPet.Description);
        }

        [Fact]
        public async Task PutPet_ExceptionAsync()
        {
            var petRequest = new PetRequest
            {
                PartnerId = 6,
                Name = "",
                Description = "Needs update",  
                Castrated = true,              
                Vaccinated = false,            
                Age = -1,                      
                Status = PetStatus.Available   
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            int petId = 1;

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/v1/pets?petId={petId}", petRequest, options);

            var body = await response.Content.ReadAsStringAsync();
            var exceptionResponse = JsonSerializer.Deserialize<ExceptionResponse>(body);

            Assert.False(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.UnprocessableEntity);

            Assert.NotNull(exceptionResponse);
            Assert.Equal(StatusProcess.Failure, exceptionResponse.Status);
            Assert.NotEmpty(exceptionResponse.Description);
        }

        [Fact]
        public async Task PutPetStatus_SuccessAsync()
        {
            int petId = 1;
            PetStatus newStatus = PetStatus.AdoptOng;

            var response = await _httpClient.PatchAsync($"api/v1/pets/{petId}/status",
                new StringContent(JsonSerializer.Serialize(newStatus), Encoding.UTF8, "application/json"));

            var body = await response.Content.ReadAsStringAsync();
            var customResponse = JsonSerializer.Deserialize<CustomResponse<PetStatus>>(body);

            Assert.True(response.IsSuccessStatusCode);

            Assert.NotNull(customResponse);
            Assert.Equal(StatusProcess.Success, customResponse.Status);
            Assert.Equal("Processado com Sucesso", customResponse.Description);
        }

        [Fact]
        public async Task PutPetStatus_ExceptionAsync()
        {
            int petId = 9999;
            PetStatus newStatus = PetStatus.AdoptOng;

            var response = await _httpClient.PatchAsync($"api/v1/pets/{petId}/status",
                new StringContent(JsonSerializer.Serialize(newStatus), Encoding.UTF8, "application/json"));

            var body = await response.Content.ReadAsStringAsync();
            var exceptionResponse = JsonSerializer.Deserialize<ExceptionResponse>(body);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Erro ao Processar", exceptionResponse.Description);
        }

        [Fact]
        public async Task DeletePet_SuccessAsync()
        {
            int petId = 2; 

            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/v1/pets/{petId}");
            
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeletePet_ExceptionAsync()
        {
            int petId = 9999;

            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/v1/pets/{petId}");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnprocessableContent, response.StatusCode);
        }
    }
}