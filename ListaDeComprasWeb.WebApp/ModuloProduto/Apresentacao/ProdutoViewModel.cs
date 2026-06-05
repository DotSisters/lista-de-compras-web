
namespace ListaDeComprasWeb.WebApp.ModuloProduto.Apresentacao;

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    string UnidadeMedida,
    decimal PrecoAproximado,
    string CategoriaNome
);