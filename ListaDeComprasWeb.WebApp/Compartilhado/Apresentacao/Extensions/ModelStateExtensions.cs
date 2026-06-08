using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;

public static class ModelStateExtensions
{
    public static void AddModelError(this ModelStateDictionary modelState, ResultBase result)
    {
        foreach (IError erro in result.Errors)
        {
            string campo = string.Empty;

            if (erro.Metadata.TryGetValue("Campo", out object? valorCampo) && valorCampo is string nomeCampo)
                campo = nomeCampo;

            modelState.AddModelError(campo, erro.Message);
        }
    }
}
