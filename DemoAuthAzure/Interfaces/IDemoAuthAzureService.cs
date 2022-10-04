using DemoAuthAzure.Models;

namespace DemoAuthAzure.Interfaces
{
    public interface IDemoAuthAzureService
    {
        Task<List<WeatherForecast>> Obtenir();
    }
}
