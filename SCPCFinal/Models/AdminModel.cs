namespace SCPCFinal.Models
{
    public class AdminModel
    {
        public dynamic Relatorio { get; set; }
        public List<CentroCustoModel> centroCusto { get; set; }
        public List<EspecialidadesModel> especialidades { get; set; }
        public List<ConvenioModel> convenio { get; set; }
        public List<ServicoModel> servico {get; set; }
        
        

        public AdminModel() 
        {
            centroCusto = new List<CentroCustoModel>();
            especialidades = new List<EspecialidadesModel>();
            convenio = new List<ConvenioModel>();
            servico = new List<ServicoModel>();
                        
        }
    }
}
