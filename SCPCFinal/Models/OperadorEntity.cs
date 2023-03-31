using Dapper.Contrib.Extensions;

namespace SCPCRazor.Model
{
    [Table("cadope")]
    public class Operador
    {
        public int id { get; set; }
        public int prestador { get; set; }
        public string perfil { get; set; }
        public string operador { get; set; }
        public string senha { get; set; }   
    }
}
