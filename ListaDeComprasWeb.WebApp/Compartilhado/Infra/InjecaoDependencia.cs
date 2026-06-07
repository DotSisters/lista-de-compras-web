using ListaDeComprasWeb.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Dominio;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Infra;
using ListaDeComprasWeb.WebApp.ModuloProduto.Dominio;
using ListaDeComprasWeb.WebApp.ModuloProduto.Infra;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Infra;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmArquivo>();

        services.AddScoped<IRepositorioListaCompras, RepositorioListaComprasEmArquivo>();

    }
}
