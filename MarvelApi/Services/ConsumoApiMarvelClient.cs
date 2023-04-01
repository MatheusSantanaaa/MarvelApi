using MarvelApi.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace MarvelApi.Services
{
    public class ConsumoApiMarvelClient : IConsumoApiMarvelClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConsumoApiMarvelClient> _logger;

        public ConsumoApiMarvelClient(IConfiguration configuration,
                                ILogger<ConsumoApiMarvelClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string AcessarMetodoCharactersApiMarvel(string publicKey, string privateKey)
        {
            var timestamp = DateTime.UtcNow.Ticks.ToString();
            var hash = GerarMd5Hash($"{timestamp}{privateKey}{publicKey}");

            try
            {
                var baseUrl = _configuration.GetSection("API.MARVEL").Value + "characters?";

                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.GET);
                request.AddParameter("apikey", publicKey);
                request.AddParameter("ts", timestamp);
                request.AddParameter("hash", hash);

                var response = client.Execute(request);

                return response.Content;
            }
            catch (Exception ex)
            {
                _logger.LogError("Não foi possivel acessar API da Marvel: " + ex.Message);
                throw new Exception("Não foi possivel acessar API da Marvel: " + ex.Message);
            }
        }

        public string GerarMd5Hash(string input)
        {
            try
            {
                using var md5 = System.Security.Cryptography.MD5.Create();
                var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);
                var sb = new System.Text.StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao gerar Md5Hash: " + ex.Message);
                throw new Exception("Erro ao gerar Md5Hash: " + ex.Message);
            }
        }
    }
}
