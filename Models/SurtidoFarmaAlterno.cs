using System;

namespace Farma_plus.Models
{
    public class SurtidoFarmaAlterno
    {
        public int? FolioReceta { get; set; }
        public int? NumPensiones { get; set; }
        public int? Parentesco { get; set; }
        public string? Nombre { get; set; }
        public string? DescArticulo { get; set; }
        public string? Dosis { get; set; }
        public string? DesDosis { get; set; }
        public int? Frecuencia { get; set; }
        public string? TipoFrecuencia { get; set; }
        public string? Dias { get; set; }
        public string? FechaHoraSurtido { get; set; }
        public string? ClaveMedicamento { get; set; }
        public int? CantSurtida { get; set; }
        public string? ValeSubrogado { get; set; }
        public int? NumFarmaciaSubrogado { get; set; }
        public string? NuevoResurtido { get; set; }

    }
}
