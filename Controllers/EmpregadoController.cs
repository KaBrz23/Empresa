using Empresa.Models;
using Empresa.Reapository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpregadoController : ControllerBase
    {
        private readonly IEmpregadoRepository empregadoRepository;

        public EmpregadoController(IEmpregadoRepository empregadoRepository)
        {
            this.empregadoRepository = empregadoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> getEmpregados()
        {
            try
            {
                return Ok(await empregadoRepository.GetEmpregados());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empregado>> getEmpregado(int id)
        {
            try
            {
                var result = await empregadoRepository.GetEmpregado(id);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Empregado>> createEmpregado([FromBody] Empregado empregado)
        {
            try
            {
                if (empregado == null) return BadRequest();

                var result = await empregadoRepository.AddEmpregado(empregado);

                return CreatedAtAction(nameof(getEmpregado), new { id = result.EmpId }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no banco de dados");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Empregado>> UpdateEmpregado([FromBody] Empregado empregado)
        {
            try
            {
                var result = await empregadoRepository.GetEmpregado(empregado.EmpId);
                if (result == null) return NotFound($"Empregado chamado = {empregado.Nome} não encontrado");

                return await empregadoRepository.UpdateEmpregado(empregado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no banco de dados");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Empregado>> DeleteEmpregado(int id)
        {
            try
            {
                var result = await empregadoRepository.GetEmpregado(id);
                if (result == null) return NotFound($"Empregado com id = {id} não encontrado");

                empregadoRepository.DeleteEmpregado(id);

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no banco de dados");
            }
        }

        // Novo método para associar um empregado a um departamento
        [HttpPost("AssociarEmpregado")]
        public async Task<ActionResult<Empregado>> AssociarEmpregadoAoDepartamento(int empId, int depId)
        {
            try
            {
                var result = await empregadoRepository.AssociarEmpregadoAoDepartamento(empId, depId);
                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao associar empregado ao departamento: {ex.Message}");
            }
        }
    }
}

