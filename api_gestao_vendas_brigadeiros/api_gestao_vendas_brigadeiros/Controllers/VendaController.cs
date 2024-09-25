using api_gestao_vendas_brigadeiros.Models;
using api_gestao_vendas_brigadeiros.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Runtime.Intrinsics.X86;

namespace api_gestao_vendas_brigadeiros.Controllers
{
    [Route("api/Venda")]
    [ApiController]
    public class VendaController : ControllerBase
    {

        //Os controller são responsáveis por lidar com as requisições HTTP e retornar as respostas.
        private readonly VendaRepository repository;

        public VendaController(IConfiguration configuration)
        {
            repository = new VendaRepository(configuration);
        }

        [HttpGet("BuscarTodasVendas")]
        public IActionResult BuscarTodasAsVendas()
        {
            try
            {
                IEnumerable<Venda> listaRetorno = repository.BuscarTodasAsVendas();

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


        [HttpGet("BuscarVendaPorId")]
        public IActionResult BuscarVendaPorId(int id)
        {
            try
            {
                var listaRetorno = repository.BuscarVendaPorId(id);

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


        [HttpGet("BuscarVendaPorData")]
        public IActionResult BuscarPorData(DateTime data)
        {
            try
            {
                var listaRetorno = repository.BuscarPorData(data);

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

        [HttpPost("InserirDadosVendas")]
        public IActionResult InserirDadosVenda([FromBody] Venda venda)
        {

            if (venda == null)
            {
                return BadRequest("Venda não localizada");
            }
            try
            {
                var listaRetorno = repository.InserirVendas(venda);

                if (listaRetorno > 0)
                {
                    // Retorna o id da encomenda criada e uma resposta HTTP 201(Created)
                    return CreatedAtAction(nameof(BuscarVendaPorId), new { id = listaRetorno }, venda);
                }
                else
                {
                    return StatusCode(500, "Erro ao inserir dados");


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("AtualizarVenda/{id}")]
        public IActionResult AtualizarVenda( [FromBody] Venda venda, int id)
        {
            if (venda == null)
            {
                return BadRequest("Venda não localizada.");
            }

            var linhasRetorno = repository.AtualizacaoDeVenda(venda, id);

            if (linhasRetorno > 0)
            {
                return Ok("Dados atualizados com sucesso!");
            }
            else
            {
                return NotFound("Venda não encontrada!");

            }
        }

        [HttpDelete("ExcluirVenda/{id}")]
        public IActionResult ExcluirVenda(int id)
        {
            try
            {
                int numLinhas = repository.ExcluirVenda(id);
                return Ok($"Número de linhas excluídas: {numLinhas}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
