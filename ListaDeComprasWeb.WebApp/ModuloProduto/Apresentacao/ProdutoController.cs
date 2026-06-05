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

    private List<OpcaoCategoriaViewModel> SelecionarCategorias()
    {
        List<ListarCategoriasDto> dtos = servicoCategoria.SelecionarTodos();

        return mapeador.Map<List<OpcaoCategoriaViewModel>>(dtos);
    }
}