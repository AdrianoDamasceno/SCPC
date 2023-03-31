using Microsoft.AspNetCore.Mvc.Rendering;

namespace SCPCFinal.Models
{
    public class LoginModel
    {
        public string UsuarioSCPC { get; set; }
        public string SenhaSCPC { get; set; }

        public string Modulo { get; set; }
        public List<SelectListItem> ListaModulos { get; set; }
        public string Mensagem { get; set; }

        public LoginModel() 
        {
            UsuarioSCPC = string.Empty;
            SenhaSCPC = string.Empty;
            Mensagem = string.Empty;
            ListaModulos = new List<SelectListItem>();
            ListaModulos.Add(new SelectListItem("PEP", "PEP"));
            ListaModulos.Add(new SelectListItem("Administrativo", "Administrativo"));

        }

    }
}
