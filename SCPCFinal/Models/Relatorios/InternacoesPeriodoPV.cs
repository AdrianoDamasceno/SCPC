namespace SCPCFinal.Models.Relatorios
{
    public class InternacoesPeriodoPV
    {   
        public List<TransferenciaPeriodoModel> listaTransferenciaPeriodo {get; set;}
        public List<InternacaoPeriodoModel> listaInternacaoPeriodo { get; set; }

        public InternacoesPeriodoPV()
        {
            listaTransferenciaPeriodo = new List<TransferenciaPeriodoModel>();
            listaInternacaoPeriodo = new List<InternacaoPeriodoModel>();
        }
    }
}
