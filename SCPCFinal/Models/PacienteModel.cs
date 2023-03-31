 namespace SCPCRazor.Model
{
    public class PacienteModel
    {
        public string Prontuario { get; set; }
        public string Nome { get; set; }
        public string DataNasc { get; set; }
        public string Plano { get; set; }
        public string Filiacao { get; set; }
        public string Pasta { get; set; }
        public string extra07 { get; set; }
        public string RG { get; set; }
        public string CartaoSUS { get; set; }
        public string Matricula { get; set; }
        public string Unid { get; set; }
        public string CPF { get; set; }


        public List<PacienteModel> Paciente()
        {
            var retorno = new List<PacienteModel>();

            retorno.Add(new PacienteModel
            {
                Prontuario = "12345678",
                Nome = "Adriano",
                DataNasc = "2022-01-01",
                Plano = "Sem plano",
                Filiacao = "Rosana",
                Pasta = "1A",
                extra07 = "N/A",
                RG = "0000000000000",
                CartaoSUS = "1289763812763",
                Matricula = "128736128736",
                Unid = "123",
                CPF = "111111111111"
            });

            return retorno;
        }
    }
}
