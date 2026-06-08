using FluentResults;
using ListaDeComprasWeb.WebApp.ModuloItemLista.Dominio;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;

public class ServicoListaCompras
{
    private readonly IRepositorioListaCompras repositorioListaCompras;
    private readonly IRepositorioItemLista repositorioItemLista;

    public ServicoListaCompras(IRepositorioListaCompras repositorioListaCompras, IRepositorioItemLista repositorioItemLista)
    {
        this.repositorioListaCompras = repositorioListaCompras;
        this.repositorioItemLista = repositorioItemLista;
    }

    public List<ListarListasDeComprasDto> SelecionarTodos()
    {
        List<ListaCompras> listaCompras = repositorioListaCompras.SelecionarTodos();

        return listaCompras
            .Select(l =>
            {
                List<ItemLista> itensDaLista = repositorioItemLista.Filtrar(i => i.ListaCompras.Id == l.Id);
                int quantidadeDeItens = itensDaLista.Count;
                decimal totalEstimado = itensDaLista.Sum(i => i.CalcularValorTotal());

                return new ListarListasDeComprasDto(l.Id, l.Nome, l.DataCriacao, l.Status.ToString(), quantidadeDeItens, totalEstimado);
            })
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
            return Result.Fail("A Lista de compras não foi encontrada.");

        List<ItemLista> itensDaLista = repositorioItemLista.Filtrar(i => i.ListaCompras.Id == id);

        if (itensDaLista.Count > 0)
            return Result.Fail("Não é possível excluir uma lista enquanto houver itens vinculados a ela.");

        bool conseguiuExcluir = repositorioListaCompras.Excluir(lista);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível excluir a lista.");

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
