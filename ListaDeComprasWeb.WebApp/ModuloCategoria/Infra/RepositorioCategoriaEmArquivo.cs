
using ListaDeComprasWeb.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloCategoria.Infra;

public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Categoria> CarregarRegistros()
    {
        return contexto.Categorias;
    }
}