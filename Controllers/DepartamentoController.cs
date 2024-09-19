using Empresa.Models;
using Empresa.Reapository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IDepartamentoRepository departamentoRepository;

        public DepartamentoController(IDepartamentoRepository departamentoRepository)
        {
            this.departamentoRepository = departamentoRepository;
        }
        #endregion

        #region GET para buscar todos os departamentos
        [HttpGet]
        public async Task<ActionResult> getDepartamentos()
        {
            try
            {
                return Ok(await departamentoRepository.GetDepartamentos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar dados do banco de dados");
            }
        }
        #endregion

        #region GET para buscar um departamento usando o ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Departamento>> getDepartamento(int id)
        {
            try
            {
                var result = await departamentoRepository.GetDepartamento(id);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
            }
        }
        #endregion

        #region POST para criar um novo departamento
        [HttpPost]
        public async Task<ActionResult<Departamento>> createDepartamento([FromBody]Departamento departamento)
        {
            try
            {
                if(departamento == null) return BadRequest();

                var result = await departamentoRepository.AddDepartamento(departamento);

                return CreatedAtAction(nameof(getDepartamento), new { id = result.DepId }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no banco de dados");
            }
        }
        #endregion

        #region PUT para atualizar um departamento
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Departamento>> UpdateDepartamento([FromBody]Departamento departamento)
        {
            try 
            {
                var result = await departamentoRepository.GetDepartamento(departamento.DepId);
                if (result == null) return NotFound($"Departamento = {departamento.DepNome} não encontrado");

                return await departamentoRepository.UpdateDepartamento(departamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no banco de dados");
            }
        }
        #endregion

        #region DELETE para deletar um departamento
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Departamento>> DeleteDepartamento(int id)
        {
            try
            {
                var result = await departamentoRepository.GetDepartamento(id);
                if (result == null) return NotFound($"Departamento com id = {id} não encontrado");

                departamentoRepository.DeleteDepartamento(id);

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no banco de dados");
            }
        }
        #endregion


        #region GET para buscar empregados por um departamento
        [HttpGet("{depId:int}/Empregados")]
        public async Task<ActionResult<IEnumerable<Empregado>>> GetEmpregadosPorDepartamento(int depId)
        {
            try
            {
                var result = await departamentoRepository.GetEmpregadosPorDepartamento(depId);
                if (result == null || !result.Any()) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar empregados do departamento: {ex.Message}");
            }
        }
        #endregion

    }
}
