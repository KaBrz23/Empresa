using Empresa.Data;
using Empresa.Models;
using Empresa.Reapository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Reapository
{
    public class EmpregadoRepository : IEmpregadoRepository
    {
        private readonly dbContext dbContext;
        public EmpregadoRepository(dbContext dbContext) { 
            this.dbContext = dbContext;
        }
        public Task<Empregado> AddEmpregado(Empregado empregado)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmpregado(int empId)
        {
            throw new NotImplementedException();
        }

        public Task<Empregado> GetEmpregado(int empId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Empregado>> GetEmpregados()
        {
            return await dbContext.Empregados.ToListAsync();
        }

        public Task<Empregado> UpdateEmpregado(Empregado empregado)
        {
            throw new NotImplementedException();
        }
    }
}
