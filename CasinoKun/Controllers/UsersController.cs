using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CasinoKun.Models;
using System.Text;
using System.Security.Cryptography;
using CasinoKun.Data;

namespace CasinoKun.Controllers
{
	public class UsersController : Controller
	{
		private readonly CasinoKunContext _context;

		public UsersController(CasinoKunContext context)
		{
			_context = context;
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Logout()
		{
			// Aquí puedes implementar la lógica para cerrar la sesión
			return RedirectToAction("Index");
		}

        public IActionResult InitLog()
        {
            return View();
        }

		public IActionResult Cuenta()
		{
			return View();
		}

		public IActionResult Seguridad()
		{
			return View();
		}

		public IActionResult AggFondos()
		{
			return View();
		}

        public IActionResult RetirarFondos()
        {
            return View();
        }

        public IActionResult Ubicacion()
        {
            return View();
        }

        public IActionResult Soporte()
        {
            return View();
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(User oUser)
		{
			if (ModelState.IsValid)
			{
				if (oUser.Password != oUser.ConPassword)
				{
					ViewData["Mensaje"] = "Las Contraseñas no coinciden";
					return View(oUser);
				}

				// Verificar si el usuario ya está registrado
				var existingUser = _context.User.FirstOrDefault(u => u.Cedula == oUser.Cedula);
				if (existingUser != null)
				{
					ViewData["Mensaje"] = "El usuario ya está registrado";
					return View(oUser);
				}

				// Convertir la contraseña a SHA256
				//oUser.Password = ConvertirSha256(oUser.Password);

				// Guardar el usuario en la base de datos
				_context.User.Add(oUser);
				_context.SaveChanges();

				return RedirectToAction("Login");
			}

			return View(oUser);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User oUser)
        {
            if (ModelState.IsValid)
            {
                // Convertir la contraseña a SHA256
                // hashedPassword = ConvertirSha256(oUser.Password);

                // Buscar al usuario en la base de datos por su cédula y contraseña
                var existingUser = _context.User.FirstOrDefault(u => u.Cedula == oUser.Cedula && u.Password == oUser.Password);

                if (existingUser != null)
                {
                    // Usuario autenticado correctamente, puedes establecer la sesión
                    // Aquí redirecciona al usuario a una página de bienvenida o al área protegida
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Las credenciales son incorrectas
                    ViewData["Mensaje"] = "Cédula o contraseña incorrectas";
                    return View();
                }
            }

            return View();
        }









        public static string ConvertirSha256(string texto)
		{
			StringBuilder Sb = new StringBuilder();
			using (SHA256 hash = SHA256Managed.Create())
			{
				Encoding enc = Encoding.UTF8;
				byte[] result = hash.ComputeHash(enc.GetBytes(texto));

				foreach (byte b in result)
					Sb.Append(b.ToString("x2"));
			}

			return Sb.ToString();
		}
	}
}
