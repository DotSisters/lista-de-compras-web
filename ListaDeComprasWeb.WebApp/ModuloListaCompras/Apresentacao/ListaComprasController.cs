using ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentResults;

namespace ListaDeComprasWeb.WebApp.ModuloListaCompras.Apresentacao;

public class ListaComprasController(ServicoListaCompras servicoListaCompras, IMapper mapeador) : Controller
{

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarListasDeComprasDto> dtos = servicoListaCompras.SelecionarTodos();

        List<ListarListasDeComprasViewModel> listarVms = mapeador.Map<List<ListarListasDeComprasViewModel>>(dtos);

        return View(listarVms);
    }


}
