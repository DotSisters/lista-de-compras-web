using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloCategoria.Dominio;

public sealed class Categoria : EntidadeBase<Categoria>
{
    public string Nome { get; set; } = string.Empty;
    public CorCategoria Cor { get; set; }

    public Categoria()
    {
    }

    public Categoria(string nome, CorCategoria cor)
    {
        Nome = nome;
        Cor = cor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length > 50)
            erros.Add("O campo \"Nome\" deve conter no máximo 50 caracteres.");

        return erros;
    }

    public override void AtualizarDados(Categoria entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Cor = entidadeAtualizada.Cor;
    }
}