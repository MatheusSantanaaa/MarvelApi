using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using System.Net;

namespace MarvelApi.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();
        protected string LocalError = string.Empty;

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidarOperacao())
            {
                return Ok(new
                {
                    code = (int)HttpStatusCode.OK,
                    success = true,
                    localError = LocalError,
                    errors = Errors.ToArray()
                }); ;
            }

            return BadRequest(new
            {
                code = (int)HttpStatusCode.BadRequest,
                success = false,
                localError = LocalError,
                errors = Errors.ToArray()
            });
        }

        protected bool ValidarOperacao()
        {
            return !Errors.Any();
        }

        protected void AdicionarMensagemErro(string error)
        {
            Errors.Add(error);
            LocalError = "Erro interno";
        }

        protected void AdicionarMensagemErro(string error, string localError)
        {
            Errors.Add(error);
            LocalError = localError;
        }

        protected void LimparErros()
        {
            Errors.Clear();
            LocalError = string.Empty;
        }
    }
}
