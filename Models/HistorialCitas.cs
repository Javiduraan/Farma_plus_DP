using System;

namespace Farma_plus.Models
{
    public class HistorialCitas
    {
        public int NoConsulta { get; set; }
        public int Periodo { get; set; }
        public int NoPensiones { get; set; }
        public int Parentesco { get; set; }
        public string ClaveMedico { get; set; }
        public DateTime FechaConsulta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int NoReceta { get; set; }
        public string HoraConsulta { get; set; }
        public string Id_forma { get; set; }
        public string StatusExpediente { get; set; }
        public string IdNota { get; set; }
        public string ClaveHospital { get; set; }
        public string HoraIniConsulta { get; set; }
        public string statusImpresion { get; set; }
        public DateTime FechaProximaCita { get; set; }
        public int HoraProximaCita { get; set; }
        public string FProximaCita { get; set; }
        public int NumImpresionReceta { get; set; }
        public int NumImpresionLaboratorio { get; set; }
        public int NumImpresionImagenologia { get; set; }
        public DateTime FechaConsultaInicial { get; set; }
        public string Forma { get; set; }

    }
}
