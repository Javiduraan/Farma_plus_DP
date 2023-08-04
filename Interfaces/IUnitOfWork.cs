using System;

namespace Farma_plus.Interfaces
{
    public interface IUnitOfWork
    {
        ICatalogoArticulos CatalogoArticulos { get; }
        IDatosGralesRepository DatosGrales { get; }
        IHistorialCitasRepository HistorialCitas { get; }
        ITratamientoRepository Tratamientos { get; }
        ISurtidoFarmaAlternoRepository SurtidoFarmas { get; }
        IUserRepository Users { get; }

    }
}
