using FluentValidation.Results;
using Microsoft.Build.Tasks;

namespace MarvelApi.Extensions
{
    public class SystemContextException : Exception
    {
        public string Error { get; set; }
        public string? LocalErro { get; set; }

        public SystemContextException(string error, string? localErro = null)
        {
            Error = error;
            LocalErro = localErro;
        }
    }
}
