using FluentResults;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;

public class ServicoListaCompras
{
    private readonly IRepositorioListaCompras repositorioListaCompras;

    public ServicoListaCompras(IRepositorioListaCompras repositorioListaCompras)
    {
        this.repositorioListaCompras = repositorioListaCompras;
    }

    public List<ListarListasDeComprasDto> SelecionarTodos()
    {
        List<ListaCompras> categorias = repositorioListaCompras.SelecionarTodos();

        return categorias
            .Select(l => new ListarListasDeComprasDto(l.Id, l.Nome, l.DataCriacao, l.Status.ToString()))
            .ToList();
    }

    public Result Cadastrar(CadastrarListaDeCompraDto dto)
    {
        ListaCompras novaListaDeCompras = new ListaCompras(dto.Nome);

        repositorioListaCompras.Cadastrar(novaListaDeCompras);

        List<string> erros = novaListaDeCompras.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        return Result.Ok();
    }

    public Result Editar(EditarListaDeComprasDto dto)
    {

        ListaCompras listaAtualizada = new ListaCompras(dto.Nome)
        {
            Status = dto.Status
        };

        List<string> erros = listaAtualizada.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        bool conseguiuEditar = repositorioListaCompras.Editar(dto.Id, listaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Lista de compras não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");

        bool conseguiuExcluir = repositorioListaCompras.Excluir(lista);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível excluir a categoria.");

        return Result.Ok();
    }

    public Result<DetalhesListaDeComprasDto> SelecionarPorId(string id)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");

        return Result.Ok(new DetalhesListaDeComprasDto(lista.Id, lista.Nome, lista.DataCriacao,
            lista.Status));
    }
}
