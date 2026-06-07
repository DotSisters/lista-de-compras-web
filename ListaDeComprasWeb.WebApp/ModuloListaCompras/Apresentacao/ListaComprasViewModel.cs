using System.ComponentModel.DataAnnotations;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Apresentacao;

public record ListarListasDeComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    string Status
);

public record CadastrarListaDeComprasViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome
);

public record EditarListaDeComprasViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    StatusListaCompras Status
);

public record ExcluirListaDeComprasViewModel(
    string Id,
    string Nome
);
