using api_gestao_vendas_brigadeiros.Models;
using api_gestao_vendas_brigadeiros.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_vendas_brigadeiros.Controllers
{
    [Route("api/GastosBrigadeiro")]
    [ApiController]
    public class GastoController : ControllerBase
    {

        private readonly GastoRepository repository;

        public GastoController(IConfiguration configuration)
        {
            repository = new GastoRepository(configuration);
        }

        [HttpGet("BuscarTodosOsGastos")]
        public IActionResult BuscarTodosOsGastos()
        {


            try
            {
                IEnumerable<Gasto> listaRetorno = repository.BuscarTodosOsGastos();

                if (listaRetorno.Any())
                {
                    return Ok(listaRetorno);
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("InserirGasto")]
        public IActionResult InserirGasto([FromBody] Gasto gasto)
        {
            if (gasto == null)
            {
                return BadRequest("Os gastos inseridos são inválidos");
            }
            try
            {
                var listaRetorno = repository.InserirGasto(gasto);

                if (listaRetorno > 0)
                {
                    return Ok("Gastos inseridos com sucesso");
                }
                else
                {
                    return StatusCode(500, "Erro ao inserir gastos");


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("AtualizarGasto/{id}")]
        public IActionResult AtualizarGasto([FromBody] Gasto gasto, int id)
        {
            if (gasto == null)
            {
                return BadRequest("Gasto não localizado.");
            }

            var linhasRetorno = repository.AtualizarGasto(gasto, id);

            if (linhasRetorno > 0)
            {
                return Ok("Dados atualizados com sucesso!");
            }
            else
            {
                return NotFound("Gasto não encontrado!");

            }
        }

        [HttpDelete("ExcluirGasto/{id}")]
        public IActionResult ExcluirGasto(int id)
        {
            try
            {
                int numLinhas = repository.ExcluirGasto(id);
                return Ok($"Número de linhas excluídas: {numLinhas}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }

}

