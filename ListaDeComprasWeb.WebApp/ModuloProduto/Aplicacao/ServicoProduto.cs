using FluentResults;
using ListaDeComprasWeb.WebApp.ModuloProduto.Dominio;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloProduto.Aplicacao;

public class ServicoProduto
{
    private readonly IRepositorioProduto repositorioProduto;
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoProduto(
        IRepositorioProduto repositorioProduto,
        IRepositorioCategoria repositorioCategoria
    )
    {
        this.repositorioProduto = repositorioProduto;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Result.Fail("Produto não encontrada.");

        return Result.Ok(new DetalhesProdutoDto(
            produto.Id,
            produto.Nome,
            produto.UnidadeMedida,
            produto.PrecoAproximado,
            produto.Categoria.Nome
            ));
    }

    public List<ListarProdutosDto> SelecionarTodos()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return produtos
            .Select(p => new ListarProdutosDto(
                p.Id,
                p.Nome,
                p.UnidadeMedida,
                p.PrecoAproximado,
                p.Categoria.Nome
            ))
            .ToList();
    }

}