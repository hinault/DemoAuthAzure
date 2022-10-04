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

       
      
        public DemoAuthAzureServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
          
        }

        public async Task<List<WeatherForecast>> Obtenir()
        {
            return await _httpClient.GetFromJsonAsync<List<WeatherForecast>>(_apiUrl);
        }

        
    }
}
