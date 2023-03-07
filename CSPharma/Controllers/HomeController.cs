using CSPharma.Models;
using DAL.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;


namespace CSPharma.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
       


        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Logout()
        {
            return View("Logout");
        }

        public  IActionResult Login(String CodEmpleado, String ClaveEmpleado,int nivelAcceso)
        {
            ViewBag.CodEmpleado = CodEmpleado;
            ViewBag.ClaveEmpleado = ClaveEmpleado;

            //Hacemos la conexion
            var connection = new NpgsqlConnection(_config.GetConnectionString("EFCConexion"));
            Console.WriteLine("ABRIENDO CONEXION");
            connection.Open();
            //Compruebo en base de datos si los datos son correctos
            NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"dlk_informacional\".\"dlk_cat_acc_empleados\" WHERE cod_empleado='{CodEmpleado}' AND clave_empleado='{ClaveEmpleado}' AND nivel_acceso_empleado='{nivelAcceso}' ", connection);
           
            if (ClaveEmpleado == null && CodEmpleado == null)
            {
                return View();
            }

            NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();

 
            
             //HttpContext.Session.SetInt32("UserRole", nivelAcceso);

            //Si tiene el rol 0 lo llevo al index.
            if (resultadoConsulta.HasRows && nivelAcceso==0)
            {
                ViewBag.nivelAcceso = nivelAcceso;
                return View("Index");
            }
            //Si tiene el rol 1 lo llevo al index.
            else if (resultadoConsulta.HasRows && nivelAcceso == 1)
            {
                ViewBag.nivelAcceso = nivelAcceso;
                return View("Index");
            }
            //Si su rol esta pendiente lo llevo a una pagina de espera.
            else if(resultadoConsulta.HasRows && nivelAcceso == 2)
            {
                return View("Pendiente");
            }
            else
            {
                ViewBag.ErrorSesion = "Error al iniciar sesión. Usuario , contraseña o rol son incorrectos.";
            }
            return View();
            Console.WriteLine("Cerrando conexion");
            connection.Close();            
        }

        public IActionResult Register(String CodEmpleado, String ClaveEmpleado,int nivelAcceso)
        {
            //Guardamos el codigo y la contraseña del empleado
            ViewBag.CodEmpleado = CodEmpleado;
            ViewBag.ClaveEmpleado = ClaveEmpleado;
            var connection = new NpgsqlConnection(_config.GetConnectionString("EFCConexion"));
            Console.WriteLine("ABRIENDO CONEXION");
            connection.Open();
            //Introducimos el nuevo usuario a base de datos
            NpgsqlCommand consulta = new NpgsqlCommand($"INSERT INTO \"dlk_informacional\".\"dlk_cat_acc_empleados\" (cod_empleado, clave_empleado,nivel_acceso_empleado) VALUES('{CodEmpleado}','{ClaveEmpleado}','{nivelAcceso}')", connection);
            if (ClaveEmpleado == null && CodEmpleado == null)
            {
                return View();
            }

            Console.WriteLine(ClaveEmpleado);
            bool mayuscula = false, minuscula = false, numero = false;
            if (ClaveEmpleado != null)
            {

                for (int i = 0; i < ClaveEmpleado.Length; i++)
                {
                    if (Char.IsUpper(ClaveEmpleado, i))
                    {
                        mayuscula = true;
                    }
                    else if (Char.IsLower(ClaveEmpleado, i))
                    {
                        minuscula = true;
                    }
                    else if (Char.IsDigit(ClaveEmpleado, i))
                    {
                        numero = true;
                    }


                    if (mayuscula && minuscula && numero && ClaveEmpleado.Length >= 7)
                    {

                        Console.WriteLine("La contraseña cumple los requisitos minimos");
                        NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();
                        ViewBag.RegistroCreado = "Registro creado con éxito";
                        return View("Register");
                    }
                }
            }
            else{
                Console.WriteLine("No puede ser nulo");
                return View();
            }
            if (!mayuscula){
                ViewBag.NotLower += "\nLa contraseña tiene que tener mínimo una mayúscula";
            }
          
            if (!minuscula){
                ViewBag.NotLower += "\nLa contraseña tiene que tener mínimo una minúscula";
            }
           
            if (ClaveEmpleado.Length < 7){
                ViewBag.NotLower += "\nLa contraseña tiene que tener mínimo 7 números";
            }
           
            if (!numero){
                ViewBag.NotLower += "\nLa contraseña tiene que tener mínimo un número";
            }
            return View("Register");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}