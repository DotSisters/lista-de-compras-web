using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloItemLista.Aplicacao;

namespace ListaDeComprasWeb.WebApp.ModuloItemLista.Apresentacao;

public class ItemListaProfile : Profile
{
    public ItemListaProfile()
    {
        CreateMap<ListarItensDaListaDto, ListarItensDaListaViewModel>();
        CreateMap<CadastrarItemDaListaViewModel, CadastrarItemListaDto>();
        CreateMap<DadosProdutoDto, DadosProdutoViewModel>();
        CreateMap<DadosCadastroItemListaDto, DadosCadastroItemListaViewModel>();
        CreateMap<DetalhesItemDaListaDto, ExcluirItemDaListaViewModel>();
    }
}
