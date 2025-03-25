using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class GestionController : Controller
    {
        // GET: Gestion
        public ActionResult Categorias()
        {
            return View();
        }

        public ActionResult Marcas()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            string mensaje = string.Empty;
            List<Categoria> categorias = new List<Categoria>();
            categorias = new CN_Categorias().Listar(out mensaje);
            return Json(new { data = categorias, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria categoria)
        {
            object resultado;
            string mensaje = string.Empty;

            if (categoria.IdCategoria == 0)
            {
                resultado = new CN_Categorias().Registrar(categoria, out mensaje);
            }
            else
            {
                resultado = new CN_Categorias().Actualizar(categoria, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int idCategoria)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Categorias().Eliminar(idCategoria, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarMarcas()
        {
            string mensaje = string.Empty;
            List<Marca> marcas = new List<Marca>();
            marcas = new CN_Marcas().Listar(out mensaje);
            return Json(new { data = marcas, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca (Marca marca)
        {
            object resultado;
            string mensaje = string.Empty;

            if (marca.IdMarca == 0)
            {
                resultado = new CN_Marcas().Registrar(marca, out mensaje);
            }
            else
            {
                resultado = new CN_Marcas().Actualizar(marca, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int idMarca)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Marcas().Eliminar(idMarca, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}