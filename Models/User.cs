using Microsoft.AspNetCore.Identity;

namespace Farma_plus.Models
{
    public class User
    {
        public int Id_Usuario { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }

    }
}
