using DemoAuthAzure.Interfaces;
using DemoAuthAzure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace DemoAuthAzure.Services
{
    public class DemoAuthAzureServiceProxy : IDemoAuthAzureService
    {
        private readonly HttpClient _httpClient;

        private const string _apiUrl = "weatherforecast/";

        private readonly ITokenAcquisition _tokenAcquisition;

        private readonly IConfiguration _configuration;
       
      
        public DemoAuthAzureServiceProxy(HttpClient httpClient, ITokenAcquisition tokenAcquisition, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        public async Task<List<WeatherForecast>> Obtenir()
        {
            await PrepareAuthenticatedClient();
            return await _httpClient.GetFromJsonAsync<List<WeatherForecast>>(_apiUrl);
        }

        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new List<string>());
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Ajout de l'API Key dans l'entete HTTP
            _httpClient.DefaultRequestHeaders.Add("XApiKey", _configuration.GetValue<string>("API:XApiKey"));
        }

    }
}
