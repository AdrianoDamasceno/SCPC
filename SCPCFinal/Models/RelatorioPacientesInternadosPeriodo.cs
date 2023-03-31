namespace SCPCFinal.Models
{
    public class RelatorioPacientesInternadosPeriodo
    {
        public List<TransferenciaPeriodoModel> listaTransferenciaPeriodo { get; set; }
        public List<InternacaoPeriodoModel> listaInternacaoPeriodo { get; set; }

        public RelatorioPacientesInternadosPeriodo()
        {

            listaTransferenciaPeriodo = new List<TransferenciaPeriodoModel>();
            listaInternacaoPeriodo = new List<InternacaoPeriodoModel>();
        }
    }
}
