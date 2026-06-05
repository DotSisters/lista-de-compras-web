using ListaDeComprasWeb.WebApp.ModuloListaCompras.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;

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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarListaDeComprasViewModel cadastrarVm = new CadastrarListaDeComprasViewModel(
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarListaDeComprasViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarListaDeCompraDto dto = mapeador.Map<CadastrarListaDeCompraDto>(cadastrarVm);

        Result resultado = servicoListaCompras.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Result<DetalhesListaDeComprasDto> resultado = servicoListaCompras.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesListaDeComprasDto dto = resultado.Value;

        EditarListaDeComprasViewModel editarVm = mapeador.Map<EditarListaDeComprasViewModel>(dto);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarListaDeComprasViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarListaDeComprasDto dto = mapeador.Map<EditarListaDeComprasDto>(editarVm);

        Result resultado = servicoListaCompras.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
