using MarvelApi.Extensions;
using MarvelApi.Interfaces;
using MarvelApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;
using System.Net;

namespace MarvelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarvelController : MainController
    {
        private readonly IMarvelAppService _marvelAppService;

        public MarvelController(IMarvelAppService marvelAppService)
        {
            _marvelAppService = marvelAppService;
        }

        [HttpPost("gravar/personagens")]
        public IActionResult GravarPersonagens([FromBody] CaracteresMarvelViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                return CustomResponse(_marvelAppService.GravarPersonagens(viewModel));
            }
            catch (SystemContextException ex)
            {
                AdicionarMensagemErro(ex.Error, ex.LocalErro);
                return CustomResponse();
            }
            catch (Exception ex)
            {
                AdicionarMensagemErro(ex.Message);
                return CustomResponse();
            }
        }
    }
}
