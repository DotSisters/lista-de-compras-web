using ListaDeComprasWeb.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApp.ModuloItemLista.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloItemLista.Infra;

public class RepositorioItemListaEmArquivo
    : RepositorioBaseEmArquivo<ItemLista>, IRepositorioItemLista
{
    public RepositorioItemListaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ItemLista> CarregarRegistros()
    {
        return contexto.ItensDaLista;
    }
}
