using ListaDeComprasWeb.WebApp.ModuloProduto.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ListaDeComprasWeb.WebApp.ModuloProduto.Apresentacao;

public class ProdutoController(ServicoProduto servicoProduto, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutosDto> dtos = servicoProduto.SelecionarTodos();

        List<ListarProdutosViewModel> listarVms = mapeador.Map<List<ListarProdutosViewModel>>(dtos);

        return View(listarVms);
    }
}