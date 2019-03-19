using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Inicializar o visor com o zero
            ViewBag.Resposta = "0";
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string visor, string btn)
        {
            string resposta = visor;
            //avaliar o valor do btn
            switch (btn)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    //determinar se o visor tem apenas o zero
                    if (resposta == "0") //visor =="0"
                    {
                        resposta = btn;
                    }
                    else
                    {
                        resposta += btn;
                    }
                    break;
            }


            //devolver a 'resposta' para o visor do ecrã
            ViewBag.Resposta = resposta;
            return View();
        }
    }
}