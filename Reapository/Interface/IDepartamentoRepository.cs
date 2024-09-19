using Empresa.Models;

namespace Empresa.Reapository.Interface
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> GetDepartamentos();
        Task<Departamento> GetDepartamento(int DepId);
        Task<Departamento> AddDepartamento(Departamento departamento);
        Task<Departamento> UpdateDepartamento(Departamento departamento);
        void DeleteDepartamento(int DepId);
        Task<IEnumerable<Empregado>> GetEmpregadosPorDepartamento(int depId);

    }
}
