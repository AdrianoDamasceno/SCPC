using Microsoft.AspNetCore.Mvc;
using SCPCFinal.Helper;
using SCPCFinal.Models;
using SCPCFinal.Models.Relatorios;
using SCPCRazor.Data;
using SCPCRazor.Model;
using System.Diagnostics;


namespace SCPCFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string mensagem)
        {
            if (String.IsNullOrEmpty(mensagem)) mensagem = "Digite seu usuário e senha para continuar:";
            return View(new LoginModel { Mensagem = mensagem});
        }

        public IActionResult PEP(string codpac)
        {
            PEPModel model = new PEPModel();
            if (String.IsNullOrEmpty(codpac))
            {
                model = GigaStatic.retornaFiltros();
                return View(model);
            }
            model = GigaStatic.retornaCarga(codpac);
            return View(model);
        }

        public IActionResult TabelaConsumos(string codpac, string data1, string data2, string opcao)
        {
            var model = new TabelaConsumosModel();
            model = GigaStatic.retornaTabelaConsumos(codpac, data1, data2, opcao);

            return PartialView(model);
        }

        public IActionResult PacientesCACON()
        {
            var model = new PacientesCaconModelPV();
            model = GigaStatic.retornaPacientesCACON();

            return PartialView(model);
        }

        //public IActionResult PacientesInternadosPeriodo(DateTime dtentrada, DateTime dtsaida)
        //{
        //    var model = new RelatorioPacientesInternadosPeriodo();

        //    //model = GigaAdmin.CarregaRelatorioPacientesInternadosNoPeriodo(dtentrada, dtsaida);

        //    return PartialView(model);
        //}

        public IActionResult GerarRelatorioConsumo(string data1, string data2, string codpac)
        {
            var model = GigaStatic.retornaConsumo(codpac, data1, data2, null);
            return Json(model);
        }

        public IActionResult InternacaoPeriodo(string dtentrada, string dtsaida)
        {
            var model = new RelatorioPacientesInternadosPeriodo();

            if (String.IsNullOrEmpty(dtentrada)) return PartialView(model);
            
            model = GigaAdmin.CarregaRelatorioPacientesInternadosNoPeriodo(dtentrada, dtsaida);
            //model = GigaAdmin.RetornaInternacao(dtentrada, dtsaida);
            return PartialView(model);
        }

        public IActionResult Administrativo(string relatorio)
        {
            var model = new AdminModel();
            if(String.IsNullOrEmpty(relatorio)) return View(model);

            model.Relatorio = GigaAdmin.CarregaRelatorio(relatorio);
            return View(model);
        }
        

        public IActionResult EnviarCodPac(string codpac)
        {
            return Redirect("/Home/PEP?codpac="+codpac);
        }

        [HttpPost]
        public IActionResult Login(LoginModel usuario)
        {
            if(String.IsNullOrEmpty(usuario.UsuarioSCPC)|| String.IsNullOrEmpty(usuario.UsuarioSCPC))
            {
                return RedirectToAction("Index");
            }

            var db = new DataSCPCRepo();
            var OperadorAutenticado = db.Autenticar(usuario.UsuarioSCPC, usuario.SenhaSCPC);
            var valor = usuario.Modulo;

            if(OperadorAutenticado is null)
            {
                return RedirectToAction("Index");
            }

            //Session["id"] = OperadorAutenticado.id

            if (valor == "PEP")
            {
                return RedirectToAction("PEP");
            }
            else if (valor == "Administrativo")
            {
                return RedirectToAction("Administrativo");
            }
            else return null;

        }

        public ActionResult GetList()
        {
            var db = new DataSCPCRepo();

            var empList = db.BuscarListaPacienteModel();
            //var empList = new ModeloDados();
            //empList.data = db.Paciente();
            return Json(new { data = empList });
        }

        public JsonResult BuscaPaciente(DataTableAjaxPostModel model)
        {
            var db = new DataSCPCRepo();
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            var res = db.BuscarListaPacienteModel();

            var result = new List<PacienteModel>(res.Count);
            //foreach (var s in res)
            //{
            //    // simple remapping adding extra info to found dataset
            //    result.Add(new PacienteModel
            //    {
            //        CPF = s.CPF
            //    });
            //};

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = res
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}