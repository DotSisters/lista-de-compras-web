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
        ListaCompras novaListaDeCompras = new ListaCompras(
            dto.Nome
        );

        repositorioListaCompras.Cadastrar(novaListaDeCompras);

        return Result.Ok();
    }


    public Result Editar(EditarListaDeComprasDto dto)
    {

        ListaCompras listaAtualizada = new ListaCompras(dto.Nome);

        bool conseguiuEditar = repositorioListaCompras.Editar(dto.Id, listaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Lista de compras não encontrada.");

        return Result.Ok();
    }

    public Result<DetalhesListaDeComprasDto> SelecionarPorId(string id)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");

        return Result.Ok(new DetalhesListaDeComprasDto(lista.Id, lista.Nome));
    }
}
