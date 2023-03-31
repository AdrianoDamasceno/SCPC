namespace SCPCFinal.Models
{
    public class PrescricaoModel
    {
        public string Tipo { get; set; }
        public DateTime DataIni { get; set; }
        public string NumAtend { get; set; }
        public string NumPrescr { get; set; }
        public string NumItem { get; set; }
        public string CodProd { get; set; }
        public string CodIntSv { get; set; }
        public string Descricao { get; set; }
        public string DescIntSv { get; set; }
        public string Descricao2 { get; set; }
        public string Cancelado { get; set; }
        public string Via { get; set; }
        public string Periodo { get; set; }
        public string QtdTotal { get; set; }        

    }
}
