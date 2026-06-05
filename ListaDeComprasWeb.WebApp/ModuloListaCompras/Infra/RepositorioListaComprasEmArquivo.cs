using System;
using ListaDeComprasWeb.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Infra;

public class RepositorioListaComprasEmArquivo : RepositorioBaseEmArquivo<ListaCompras>, IRepositorioListaCompras
{
    public RepositorioListaComprasEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<ListaCompras> CarregarRegistros()
    {
        return contexto.ListasDeCompras;
    }
}
