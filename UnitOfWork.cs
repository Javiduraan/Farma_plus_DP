using Farma_plus.Interfaces;

namespace Farma_plus
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICatalogoArticulos catalogoArticulos,
                          ITratamientoRepository tratamientos,
                          IHistorialCitasRepository historialCitas,
                          IDatosGralesRepository datosGrales,
                          ISurtidoFarmaAlternoRepository surtidoFarmas,
                          IUserRepository users) 
        { 
            CatalogoArticulos = catalogoArticulos;
            Tratamientos = tratamientos;
            HistorialCitas = historialCitas;
            DatosGrales = datosGrales;
            SurtidoFarmas = surtidoFarmas;
            Users = users;
        }

        public ICatalogoArticulos CatalogoArticulos { get; }
        public ITratamientoRepository Tratamientos { get; }
        public IHistorialCitasRepository HistorialCitas { get; }
        public IDatosGralesRepository DatosGrales { get; }
        public ISurtidoFarmaAlternoRepository SurtidoFarmas { get; }
        public IUserRepository Users { get; }

    }
}
