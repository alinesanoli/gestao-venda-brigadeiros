
using api_gestao_vendas_brigadeiros.Repositories;
using api_gestao_vendas_brigadeiros.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_vendas_brigadeiros.Controllers
{
    [Route("api/Venda-Brigadeiro")]
    [ApiController]
    public class VendaBrigadeiroController : ControllerBase
    {
        private readonly VendaBrigadeiroRepository repository;
        private readonly VendaRepository _repository;

        public VendaBrigadeiroController(IConfiguration configuration)
        {
            repository = new VendaBrigadeiroRepository(configuration);
            _repository = new VendaRepository(configuration);
        }

        [HttpGet("BuscarTodosVendasBrigadeiros")]
        public IActionResult BuscarTodosVendasBrigadeiros()
        {
            try
            {
                IEnumerable<Brigadeiro> listaRetorno = repository.BuscarBrigadeiros();

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


        [HttpGet("BuscarBrigadeirosPorVendas/{idVenda}")]
        public IActionResult BuscarBrigadeirosVendas(int idVenda)
        {
            try
            {
                IEnumerable<VendaBrigadeiro> listaRetorno = repository.BuscarBrigadeiroPorVenda(idVenda);

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




        [HttpPost("InserirBrigadeiroAVenda{id}")]
        public IActionResult AdicionarBrigadeiroAVenda(int id, [FromBody] List<VendaBrigadeiro> vendaBrigadeiros)
        {
            var venda = _repository.BuscarVendaPorId(id);
            if ( venda == null)
            {
                return NotFound("Venda não encontrada");
            }
            try
            {
                int totalLinhasAfetadas = 0;

                // Itera sobre a lista de brigadeiros e adiciona à encomenda
                foreach (var vendaBrigadeiro in vendaBrigadeiros)
                {
                    var linhasAfetadas = repository.AdicionarBrigadeiroAVenda(id, vendaBrigadeiro.IdBrigadeiro, vendaBrigadeiro.Quantidade);
                    totalLinhasAfetadas += linhasAfetadas;
                }


                if (totalLinhasAfetadas > 0)
                {
                    return Ok("Brigadeiro adicionados à venda com sucesso");
                }
                else
                {
                    return StatusCode(500, "Erro ao adicionar à venda");


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    
    }
}
