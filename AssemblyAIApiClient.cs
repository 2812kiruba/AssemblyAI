using AssemblyAITranscriber.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AssemblyAITranscriber
{
 public class AssemblyAIApiClient
    {
        private readonly string _apiToken;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public AssemblyAIApiClient(string apiToken, string baseUrl)
        {
            _apiToken = apiToken;
            _baseUrl = baseUrl;
            _httpClient = new HttpClient() { BaseAddress = new Uri(_baseUrl) };
            _httpClient.DefaultRequestHeaders.Add("Authorization", _apiToken);
        }

        private async Task<TModel> SendRequestAsync<TModel>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TModel>(json);
        }
        public Task<UploadAudioResponse> UploadFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "v2/upload");
            request.Headers.Add("Transer-Encoding", "chunked");

            var fileReader = File.OpenRead(filePath);
            request.Content = new StreamContent(fileReader);

            return SendRequestAsync<UploadAudioResponse>(request);
        }
        public Task<TranscriptionResponse> SubmitAudioFileAsync(string audioUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "v2/transcript");
            var requestBody = JsonSerializer.Serialize(new TranscriptionRequest { AudioUrl = audioUrl });
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            return SendRequestAsync<TranscriptionResponse>(request);
        }

        public Task<TranscriptionResponse> GetTranscriptionAsync(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"v2/transcript/{id}");

            return SendRequestAsync<TranscriptionResponse>(request);
        }
    }
}
