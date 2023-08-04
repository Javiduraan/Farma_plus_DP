namespace Farma_plus.Models
{
    public class Tratamiento
    {
        public int NoConsulta { get; set; }
        public int Periodo { get; set; }
        public string ClaveMedicamento { get; set; }
        public int Dosis { get; set; }
        public int Frecuencia { get; set; }
        public int Dias { get; set; }
        public string StatusFarmacia  { get; set; }
        public string DescDosis { get; set; }
        public string DescFrecuencia { get; set; }
        public string TipoFrecuencia { get; set; }
        public string OtrasIndicaciones { get; set; }
        public string StatusImpresion { get; set; }
        public int Hora { get; set; }
        public int flujoSanguineo { get; set; }
        public string Identificacion { get; set; }
        public string Estatus { get; set; }
        public int CantSurt { get; set; }
        public string Cirugia { get; set; }
        public string DosisCirug { get; set; }
        public string Indicaciones { get; set; }

    }
}
