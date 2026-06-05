using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Apresentacao;

public class ListaComprasProfile : Profile
{
    public ListaComprasProfile()
    {
        CreateMap<ListarListasDeComprasDto, ListarListasDeComprasViewModel>();
        CreateMap<CadastrarListaDeComprasViewModel, CadastrarListaDeCompraDto>();
        CreateMap<EditarListaDeComprasViewModel, EditarListaDeComprasDto>();

        CreateMap<DetalhesListaDeComprasDto, EditarListaDeComprasViewModel>();

    }
}
