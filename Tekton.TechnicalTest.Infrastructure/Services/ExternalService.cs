using Newtonsoft.Json;
using Tekton.TechnicalTest.Domain;
using Tekton.TechnicalTest.Shared.Abstractions;
using Tekton.TechnicalTest.Shared.Common.Exceptions;

namespace Tekton.TechnicalTest.Infrastructure.Services
{
    public class ExternalService(HttpClient HttpClient) : IExternalService
    {
        readonly HttpClient _httpClient = HttpClient;

        public async Task<ExternalServiceDto?> SearchProductAmountPercent(int productId)
        {
            //Move BasePath to Configuration
            var uri = $"https://6580c8bd3dfdd1b11c42177f.mockapi.io/api/v1/discounts/";

            var url = string.Concat(uri, productId);
            var result = await _httpClient.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExternalServiceDto>(json);
            }
            else
                throw new NotFoundException(nameof(ExternalServiceDto), productId);
        }
    }
}
