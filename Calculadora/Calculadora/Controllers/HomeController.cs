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
            Session["primeiraVezOperador"] = true;
            Session["iniciarOperando"] = false;
            Session["baseNum"] = "DEC";
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string visor, string btn)
        {
            string resposta = visor;
            string trocavalor = resposta.Substring(1);
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
              
            
            if ((bool)Session["iniciarOperando"] || visor.Equals("0")) //visor =="0"
                    {
                        resposta = btn;
                    }
                    else
                    {
                        resposta = resposta + btn;
                    }
                    Session["iniciarOperando"] = false;
                    break;
                //Alterar o valor de positivo para negativo
                case "+/-":
                    if (!visor.Contains("-"))
                    {
                        resposta = "-" + visor;
                    }
                    else
                    {
                        resposta = trocavalor;
                    }
                    break;
                case ".":
                    //Processar o caso da ','
                    if (!visor.Contains("."))
                    {
                        resposta = resposta + btn;
                    }
                    break;
                //Se entrei aqui, é porque foi selecionado um 'operador'
                case "+":
                case "-":
                case "X":
                case "/":
                case "=":
                    //É a primeira vez que carreguei num destes operadores?
                    if ((bool)Session["primeiraVezOperador"]) // == true é facultativo
                    { 
                        Session["primeiraVezOperador"] = false;
                    }
                    else
                    {
                        // Recuperar os valores dos operandos
                        double operador1 = Convert.ToDouble((string)Session["primeiroOperando"]);
                        double operador2 = Convert.ToDouble(visor);
                        switch ((string)Session["operadorAnterior"])
                        {
                            case "+":
                                visor = operador1 + operador2 + "";
                                break;
                            case "-":
                                visor = operador1 - operador2 + "";
                                break;
                            case "X":
                                visor = operador1 * operador2 + "";
                                break;
                            case "/":
                                visor = operador1 / operador2 + "";
                                break;
                        }

                    }
                    resposta = visor;
                    Session["primeiroOperando"] = visor;
                    // limpar display
                    Session["iniciarOperando"] = true;

                    if (btn.Equals("="))
                    {
                        //Marcar o operador como primeiro operando
                        Session["primeiraVezOperador"] = true;
                    }
                    else
                    {
                        // Guardar o valor do operador
                        Session["operadorAnterior"] = btn;
                        Session["primeiraVezOperador"] = false;
                    }
                    //Guardar o display para utilização futura
                    Session["primeiroOperando"] = resposta;
                    Session["iniciarOperando"] = true;
                    break;
                //Apagar o valor dentro do ecrã
                case "C":
                    resposta = "0";
                    Session["primeiraVezOperador"] = true;
                    Session["iniciarOperando"] = false;
                    break;
                case "BIN":
                    if((string)Session["baseNum"] == "DEC")
                    {
                        int valor = Convert.ToInt32(resposta);
                        string bin = Convert.ToString(valor, 2);
                        resposta = bin;
                        Session["baseNum"] = "BIN";
                    }
                    break;
                case "DEC":
                    if ((string)Session["baseNum"] == "BIN")
                    {
                        int dec = Convert.ToInt32(resposta, 10);
                        resposta = dec.ToString();
                        Session["baseNum"] = "DEC";
                    }
                    break;
            }//switch


            //devolver a 'resposta' para o visor do ecrã
            ViewBag.Resposta = resposta;
            return View();
        }
    }
}