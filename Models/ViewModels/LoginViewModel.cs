// LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    public string Usuario { get; set; }

    [Required]
    public string Contraseña { get; set; }
}