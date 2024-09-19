using api_gestao_vendas_brigadeiros.Models;
using api_gestao_vendas_brigadeiros.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_vendas_brigadeiros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastoController : ControllerBase
    {

        private readonly GastoRepository repository;

        public GastoController(IConfiguration configuration)
        {
            repository = new GastoRepository(configuration);
        }

        [HttpGet("BuscasTodosOsGastos")]
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

    }
}
