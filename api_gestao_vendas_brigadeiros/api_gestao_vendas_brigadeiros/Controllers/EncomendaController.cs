using api_gestao_vendas_brigadeiros.Models;
using api_gestao_vendas_brigadeiros.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_vendas_brigadeiros.Controllers
{
    [Route("api/Encomenda")]
    [ApiController]
    public class EncomendaController : ControllerBase
    {

        private readonly EncomendaRepository repository;

        public EncomendaController(IConfiguration configuration)
        {
            repository = new EncomendaRepository(configuration);
        }

        [HttpGet("BuscarTodasAsEncomendas")]
        public IActionResult BuscarTodasAsEncomendas()
        {


            try
            {
                IEnumerable<Encomenda> listaRetorno = repository.BuscarTodasAsEncomendas();

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

        [HttpGet("BuscasEncomendaPorId")]
        public IActionResult BuscarEncomendaPorId(int id)
        {
            try
            {
                var listaRetorno = repository.BuscarEncomendaPorId(id);

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


        [HttpPost("InserirEncomenda")]
        public IActionResult InserirEncomenda([FromBody] Encomenda encomenda)
        {
            if (encomenda == null || encomenda.EncomendaBrigadeiro == null)
            {
                return BadRequest("Dados da encomenda inválidos ou sem brigadeiros.");
            }
            try
            {
                var listaRetorno = repository.InserirEncomenda(encomenda);

                if (listaRetorno > 0)
                {

                    // Retorna o id da encomenda criada e uma resposta HTTP 201 (Created)
                    return CreatedAtAction(nameof(BuscarEncomendaPorId), new { id = listaRetorno }, encomenda);

                }

                else
                {
                    return StatusCode(500, "Erro ao inserir encomenda");


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("AtualizarEncomenda/{id}")]
        public IActionResult AtualizarGasto([FromBody] Encomenda encomenda, int id)
        {
            if (encomenda == null)
            {
                return BadRequest("Encomenda não localizada.");
            }

            var linhasRetorno = repository.AtualizarEncomenda(encomenda, id);

            if (linhasRetorno > 0)
            {
                return Ok("Dados atualizados com sucesso!");
            }
            else
            {
                return NotFound("Encomenda não encontrada!");

            }
        }

        [HttpDelete("ExcluirEncomenda/{id}")]
        public IActionResult ExcluirEncomenda(int id)
        {
            try
            {
                int numLinhas = repository.ExcluirEncomenda(id);
                return Ok($"Número de linhas excluídas: {numLinhas}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
