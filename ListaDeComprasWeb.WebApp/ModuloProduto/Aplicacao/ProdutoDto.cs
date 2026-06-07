namespace ListaDeComprasWeb.WebApp.ModuloProduto.Aplicacao;

public record ListarProdutosDto(
    string Id,
    string Nome,
    string UnidadeMedida,
    decimal PrecoAproximado,
    string CategoriaNome
);

public record CadastrarProdutoDto(
    string Nome,
    string UnidadeMedida,
    decimal PrecoAproximado,
    string CategoriaNome
);

public record EditarProdutoDto(
    string Id,
    string Nome,
    string UnidadeMedida,
    decimal PrecoAproximado,
    string CategoriaNome
);

public record DetalhesProdutoDto(
    string Id,
    string Nome,
    string UnidadeMedida,
    decimal PrecoAproximado,
    string CategoriaNome
);