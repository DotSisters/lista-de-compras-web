using ListaDeComprasWeb.WebApp.Compartilhado.Aplicacao;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao;
using ListaDeComprasWeb.WebApp.Compartilhado.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraRepositories();

builder.Services.AddApplicationServices();

builder.Services.AddPresentation();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();