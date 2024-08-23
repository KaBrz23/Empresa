using Empresa.Models;

namespace Empresa.Reapository.Interface
{
    public interface IDepartamentoRepository
    {
        IEnumerable<Departamento> GetDepartamento();
        Departamento GetDepartamento(int id);
    }
}
