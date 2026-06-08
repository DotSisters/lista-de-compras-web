using System.ComponentModel.DataAnnotations;

namespace ListaDeComprasWeb.WebApp.ModuloItemLista.Apresentacao;

public record ListarItensDaListaViewModel(
    string Id,
    string IdLista,
    string NomeLista,
    string NomeProduto,
    string NomeCategoria,
    decimal Quantidade,
    decimal PrecoAproximado,
    decimal ValorTotal
);

public record CadastrarItemDaListaViewModel(
    string ListaId,

    [Required(ErrorMessage = "O campo \"Produto\" deve ser preenchido.")]
    string ProdutoId,

    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor maior que 0.")]
    decimal Quantidade
);

public record DadosProdutoViewModel(
    string Id,
    string Nome,
    string NomeCategoria,
    decimal PrecoAproximado
);

public record DadosCadastroItemListaViewModel(
    string IdLista,
    string NomeLista,
    List<DadosProdutoViewModel> Produtos
);

public record ExcluirItemListaViewModel(
    string Id,
    string IdLista,
    string NomeLista,
    string NomeProduto,
    string NomeCategoria,
    decimal Quantidade,
    decimal PrecoAproximado,
    decimal ValorTotal
);
