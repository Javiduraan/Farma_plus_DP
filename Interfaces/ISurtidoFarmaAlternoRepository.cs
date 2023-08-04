using Farma_plus.Models;
using Farma_plus.Models.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Farma_plus.Interfaces
{
    public interface ISurtidoFarmaAlternoRepository : IGenericRepository<SurtidoFarmaAlterno>
    {
        Task<IReadOnlyList<SurtidoFarmaAlterno>> GetSurtidoFarma(int NumeroPensiones, int Parentesco, int FolioReceta);
        Task<IReadOnlyList<ValeSubrogadoDto>> GetDetalleVale(int folioReceta);
        Task<IReadOnlyList<Farmacias>> GetFarmacias(string vMedicamento);
        Task<IReadOnlyList<_BMedicamento>> GetMedicamentos(string vMedicamento);
    }
}
