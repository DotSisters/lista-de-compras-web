using ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;

public record ListarListasDeComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    string Status,
    int QuantidadeDeItens,
    decimal TotalEstimado
);

public record CadastrarListaDeCompraDto(
    string Nome
);

public record EditarListaDeComprasDto(
    string Id,
    string Nome,
    StatusListaCompras Status
);

public record DetalhesListaDeComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status
);
