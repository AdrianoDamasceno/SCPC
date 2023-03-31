namespace SCPCFinal.Models
{
    public class PEPModel
    {
        public FormularioModel formulario { get; set; }
        public List<ConsumoProdutosModel> consumoProdutos { get; set; }
        public List<ConsumoServicosModel> consumoServicos { get; set; }
        public List<ExamesModel> exames { get; set; }
        public List<PrescricaoModel> prescricao { get; set; }
        public List<EvolucaoModel> evolucao { get; set; }
        public List<AtendimentoModel> atendimento { get; set; }
        public List<CentroCustoModel> centroCusto { get; set; }
        public List<EspecialidadesModel> especialidades { get; set; }
        public List<ConvenioModel> convenio { get; set; }
        public List<ServicoModel> servico { get; set; }
        public List<TransferenciaPeriodoModel> transferenciaPeriodo { get; set; }
        public List<InternacaoPeriodoModel> internacaoPeriodo { get; set; }
        public List<PacientesCaconModel> pacientesCacon { get; set; }
        public List<ProdutosPeriodoModel> produtosPeriodo { get; set; }
        public List<ServicosPeriodoModel> servicosPeriodoModel { get; set; }

        public PEPModel()
        {
            formulario = new FormularioModel();
            consumoServicos = new List<ConsumoServicosModel>();
            consumoProdutos = new List<ConsumoProdutosModel>();
            exames = new List<ExamesModel>();
            prescricao = new List<PrescricaoModel>();
            evolucao = new List<EvolucaoModel>();
            atendimento = new List<AtendimentoModel>();
            centroCusto = new List<CentroCustoModel>();
            especialidades = new List<EspecialidadesModel>();
            convenio = new List<ConvenioModel>();
            servico = new List<ServicoModel>();
            transferenciaPeriodo = new List<TransferenciaPeriodoModel>();
            internacaoPeriodo = new List<InternacaoPeriodoModel>();
            pacientesCacon = new List<PacientesCaconModel>();
            produtosPeriodo = new List<ProdutosPeriodoModel>();
            servicosPeriodoModel = new List<ServicosPeriodoModel>();
        }
    }
}
