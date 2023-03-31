namespace SCPCFinal.Models
{
    public class AtendimentoModel
    {
        public string NumAtend { get; set; }

        public string TipoAtend { get; set; }        

        public DateTime DataAtend { get; set; }
        public DateTime DataSai { get; set; }

        public string CodPlano { get; set; }

        public string NomeServ { get; set; }

        public string CentroCusto { get; set; }
        public string NomePrest { get; set; }
    }
}
