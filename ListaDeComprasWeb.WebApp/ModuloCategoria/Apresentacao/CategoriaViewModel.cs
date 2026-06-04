using System.ComponentModel.DataAnnotations;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    string Id,
    string Nome,
    CorCategoria Cor
);

public record CadastrarCategoriaViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    CorCategoria Cor
);