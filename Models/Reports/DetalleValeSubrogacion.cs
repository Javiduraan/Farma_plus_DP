namespace Farma_plus.Models.Reports
{
    public class DetalleValeSubrogacion
    {
        public int ClaveMedicamento { get; set; }
        public int Cantidad { get; set; }
        public string NombreMedicamento { get; set; }
        public string? Fuerza { get; set; }
        public string? Presentacion { get; set; }
        public string DescArticulo { get; set; }

    }
}
