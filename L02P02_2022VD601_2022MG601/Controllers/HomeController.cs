using Firebase.Auth;
using Firebase.Storage;
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

        [HttpPost]
        public async Task<ActionResult> SubirArchivo(IFormFile archivo)
        {
            Stream archivoASubir = archivo.OpenReadStream();

            string email = "roxana.valencia@catolica.edu.sv";
            string clave = "123456";
            string ruta = "gs://lab2-7c32e.appspot.com";
            string api_key = "AIzaSyAMqDA1mxo6zkLX0D4kW1ty4Fr0dahNbko";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var autenticarFireBase = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();
            var tokenUser = autenticarFireBase.FirebaseToken;

            var tareaCargarArchivo = new FirebaseStorage(ruta,
                                                            new FirebaseStorageOptions
                                                            {
                                                                AuthTokenAsyncFactory = () => Task.FromResult(tokenUser),
                                                                ThrowOnCancel = true
                                                            }).Child("Archivos").Child(archivo.FileName).PutAsync(archivoASubir, cancellation.Token);

            var urlArchivoCargado = await tareaCargarArchivo;

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {

            var listaDeDepartamento = (from d in _context.departamentos
                                       select d).ToList();

            ViewData["listadoDeDepartamento"] = new SelectList(listaDeDepartamento, "id", "departamento");


            var listaDePuesto = (from p in _context.puestos
                                 select p).ToList();

            ViewData["listadoDePuesto"] = new SelectList(listaDePuesto, "id", "puesto");



            var listadoDeClientes = (from c in _context.clientes
                                     join p in _context.puestos on c.id_puesto equals p.id
                                     join d in _context.departamentos on c.id_departamento equals d.id
                                     select new
                                     {
                                         nombre = c.nombre,
                                         apellido = c.apellido,
                                         email = c.email,
                                         direccion = c.direccion,
                                         departamento = d.departamento,
                                         genero = c.genero,
                                         puesto = p.puesto
                                     }).ToList();
            ViewData["listadoDeClientes"] = listadoDeClientes;




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