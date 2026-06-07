using ListaDeComprasWeb.WebApp.ModuloCategoria.Aplicacao;
using ListaDeComprasWeb.WebApp.ModuloProduto.Aplicacao;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;
using ListaDeComprasWeb.WebApp.ModuloItemLista.Aplicacao;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoProduto>();
        services.AddScoped<ServicoListaCompras>();
        services.AddScoped<ServicoItemLista>();
    }
}
