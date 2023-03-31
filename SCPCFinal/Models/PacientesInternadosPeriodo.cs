namespace SCPCFinal.Models
{
    public class PacientesInternadosPeriodo
    {
        public List<CentroCustoModel> centroCusto { get; set; }
        public List<EspecialidadesModel> especialidades { get; set; }
        public List<ConvenioModel> convenio { get; set; }
        public List<ServicoModel> servico { get; set; }
        public List<TransferenciaPeriodoModel> listaTransferenciaPeriodo { get; set; }
        public List<InternacaoPeriodoModel> listaInternacaoPeriodo { get; set; }

        public PacientesInternadosPeriodo()
        {
            centroCusto = new List<CentroCustoModel>();
            especialidades = new List<EspecialidadesModel>();
            convenio = new List<ConvenioModel>();
            servico = new List<ServicoModel>();
            listaTransferenciaPeriodo = new List<TransferenciaPeriodoModel>();
            listaInternacaoPeriodo = new List<InternacaoPeriodoModel>();
        }
    }

}
