using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using ZaxHerbivoryTrainer.APP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ZaxHerbivoryTrainer.APP.Business
{
    public class ApiBusinessController 
    {


        private readonly IHttpClientFactory _httpClientFactory;

        public ApiBusinessController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<APIResponse<T>> BuildApiResponse<T>(HttpRequestMessage request, [Optional] string bearer )
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            if(!string.IsNullOrEmpty(bearer))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",bearer);
           
            try
            {
                
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false);

                var responseStream = await response.Content.ReadAsStreamAsync();
                var content = await JsonSerializer.DeserializeAsync<T>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    return new APIResponse<T>
                    {
                        Status = response.StatusCode,
                        Content = content,
                        Error = ex.Message
                    };
                }

                return new APIResponse<T>
                {
                    Status = response.StatusCode,
                    Error = "",
                    Content = content
                };

            }
            catch (Exception e)
            {
                return new APIResponse<T>
                {
                    Status = HttpStatusCode.Conflict,
                    Error = e.Message
                };
            }
        } 
    

    }
}