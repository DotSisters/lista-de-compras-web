using AutoMapper;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Aplicacao;
using ListaDeComprasWeb.WebApp.ModuloProduto.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApp.ModuloProduto.Apresentacao;

public class ProdutoController(
    ServicoProduto servicoProduto,
    ServicoCategoria servicoCategoria,
    IMapper mapeador
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutosDto> dtos = servicoProduto.SelecionarTodos();

        List<ListarProdutosViewModel> listarVms = mapeador.Map<List<ListarProdutosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel(
            string.Empty,
            string.Empty,
            0m,
            string.Empty,
            SelecionarCategorias()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Categorias = SelecionarCategorias() });

        CadastrarProdutoDto dto = mapeador.Map<CadastrarProdutoDto>(cadastrarVm);

        Result resultado = servicoProduto.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Categorias = SelecionarCategorias() });
        }

        return RedirectToAction(nameof(Listar));
    }


    [HttpGet]
    public ActionResult Editar(string id)
    {
        Result<DetalhesProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarProdutoViewModel editarVm =
            mapeador.Map<EditarProdutoViewModel>(resultado.Value) with { Categorias = SelecionarCategorias() };

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarProdutoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with { Categorias = SelecionarCategorias() });

        EditarProdutoDto dto = mapeador.Map<EditarProdutoDto>(editarVm);

        Result resultado = servicoProduto.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm with { Categorias = SelecionarCategorias() });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<DetalhesProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirProdutoViewModel excluirVm =
            mapeador.Map<ExcluirProdutoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirProdutoViewModel excluirVm)
    {
        Result resultado = servicoProduto.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoCategoriaViewModel> SelecionarCategorias()
    {
        List<ListarCategoriasDto> dtos = servicoCategoria.SelecionarTodos();

        return mapeador.Map<List<OpcaoCategoriaViewModel>>(dtos);
    }
}