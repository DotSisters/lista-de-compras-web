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

    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        Categoria? categoriaSelecionada = repositorioCategoria.SelecionarPorId(dto.CategoriaNome);

        if (categoriaSelecionada == null)
        {
            return Result.Fail(
                new Error("Selecione uma categoria válida.")
                    .WithMetadata("Campo", nameof(dto.CategoriaNome))
            );
        }

        Produto novoProduto = new Produto(
            dto.Nome,
            dto.UnidadeMedida,
            dto.PrecoAproximado,
            categoriaSelecionada!
        );

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok();
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok(new DetalhesProdutoDto(
            produto.Id,
            produto.Nome,
            produto.UnidadeMedida,
            produto.PrecoAproximado,
            produto.Categoria.Nome
        ));
    }

    public Result Editar(EditarProdutoDto dto)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(dto.Id);

        if (produto == null)
            return Result.Fail("Produto não encontrado.");

        Categoria? categoriaSelecionada = repositorioCategoria.SelecionarPorId(dto.CategoriaNome);

        if (categoriaSelecionada == null)
        {
            return Result.Fail(
                new Error("Selecione uma categoria válida.")
                    .WithMetadata("Campo", nameof(dto.CategoriaNome))
            );
        }

        if (ExisteProdutoMesmoNome(dto.Nome, dto.Id, categoriaSelecionada.Id))
        {
            return Result.Fail(
                new Error("Já existe um produto com esse nome nesta categoria.")
                    .WithMetadata("Campo", nameof(dto.Nome))
            );
        }

        Produto produtoAtualizado = new Produto(
            dto.Nome,
            dto.UnidadeMedida,
            dto.PrecoAproximado,
            categoriaSelecionada
        );

        List<string> erros = produtoAtualizado.Validar();
        if (erros.Any())
            return Result.Fail(erros);

        repositorioProduto.Editar(dto.Id, produtoAtualizado);

        return Result.Ok();
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

    private bool ExisteProdutoMesmoNome(string nome, string idIgnorado, string categoriaId)
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return produtos.Any(p =>
            p.Id != idIgnorado &&
            p.Categoria.Id == categoriaId &&
            string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase)
        );
    }

}