
using Domain.DTOs;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OmdbApiClient : IOmdbApiClient
    {
        //private const string ApiKey = "7f9312cd"; // Your API key

        //private readonly HttpClient _httpClient;

        //public OmdbApiClient(HttpClient httpClient)
        //{
        //    _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        //    _httpClient.BaseAddress = new Uri("http://www.omdbapi.com/");
        //}
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public OmdbApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Read API key and base URL from appsettings.json
            _apiKey = configuration["OmdbApi:ApiKey"];
            _baseUrl = configuration["OmdbApi:BaseUrl"];

            if (string.IsNullOrWhiteSpace(_apiKey))
                throw new ArgumentNullException("OmdbApi:ApiKey is missing in appsettings.json");
            if (string.IsNullOrWhiteSpace(_baseUrl))
                throw new ArgumentNullException("OmdbApi:BaseUrl is missing in appsettings.json");

            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        public async Task<MovieSearchResultDto> SearchByTitleAsync(string title, int? page = null)
        {
            var requestUrl = $"?s={Uri.EscapeDataString(title)}&apikey={_apiKey}&page={page}";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MovieSearchResultDto>(json);
        }

        public async Task<MovieDetailsDto> GetMovieByIdAsync(string imdbId)
        {
            var requestUrl = $"?i={Uri.EscapeDataString(imdbId)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MovieDetailsDto>(json);
        }

        public async Task<MovieDetailsDto> GetMovieByTitleAsync(string title)
        {
            var requestUrl = $"?t={Uri.EscapeDataString(title)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MovieDetailsDto>(json);
        }
    }
}