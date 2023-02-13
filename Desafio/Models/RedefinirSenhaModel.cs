using System.ComponentModel.DataAnnotations;

namespace Desafio.Models
{
    public class RedefinirSenhaModel
    {
        
        [Required(ErrorMessage = "Digite o e-mail")]
        public string Email { get; set; }
    }
}
