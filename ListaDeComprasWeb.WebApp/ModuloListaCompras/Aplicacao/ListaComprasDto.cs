namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;

public record ListarListasDeComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    string Status
);
