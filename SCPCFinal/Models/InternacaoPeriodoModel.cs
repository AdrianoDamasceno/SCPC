namespace SCPCFinal.Models
{


    public class InternacaoPeriodoModel
    {
        public string NumAtend { get; set; }
        public string TipoAtend { get; set; }
        public string Paciente { get; set; }
        public DateTime? DataAtend { get; set; }
        public DateTime? DataSai { get; set; }
        public string CodPlano { get; set; }
        public string NomeServ { get; set; }
        public string CentroCusto { get; set; }
        public string Leito { get; set; }
    }
}
