namespace ListaDeComprasWeb.WebApp.ModuloItemLista.Aplicacao;

public record ListarItensDaListaDto(
    string Id,
    string IdLista,
    string NomeLista,
    string NomeProduto,
    string NomeCategoria,
    decimal Quantidade,
    decimal PrecoAproximado,
    decimal ValorTotal
);

public record DadosProdutoDto(
    string Id,
    string Nome,
    string NomeCategoria,
    decimal PrecoAproximado
);

public record CadastrarItemListaDto(
    string ListaId,
    string ProdutoId,
    decimal Quantidade
);

public record DadosCadastroItemListaDto(
    string IdLista,
    string NomeLista,
    List<DadosProdutoDto> Produtos
);

public record DetalhesItemDaListaDto(
    string Id,
    string IdLista,
    string NomeLista,
    string ProdutoId,
    string NomeProduto,
    string NomeCategoria,
    decimal Quantidade,
    decimal PrecoAproximado,
    decimal ValorTotal
);
