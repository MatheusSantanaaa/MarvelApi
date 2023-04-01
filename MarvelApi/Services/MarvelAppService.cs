using MarvelApi.Extensions;
using MarvelApi.Interfaces;
using MarvelApi.ViewModels;
using Newtonsoft.Json;

namespace MarvelApi.Services
{
    public class MarvelAppService : IMarvelAppService
    {
        private readonly IConsumoApiMarvelClient _consumoApiMarvel;
        private readonly ILogger<ConsumoApiMarvelClient> _logger;

        public MarvelAppService(IConsumoApiMarvelClient consumoApiMarvel, ILogger<ConsumoApiMarvelClient> logger)
        {
            _consumoApiMarvel = consumoApiMarvel;
            _logger = logger;
        }

        public bool GravarPersonagens(CaracteresMarvelViewModel viewModel)
        {
            var resultadoApi = _consumoApiMarvel.AcessarMetodoCharactersApiMarvel(viewModel.ChavePublicaMarvel, viewModel.ChavePrivadaMarvel);

            var jsonMarvel = JsonConvert.DeserializeObject<JsonDeRetornoMarvelVielModel>(resultadoApi);

            if (jsonMarvel?.code != "200")
            {
                _logger.LogError(jsonMarvel?.message, "Erro de acesso a API da Marvel");
                throw new SystemContextException(jsonMarvel?.message, "Erro de acesso a API da Marvel");
            }

            GravarJsonEmPastaDiretorio(jsonMarvel.data.results);

            return true;
        }

        private void GravarJsonEmPastaDiretorio(List<ReultadosMarvelViewModel> results)
        {
            string nomeDaPasta = "Arquivos";
            string caminhoPasta = Path.Combine(System.Environment.CurrentDirectory.ToString(), nomeDaPasta);

            if(!Directory.Exists(caminhoPasta)) Directory.CreateDirectory(caminhoPasta);

            try
            {
                GravarJsonArquivoTxt(caminhoPasta, results);
            }
            catch (Exception ex)
            {
                _logger.LogError("Não foi possivel salvar json na pasta Arquivos: " + ex.Message);
                throw new Exception("Não foi possivel salvar json na pasta Arquivos: " + ex.Message);
            }
        }

        private static void GravarJsonArquivoTxt(string caminhoPasta, List<ReultadosMarvelViewModel> results)
        {
            string jsonText = JsonConvert.SerializeObject(results, Formatting.Indented);

            string nomeArquivo = "personagensmarvel.txt";
            string caminhoArquivo = Path.Combine(caminhoPasta, nomeArquivo);

            if (!File.Exists(caminhoArquivo))
            {
                // Cria o arquivo se ele não existir
                using StreamWriter sw = File.CreateText(caminhoArquivo);
                sw.WriteLine(jsonText);
            }
            else
            {
                // Limpa o que há dentro do arquivo e grava novo json
                using StreamWriter sw = new StreamWriter(caminhoArquivo, false);
                sw.WriteLine(jsonText);
            }
        }
    }
}
