using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;
using System.Text.Json.Serialization;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;

public class ListaCompras : EntidadeBase<ListaCompras>
{
    [JsonInclude]
    public string Nome { get; private set; }

    [JsonInclude]
    public DateTime DataCriacao { get; private set; }

    [JsonInclude]
    public StatusListaCompras Status { get; private set; }

    public ListaCompras()
    {
    }

    public ListaCompras(string nome)
    {
        Nome = nome;
        DataCriacao = DateTime.Now;

        Abrir();
    }

    public void Abrir()
    {
        Status = StatusListaCompras.Aberta;
    }

    public void Concluir()
    {
        Status = StatusListaCompras.Concluida;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        return erros;
    }

    public override void AtualizarDados(ListaCompras listaAtualizada)
    {
        Nome = listaAtualizada.Nome;
    }
}
