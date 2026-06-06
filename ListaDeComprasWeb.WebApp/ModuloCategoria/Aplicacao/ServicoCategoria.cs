using ListaDeComprasWeb.WebApp.ModuloCategoria.Dominio;
using FluentResults;
using ListaDeComprasWeb.WebApp.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IRepositorioProduto repositorioProduto;

    public ServicoCategoria(
        IRepositorioCategoria repositorioCategoria,
        IRepositorioProduto repositorioProduto
    )
    {
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioProduto = repositorioProduto;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome))
            return Falha("Nome", "Já existe uma categoria com esse nome.");

        Categoria novaCategoria = new Categoria(
            dto.Nome,
            dto.Cor
        );

        repositorioCategoria.Cadastrar(novaCategoria);

        return Result.Ok();
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome, dto.Id))
            return Falha("Nome", "Já existe uma categoria com esse nome.");

        Categoria categoriaAtualizada = new Categoria(dto.Nome, dto.Cor);

        bool conseguiuEditar = repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        // Verifica se existe pelo menos um produto vinculado à categoria
        if (repositorioProduto.SelecionarTodos()
            .Any(p => p.Categoria != null && p.Categoria.Id == id))
        {
            return Result.Fail("Esta categoria não pode ser excluída pois está relacionada a um produto.");
        }

        bool conseguiuExcluir = repositorioCategoria.Excluir(categoria);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível excluir a categoria.");

        return Result.Ok();
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        return categorias
            .Select(c => new ListarCategoriasDto(c.Id, c.Nome, c.Cor))
            .ToList();
    }

    public Result<DetalhesCategoriaDto> SelecionarPorId(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(new DetalhesCategoriaDto(categoria.Id, categoria.Nome, categoria.Cor));
    }

    private bool ExisteCategoriaComNome(string nome, string? idIgnorado = null)
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        foreach (Categoria c in categorias)
        {
            if (c.Id != idIgnorado && string.Equals(c.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}