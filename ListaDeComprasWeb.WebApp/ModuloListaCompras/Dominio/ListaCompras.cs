using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Dominio;

public class ListaCompras : EntidadeBase<ListaCompras>
{
    public string Nome { get; private set; }
    public DateTime DataCriacao { get; private set; }
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
