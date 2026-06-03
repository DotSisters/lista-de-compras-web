using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Infra;

public interface IRepositorio<T> where T : EntidadeBase<T>
{
    void Cadastrar(T entidade);
    bool Editar(string idSelecionado, T entidadeAtualizada);
    bool Excluir(T registro);
    T? SelecionarPorId(string idSelecionado);
    List<T> SelecionarTodos();
    List<T> Filtrar(Predicate<T> filtro);
}