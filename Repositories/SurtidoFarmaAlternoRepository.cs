using Dapper;
using Farma_plus.Interfaces;
using Farma_plus.Models;
using Farma_plus.Models.Reports;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Farma_plus.Repositories
{
    public class SurtidoFarmaAlternoRepository : ISurtidoFarmaAlternoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionManager _connectionManager;

        public SurtidoFarmaAlternoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionManager = new ConnectionManager(configuration);
        }

        public async Task<int> AddAsync(SurtidoFarmaAlterno entity)
        {
            // Set the time to the current moment
            var today = DateTime.Now;

            IDbConnection conn = _connectionManager.GetConnection();
            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO SURTIDO_FARMA_ALTERNO(folio_receta, num_pensiones, parentesco, fecha_hora_surtido, cve_medicamento, cant_surtida, vale_subrogado, num_farmacia_subrogado, nuevo_resurtido)" +
                $" VALUES({entity.FolioReceta}, {entity.NumPensiones}, {entity.Parentesco}, '{today.ToShortDateString()}', '{entity.ClaveMedicamento.Trim()}', {entity.CantSurtida}, '{entity.ValeSubrogado}', {entity.NumFarmaciaSubrogado}, '{entity.NuevoResurtido}')";

            // Pass the product object and the SQL statement into the Execute function (async)
            var result = await conn.ExecuteAsync(sql);

            _connectionManager.CloseConnection(conn);

            return result;
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<SurtidoFarmaAlterno>> GetAllAsync()
        {
            IDbConnection conn = _connectionManager.GetConnection();

            var sql = "SELECT * FROM SURTIDO_FARMA_ALTERNO";

            var result = await conn.QueryAsync<SurtidoFarmaAlterno>(sql);

            _connectionManager.CloseConnection(conn);

            return result.ToList();
        }

        public async Task<SurtidoFarmaAlterno> GetByIdAsync(int id)
        {
            IDbConnection conn = _connectionManager.GetConnection();
            var sql = "SELECT * FROM SURTIDO_FARMA_ALTERNO WHERE FOLIO_RECETA = @Id";

            var result = await conn.QuerySingleOrDefaultAsync<SurtidoFarmaAlterno>(sql, new { Id = id });

            _connectionManager.CloseConnection(conn);
            return result;
        }

        public async Task<IReadOnlyList<ValeSubrogadoDto>> GetDetalleVale(int folioReceta)
        {
            IDbConnection conn = _connectionManager.GetConnection();
            var sql = "SELECT A.NUM_PENSIONES NumPensiones," +
                " A.PARENTESCO Parentesco," +
                " B.NOMBRE NombreDh," +
                " A.FECHA_HORA_SURTIDO FechaHoraSurtido," +
                " A.CVE_MEDICAMENTO ClaveMedicamento," +
                " A.CANT_SURTIDA Cantidad," +
                " C.DESCRIPCION || ' ' || C.FUERZA || ' ' || C.PRESENTACION DescArticulo," +
                " A.VALE_SUBROGADO ValeSubrogado," +
                " D.NOMBRE_FARMACIA NombreFarmacia," +
                " d.DOMICILIO DomicilioFarmacia," +
                " A.NUEVO_RESURTIDO NuevoResurtido," +
                " A.FOLIO_RECETA FolioReceta," +
                " H.CVE_MEDICO||' '||H.CVE_ESPECIALIDAD ClaveMedico" +
                " FROM surtido_farma_alterno a" +
                " JOIN pc_datos_grales_vw b" +
                " ON B.NUMERO_PENSIONES = A.NUM_PENSIONES" +
                " AND B.PARENTESCO = A.PARENTESCO" +
                " JOIN catalogo_articulos_vw c ON C.CVE_ANTERIOR = A.CVE_MEDICAMENTO" +
                " LEFT JOIN cat_farmacia_subrog d ON d.NUM_FARMACIA = A.NUM_FARMACIA_SUBROGADO" +
                " LEFT JOIN pc_historial_citas h ON H.NO_RECETA=A.FOLIO_RECETA and" +
                " H.NO_PENSIONES=A.NUM_PENSIONES and H.PARENTESCO=A.PARENTESCO" +
                $" WHERE A.FOLIO_RECETA = {folioReceta}";

            var result = await conn.QueryAsync<ValeSubrogadoDto>(sql);

            _connectionManager.CloseConnection(conn);
            return result.ToList();
        }

        public async Task<IReadOnlyList<Farmacias>> GetFarmacias(string vMedicamento)
        {
            IDbConnection conn = _connectionManager.GetConnection();
            var sql = "SELECT m.num_farmacia AS Clave,f.nombre_farmacia AS Nombre" +
                " FROM cat_contratos_farmacia m left join cat_farmacia_subrog f on m.num_farmacia = f.num_farmacia " +
                $"where M.CVE_MEDICAMENTO = '{vMedicamento}' ";

            var result = await conn.QueryAsync<Farmacias>(sql);

            _connectionManager.CloseConnection(conn);
            return result.ToList();

        }

        public async Task<IReadOnlyList<_BMedicamento>> GetMedicamentos(string vMedicamento)
        {
            IDbConnection conn = _connectionManager.GetConnection();
            var sql = "SELECT cve_anterior AS Clave,descripcion AS Nombre" +
                " FROM CATALOGO_ARTICULOS_VW " +
                $"WHERE DESCRIPCION LIKE '%{vMedicamento}%' ";

            var result = await conn.QueryAsync<_BMedicamento>(sql);

            _connectionManager.CloseConnection(conn);
            return result.ToList();

        }

        public async Task<IReadOnlyList<SurtidoFarmaAlterno>> GetSurtidoFarma(int NumeroPensiones, int Parentesco, int FolioReceta)
        {
            IDbConnection conn = _connectionManager.GetConnection();
            var sql = "SELECT H.NO_RECETA FolioReceta, " +
            " H.NO_PENSIONES NumPensiones," +
            " H.PARENTESCO Parentesco," +
            " D.NOMBRE Nombre," +
            " T.CVE_MEDICAMENTO ClaveMedicamento," +
            " A.DESCRIPCION||' '||A.FUERZA||' '||A.PRESENTACION DescArticulo," +
            " T.DOSIS Dosis," +
            " T.DESC_DOSIS DesDosis," +
            " T.FRECUENCIA Frecuencia," +
            " DECODE (T.TIPO_FRECUENCIA, 'H', 'HORAS', 'D', 'DIAS', T.TIPO_FRECUENCIA) TipoFrecuencia," +
            " T.DIAS Dias" +
            " FROM PC_HISTORIAL_CITAS H" +
            " JOIN PC_TRATAMIENTO T" +
            " ON H.NO_CONSULTA = T.NO_CONSULTA AND H.PERIODO = T.PERIODO" +
            " JOIN PC_DATOS_GRALES_VW D" +
            " ON H.NO_PENSIONES = D.NUMERO_PENSIONES" +
            " AND H.PARENTESCO = D.PARENTESCO" +
            " JOIN CATALOGO_ARTICULOS_VW A" +
            " ON A.CVE_ANTERIOR=T.CVE_MEDICAMENTO" +
            $" WHERE H.NO_RECETA = NVL ({FolioReceta}, H.NO_RECETA)" +
            $" AND (H.NO_PENSIONES IN ({NumeroPensiones} )" +
            $" AND H.PARENTESCO = {Parentesco} )";

            var result = await conn.QueryAsync<SurtidoFarmaAlterno>(sql);

            _connectionManager.CloseConnection(conn);
            return result.ToList();

        }

        public Task<int> UpdateAsync(SurtidoFarmaAlterno entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
