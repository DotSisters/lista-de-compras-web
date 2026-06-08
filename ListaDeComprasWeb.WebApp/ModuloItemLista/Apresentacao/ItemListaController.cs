using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApp.ModuloItemLista.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApp.ModuloItemLista.Apresentacao;

public class ItemListaController(
    ServicoItemLista servicoItemLista,
    IMapper mapeador
) : Controller
{

    [HttpGet]
    public ActionResult Listar(string listaId)
    {
        Result<List<ListarItensDaListaDto>> resultado = servicoItemLista.SelecionarPorLista(listaId);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction("Listar", "ListaCompras");
        }

        List<ListarItensDaListaViewModel> listarVms =
            mapeador.Map<List<ListarItensDaListaViewModel>>(resultado.Value);

        ViewBag.IdLista = listaId;

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar(string listaId)
    {
        Result<DadosCadastroItemListaDto> resultado = servicoItemLista.DadosCadastrais(listaId);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction("Listar", "ListaCompras");
        }

        DadosCadastroItemListaViewModel dadosVm =
            mapeador.Map<DadosCadastroItemListaViewModel>(resultado.Value);

        ViewBag.DadosCadastro = dadosVm;


        CadastrarItemDaListaViewModel cadastrarVm = new CadastrarItemDaListaViewModel(
            listaId,
            string.Empty,
            1
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarItemDaListaViewModel cadastrarVm)
    {
        Result<DadosCadastroItemListaDto> resultadoDados =
            servicoItemLista.DadosCadastrais(cadastrarVm.ListaId);

        if (resultadoDados.IsFailed)
        {
            TempData.AddErrorMessage(resultadoDados);

            return RedirectToAction("Listar", "ListaCompras");
        }

        ViewBag.DadosCadastro = mapeador.Map<DadosCadastroItemListaViewModel>(resultadoDados.Value);

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarItemListaDto dto = mapeador.Map<CadastrarItemListaDto>(cadastrarVm);

        Result resultado = servicoItemLista.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar), new { listaId = cadastrarVm.ListaId });
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<DetalhesItemDaListaDto> resultado = servicoItemLista.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction("Listar", "ListaCompras");
        }

        ExcluirItemDaListaViewModel excluirVm =
            mapeador.Map<ExcluirItemDaListaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirItemDaListaViewModel excluirVm)
    {
        Result resultado = servicoItemLista.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar), new { listaId = excluirVm.IdLista });
        }

        return RedirectToAction(nameof(Listar), new { listaId = excluirVm.IdLista });
    }

}
