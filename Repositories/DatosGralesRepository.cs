using Farma_plus.Interfaces;
using Farma_plus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Farma_plus.Repositories
{
    public class DatosGralesRepository : IDatosGralesRepository
    {
        public Task<int> AddAsync(DatosGralesVw entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<DatosGralesVw>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<DatosGralesVw> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(DatosGralesVw entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
