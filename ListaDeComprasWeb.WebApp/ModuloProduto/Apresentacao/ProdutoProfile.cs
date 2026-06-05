using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Aplicacao;
using ListaDeComprasWeb.WebApp.ModuloProduto.Aplicacao;

namespace ListaDeComprasWeb.WebApp.ModuloProduto.Apresentacao;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ListarProdutosDto, ListarProdutosViewModel>();
        CreateMap<ListarCategoriasDto, OpcaoCategoriaViewModel>();

        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();

        CreateMap<DetalhesProdutoDto, EditarProdutoViewModel>();
        CreateMap<DetalhesProdutoDto, ExcluirProdutoViewModel>();

    }
}