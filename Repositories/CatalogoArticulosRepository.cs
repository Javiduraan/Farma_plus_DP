using Farma_plus.Interfaces;
using Farma_plus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Farma_plus.Repositories
{
    public class CatalogoArticulosRepository : ICatalogoArticulos
    {
        public Task<int> AddAsync(CatalogoArticulosVw entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CatalogoArticulosVw>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<CatalogoArticulosVw> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(CatalogoArticulosVw entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
