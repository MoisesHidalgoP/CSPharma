using CSPharma.Models;
using DAL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;

namespace CSPharma.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(String CodEmpleado, String ClaveEmpleado)
        {
            ViewBag.CodEmpleado = CodEmpleado;
            ViewBag.ClaveEmpleado = ClaveEmpleado;

            //Hacemos la conexion
            var connection = new NpgsqlConnection("Host=localhost;Port=5432;Pooling=true;Database=cspharma_informacional;UserId=postgres;Password=doshermanas1;");
            Console.WriteLine("ABRIENDO CONEXION");
            connection.Open();

            NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"dlk_informacional\".\"dlk_cat_acc_empleados\" WHERE cod_empleado='{CodEmpleado}' AND clave_empleado='{ClaveEmpleado}'", connection);
           
            if (ClaveEmpleado == null && CodEmpleado == null)
            {
                return View();
            }

            NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();

            if (resultadoConsulta.HasRows)
            {
              
                ViewBag.SessionIniciada = "Inicio de sesión con exito";
               
                return View("Index");

            }
            else
            {
                
                ViewBag.ErrorSesion = "Error al iniciar sesión. Usuario o contraseña son incorrectos.";
            }
            return View();
            Console.WriteLine("Cerrando conexion");
            connection.Close();

           
            
        }

        

        public IActionResult Register(String CodEmpleado, String ClaveEmpleado){
            //Guardamos el codigo y la contraseña del empleado
            ViewBag.CodEmpleado = CodEmpleado;
            ViewBag.ClaveEmpleado = ClaveEmpleado;
            var connection = new NpgsqlConnection("Host=localhost;Port=5432;Pooling=true;Database=cspharma_informacional;UserId=postgres;Password=doshermanas1;");
            Console.WriteLine("ABRIENDO CONEXION");
            connection.Open();
            NpgsqlCommand consulta = new NpgsqlCommand($"INSERT INTO \"dlk_informacional\".\"dlk_cat_acc_empleados\" (cod_empleado, clave_empleado) VALUES('{CodEmpleado}','{ClaveEmpleado}')", connection);
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