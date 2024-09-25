using api_gestao_vendas_brigadeiros.Models;
using api_gestao_vendas_brigadeiros.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_vendas_brigadeiros.Controllers
{
    [Route("api/BrigadeirosEncomenda")]
    [ApiController]
    public class EncomendaBrigadeiroController : ControllerBase
    {
        private readonly EncomendaBrigadeiroRepository repository;
        private readonly EncomendaRepository _repository;

        public EncomendaBrigadeiroController(IConfiguration configuration)
        {
            repository = new EncomendaBrigadeiroRepository(configuration);
            _repository = new EncomendaRepository(configuration);
        }

        [HttpGet("BuscarBrigadeirosPorEncomenda/{idEncomenda}")]
        public IActionResult BuscarBrigadeirosVendas(int idEncomenda)
        {
            try
            {
                IEnumerable<EncomendaBrigadeiro> listaRetorno = repository.BuscarBrigadeiroPorEncomenda(idEncomenda);

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




        [HttpPost("InserirBrigadeiroAEncomenda/{id}")]
        public IActionResult AdicionarBrigadeiroAVenda(int id, [FromBody] List<EncomendaBrigadeiro> encomendaBrigadeiros)
        {
            var encomenda = _repository.BuscarEncomendaPorId(id);
            if (encomenda == null)
            {
                return NotFound("Encomenda não encontrada");
            }
            try
            {
                int totalLinhasAfetadas = 0;

                // Itera sobre a lista de brigadeiros e adiciona à encomenda
                foreach (var encomendaBrigadeiro in encomendaBrigadeiros)
                {
                    var linhasAfetadas = repository.AdicionarBrigadeiroAEncomenda(id, encomendaBrigadeiro.IdBrigadeiro, encomendaBrigadeiro.Quantidade);
                    totalLinhasAfetadas += linhasAfetadas;
                }

               
                if (totalLinhasAfetadas > 0)
                {
                    return Ok("Brigadeiro adicionados à encomenda com sucesso");
                }
                else
                {
                    return StatusCode(500, "Erro ao adicionar à encomenda");


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
