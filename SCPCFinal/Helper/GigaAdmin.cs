using SCPCFinal.Data;
using SCPCFinal.Models;
using SCPCFinal.Models.Relatorios;
using SCPCRazor.Data;

namespace SCPCFinal.Helper
{
    public static class GigaAdmin
    {
        public static dynamic CarregaRelatorio(dynamic nomeModel) 
        {
            const string CONSULTA_METODO = "Consulta";
            var db = new DataPost(string.Empty);
            var retorno = new List<dynamic>();

            //var tt = Type.GetTypeFromProgID(nomeModel.ProgID);
            //assemblyInstance.GetType(typeName)
            var tipo = Type.GetType(nomeModel);
            if (tipo != null)
            {
                dynamic objetoEscolhido = Activator.CreateInstance(tipo);
                db.GetList(objetoEscolhido.GetMethod(CONSULTA_METODO).ToString(), retorno, tipo);
            }
            return retorno;

        }

        public static List<InternacaoPeriodoModel> RetornaInternacao(DateTime dtentrada, DateTime dtsaida)
        {
            var retorno = new List<InternacaoPeriodoModel>();
            var db = new DataSCPCRepo();
            
            retorno = db.BuscarInternacao(dtentrada, dtsaida);            


            return retorno;
        }

        public static RelatorioPacientesInternadosPeriodo CarregaRelatorioPacientesInternadosNoPeriodo(string dtentrada, string dtsaida)
        {
            var retorno = new RelatorioPacientesInternadosPeriodo();
            var relatorio = new RelatorioPacientesInternadosPeriodo();
            var db = new DataSCPCRepo();
            DateTime dt1 = new DateTime(), dt2 = new DateTime();

            dt1 = DateTime.Parse(dtentrada);
            dt2 = DateTime.Parse(dtsaida);

            var modelInternacoes = db.BuscarInternacao(dt1, dt2);
            var listaTransf = new List<TransferenciaPeriodoModel>();

            foreach(var item in modelInternacoes)
            {
                listaTransf.AddRange(db.BuscarTransferencia(item.NumAtend));
            }

            relatorio.listaTransferenciaPeriodo = listaTransf;
            relatorio.listaInternacaoPeriodo = modelInternacoes;

            retorno = relatorio;

            return retorno;
        }
        


    }
}
