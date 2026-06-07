
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ListaDeComprasWeb.WebApp.ModuloProduto.Apresentacao;

public record OpcaoCategoriaViewModel(
    string Id,
    string Nome
);

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    string UnidadeMedida,
    decimal PrecoAproximado,
    string CategoriaNome
);

public record CadastrarProdutoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"UnidadeMedida\" deve ser preenchido.")]
    [StringLength(7, ErrorMessage = "O campo \"UnidadeMedida\" deve conter no máximo 50 caracteres.")]
    string UnidadeMedida,

    [Required(ErrorMessage = "O campo \"PrecoAproximado\" deve ser preenchido.")]
    decimal PrecoAproximado,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    string CategoriaNome,

    [ValidateNever] List<OpcaoCategoriaViewModel> Categorias
);

public record EditarProdutoViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"UnidadeMedida\" deve ser preenchido.")]
    [StringLength(7, ErrorMessage = "O campo \"UnidadeMedida\" deve conter no máximo 50 caracteres.")]
    string UnidadeMedida,

    [Required(ErrorMessage = "O campo \"PrecoAproximado\" deve ser preenchido.")]
    decimal PrecoAproximado,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    string CategoriaNome,

    [ValidateNever]
    List<OpcaoCategoriaViewModel>? Categorias = null
);

public record ExcluirProdutoViewModel(
    string Id,
    string Nome,
    string UnidadeMedida,
    decimal PrecoAproximado,
    string CategoriaNome
);