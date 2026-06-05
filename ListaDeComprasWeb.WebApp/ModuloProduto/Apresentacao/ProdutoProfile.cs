using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloProduto.Aplicacao;

namespace ListaDeComprasWeb.WebApp.ModuloProduto.Apresentacao;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ListarProdutosDto, ListarProdutosViewModel>();
    }
}