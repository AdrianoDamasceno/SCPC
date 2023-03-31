namespace SCPCFinal.Models.Relatorios
{
    public class TabelaConsumosModel
    {
        public List<ServicosPeriodoModel> servicosPeriodoModel { get; set; }
        public List<ProdutosPeriodoModel> produtosPeriodo { get; set; }

        public TabelaConsumosModel()
        {
            servicosPeriodoModel = new List<ServicosPeriodoModel>();
            produtosPeriodo = new List<ProdutosPeriodoModel>();
        }
    }

}
