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

       
      
        public DemoAuthAzureServiceProxy(HttpClient httpClient, ITokenAcquisition tokenAcquisition)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
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

        }

    }
}
