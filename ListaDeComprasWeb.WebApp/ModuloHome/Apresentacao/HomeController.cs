using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApp.ModuloHome.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
