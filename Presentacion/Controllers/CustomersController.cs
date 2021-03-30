using Base_Comun;
using Entidades.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Collections.Generic;

namespace Presentacion.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class CustomersController : Controller
    {

        private readonly IApiService _apiService;
        public CustomersController(IApiService apiService)
        {
            _apiService = apiService;

            if (!_apiService.IsAutenticate())
                _apiService.Autenticar();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            return Json(_apiService.Listar());
        }

        public IActionResult DescargarReporte()
        {

            ExcelHelper excelHelper = new ExcelHelper();
            excelHelper.AgregarHoja();
            List<CustomersVModel> lista = _apiService.Listar();

            excelHelper.AgregaEncabezados(new List<string>() { "IdCliente", "FechaRegistroEmpresa", "RazonSocial", "RFC", "Sucursal", "IdEmpleado", " Nombre", "Paterno", "Materno", "IdViaje" }, 10);

            foreach (var fila in lista)
            {
                excelHelper.AgregarFila();

                excelHelper.AgregarCelda((int)fila.IdCliente);
                excelHelper.AgregarCelda(fila.FechaRegistroEmpresa.ToString());
                excelHelper.AgregarCelda(fila.RazonSocial);
                excelHelper.AgregarCelda(fila.RFC);
                excelHelper.AgregarCelda(fila.Sucursal);
                excelHelper.AgregarCelda(fila.IdEmpleado);
                excelHelper.AgregarCelda(fila.Nombre);
                excelHelper.AgregarCelda(fila.Paterno);
                excelHelper.AgregarCelda(fila.Materno);
                excelHelper.AgregarCelda(fila.IdViaje);
            }

            excelHelper.AjustarTamanioColumnasHojaActual();

            return File(excelHelper.ObtenerArchivo(), "application/octet-stream", "ReporteClientes.xlsx");

        }
    }
}