using L02P02_2022VD601_2022MG601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace L02P02_2022VD601_2022MG601.Controllers
{
	public class HomeController : Controller
	{

        private readonly usuariosDBContex _context;

		private readonly ILogger<HomeController> _logger;

		public HomeController(usuariosDBContex context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

  //      private readonly ILogger<HomeController> _logger;

		//public HomeController(ILogger<HomeController> logger)
		//{
		//	_logger = logger;
		//}

		public IActionResult Index()
		{

			var listaDeDepartamento = (from d in _context.departamentos
								   select d).ToList();

			ViewData["listadoDeDepartamento"] = new SelectList(listaDeDepartamento, "id", "departamento");

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
