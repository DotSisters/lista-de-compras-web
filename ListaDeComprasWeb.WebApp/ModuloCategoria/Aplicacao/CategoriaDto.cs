using ListaDeComprasWeb.WebApp.ModuloCategoria.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloCategoria.Aplicacao;

public record ListarCategoriasDto(
    string Id,
    string Nome,
    CorCategoria Cor
);

public record CadastrarCategoriaDto(
    string Nome,
    CorCategoria Cor
);

public record EditarCategoriaDto(
    string Id,
    string Nome,
    CorCategoria Cor
);

public record DetalhesCategoriaDto(
    string Id,
    string Nome,
    CorCategoria Cor
);
