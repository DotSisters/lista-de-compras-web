using FluentResults;
using ListaDeComprasWeb.WebApp.ModuloItemLista.Dominio;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;
using ListaDeComprasWeb.WebApp.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloItemLista.Aplicacao;

public class ServicoItemLista
{

    private readonly IRepositorioItemLista repositorioItemLista;
    private readonly IRepositorioListaCompras repositorioListaCompras;
    private readonly IRepositorioProduto repositorioProduto;

    public ServicoItemLista(
        IRepositorioItemLista repositorioItemLista,
        IRepositorioListaCompras repositorioListaCompras,
        IRepositorioProduto repositorioProduto
    )
    {
        this.repositorioItemLista = repositorioItemLista;
        this.repositorioListaCompras = repositorioListaCompras;
        this.repositorioProduto = repositorioProduto;
    }

    public Result<List<ListarItensDaListaDto>> SelecionarPorLista(string listaId)
    {
        ListaCompras? listaCompra = repositorioListaCompras.SelecionarPorId(listaId);

        if (listaCompra == null)
            return Result.Fail("Lista de compras não encontrada.");

        List<ListarItensDaListaDto> itens = repositorioItemLista
            .Filtrar(i => i.ListaCompras.Id == listaId)
            .Select(i => new ListarItensDaListaDto(
                i.Id,
                i.ListaCompras.Id,
                i.ListaCompras.Nome,
                i.Produto.Nome,
                i.Produto.Categoria.Nome,
                i.Quantidade,
                i.Produto.PrecoAproximado,
                i.CalcularValorTotal()
            ))
            .ToList();

        return Result.Ok(itens);
    }

    public Result Cadastrar(CadastrarItemListaDto dto)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(dto.ListaId);

        Produto? produto = repositorioProduto.SelecionarPorId(dto.ProdutoId);

        if (lista == null)
            return Result.Fail("A lista de compras foi não encontrada.");

        if (produto == null)
            return Result.Fail("O produto não encontrado.");

        bool produtoExiste = repositorioItemLista
            .Filtrar(i => i.ListaCompras.Id == dto.ListaId && i.Produto.Id == dto.ProdutoId)
            .Any();

        if (produtoExiste)
            return Falha(nameof(dto.ProdutoId), "Este produto já foi adicionado na lista.");

        ItemLista novoItem = new ItemLista(lista, produto, dto.Quantidade);

        List<string> erros = novoItem.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioItemLista.Cadastrar(novoItem);

        return Result.Ok();
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }

    public Result<DadosCadastroItemListaDto> DadosCadastrais(string listaId)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(listaId);

        if (lista == null)
            return Result.Fail("A Lista de compras não foi encontrada.");

        List<DadosProdutoDto> produtos = repositorioProduto
           .SelecionarTodos()
           .Select(p => new DadosProdutoDto(
               p.Id,
               p.Nome,
               p.Categoria.Nome,
               p.PrecoAproximado
           ))
           .ToList();

        DadosCadastroItemListaDto dto = new DadosCadastroItemListaDto(
            lista.Id,
            lista.Nome,
            produtos
        );

        return Result.Ok(dto);
    }
}
