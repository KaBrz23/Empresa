using Empresa.Data;
using Empresa.Models;
using Empresa.Reapository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Reapository
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly dbContext dbContext;
        public DepartamentoRepository(dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Departamento>> GetDepartamentos()
        {
            return await dbContext.Departamentos.ToListAsync();
        }

        public async Task<Departamento> GetDepartamento(int DepId) => await dbContext.Departamentos.FirstOrDefaultAsync(d => d.DepId == DepId);

        public async Task<Departamento> AddDepartamento(Departamento departamento)
        {
            var result = await dbContext.Departamentos.AddAsync(departamento);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }        

        public async Task<Departamento> UpdateDepartamento(Departamento departamento)
        {
            var result = await dbContext.Departamentos.FirstOrDefaultAsync(d => d.DepId == departamento.DepId);
            if (result != null)
            {
                result.DepNome = departamento.DepNome;

                await dbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        public async void DeleteDepartamento(int DepId)
        {
            var result = await dbContext.Departamentos.FirstOrDefaultAsync(d => d.DepId == DepId);
            if (result != null)
            {
                dbContext.Departamentos.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Empregado>> GetEmpregadosPorDepartamento(int depId)
        {
            return await dbContext.Empregados
                                  .Where(e => e.DepId == depId)
                                  .ToListAsync();
        }

    }
}
