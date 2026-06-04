using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Aplicacao;

namespace ListaDeComprasWeb.WebApp.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<ListarCategoriasDto, ListarCategoriasViewModel>();
        CreateMap<CadastrarCategoriaViewModel, CadastrarCategoriaDto>();
    }
}