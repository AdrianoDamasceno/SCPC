using Microsoft.AspNetCore.Mvc.RazorPages;
using SCPCFinal.Models;
using SCPCRazor.Data;
using RtfPipe;
using System.Text.Encodings;
using System.Text;
using SCPCFinal.Models.Relatorios;

namespace SCPCFinal.Helper
{
    public static class GigaStatic
    {
        public static PEPModel retornaFiltros()
        {
            var model = new PEPModel();
            var db = new DataSCPCRepo();

            model.centroCusto = db.BuscarCentroCusto();
            model.especialidades = db.BuscarEspecialidadesM();
            model.convenio = db.BuscarConvenio();
            model.servico = db.BuscarServico();            

            return model;
        }
        public static TabelaConsumosModel retornaTabelaConsumos(string codpac, string data1, string data2, string op)
        {
            var model = new TabelaConsumosModel();
            var db = new DataSCPCRepo();

            if (op == "S")
            {
                model.servicosPeriodoModel = db.BuscarServicosPeriodo(codpac, data1, data2);
            }
            else if(op == "P")
            {
                model.produtosPeriodo = db.BuscarProdutosPeriodo(codpac, data1, data2);
            }
            return model;
        }
        public static PacientesCaconModelPV retornaPacientesCACON()
        {
            var model = new PacientesCaconModelPV();
            var db = new DataSCPCRepo();

            model.pacientesCacon = db.BuscarPacientesCacon();
            return model;
        }

        //public static PacientesInternadosPeriodoPV retornaPacientesInternadosPeriodo()
        //{
        //    var model = new PacientesInternadosPeriodoPV();
        //    var db = new DataSCPCRepo();

        //    model.pacientesInternadosPeriodo = db.BuscarPacientesInternadosPeriodo();
        //    return model;
        //}

        public static PEPModel retornaConsumo(string codpac, string data1, string data2, PEPModel? model1)
        {
            var model = new PEPModel();
            var db = new DataSCPCRepo();

            if (model1 is not null)
            {
                model = model1;
            }

            model.produtosPeriodo = db.BuscarProdutosPeriodo(codpac, data1, data2);
            model.servicosPeriodoModel = db.BuscarServicosPeriodo(codpac, data1, data2);

            return model;
        }

        public static PEPModel retornaCarga(string codpac)
        {
            var model = new PEPModel();
            var db = new DataSCPCRepo();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            model.formulario = db.BuscarFormulario(codpac);
            
            if(model.formulario.DataNasc == null)
            {
                model.formulario.Idade = 0;
                model.formulario.DataNascimento = String.Empty;
            }
            
            else
            {
                model.formulario.Idade = CalculaIdade(model.formulario.DataNasc);
                model.formulario.DataNascimento = DataNascToString(model.formulario.DataNasc);
            }
            
            model.consumoProdutos = db.BuscarConsumoProdutos(codpac);
            model.consumoServicos = db.BuscarConsumoServicos(codpac);
            model.exames = db.BuscarExames(codpac);
            model.evolucao = db.BuscarEvolucao(codpac);
            model.atendimento = db.BuscarAtendimento(codpac);
            //model.produtosPeriodo = db.BuscarProdutosPeriodo(codpac, data1, data2);

            //model.internacaoPeriodo = db.BuscarInternacao(codpac);
            //model.transferenciaPeriodo = db.BuscarTransferencia(codpac);
            
            
            

            foreach(var exame in model.exames)
            {
                exame.TextoResultado = Rtf.ToHtml(exame.TextoResultado);
            }

            foreach (var evolucao in model.evolucao)
            {
                evolucao.CorpoTexto = Rtf.ToHtml(evolucao.CorpoTexto);
            }

            model.prescricao = db.BuscarPrescricao(codpac);


            foreach(var atend in model.atendimento)
            {
                if (atend.DataSai.ToShortDateString() == "01/01/0001")
                {
                   //return atend.DataSai
                }
            }

            return model;

        }

        private static string DataNascToString(DateTime? DataNasc)
        {
            var dt = (DateTime)DataNasc;
            return dt.Date.ToString("dd/MM/yyyy");
            
        }

        private static string DataSaiNullValue(DateTime? DataSai)
        {
            var dt = (DateTime)DataSai;

            if (dt.Date.ToString("dd/MM/yyyy") == ("01/01/0001"))
            {
                return string.Empty;

            }
            return dt.Date.ToString("dd/MM/yyyy");
        }

        public static int CalculaIdade(DateTime? DataNasc)
        {
            int retorno = -1;
            var dt = (DateTime)DataNasc;
            retorno = DateTime.Today.Year - dt.Year;
            if (dt.Date > DateTime.Today.AddYears(-retorno)) retorno--;

            return retorno;
        }
        
    }
}
