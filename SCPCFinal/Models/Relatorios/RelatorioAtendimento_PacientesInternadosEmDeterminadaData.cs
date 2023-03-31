using SCPCFinal.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCPCFinal.Models.Relatorios
{
    public class RelatorioAtendimento_PacientesInternadosEmDeterminadaData
    {
        [Display(Name="ID")]
        public string Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public string Consulta(string Id)
        {
            string retorno = @"SELECT * FROM cadpac LIMIT 5";
            return retorno;
        }

        public RelatorioAtendimento_PacientesInternadosEmDeterminadaData() { }

        //public List<RelatorioAtendimento_PacientesInternadosEmDeterminadaData> GetTabela(DataPost db)
        //{
        //    return db.GetList(Consulta("1"));
        //}
    }
}
