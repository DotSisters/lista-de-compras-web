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
        ListaCompras novaCategoria = new ListaCompras(
            dto.Nome
        );

        repositorioListaCompras.Cadastrar(novaCategoria);

        return Result.Ok();
    }
}
