using api_gestao_vendas_brigadeiros.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_gestao_vendas_brigadeiros.Models;

namespace api_gestao_vendas_brigadeiros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //Os controller são responsáveis por lidar com as requisições HTTP e retornar as respostas.
        private readonly ClienteRepository repository;

        public ClienteController(IConfiguration configuration)
        {
            repository = new ClienteRepository(configuration);
        }

        [HttpGet("BuscasTodosClientes")]
        public IActionResult BuscarTodosClientes()
        {
            try
            {
                IEnumerable<Cliente> listaRetorno = repository.BuscarTodosClientes();

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


        [HttpGet("BuscasClientePorNome")]
        public IActionResult BuscarClientePorNome(string nome)
        {
            try
            {
                var listaRetorno = repository.BuscarClientePorNome(nome);

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

        [HttpPost("InserirDadosCliente")]
        public IActionResult InserirDadosPessoa([FromBody] Cliente cliente)
        {

            if (cliente == null)
            {
                return BadRequest("Dados da cliente inválidos");
            }
            try
            {
                var listaRetorno = repository.InserirDadosCliente(cliente);

                if (listaRetorno > 0)
                {
                    return Ok("Dados inseridos com sucesso");
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


        [HttpPut("{id}")]
        public IActionResult AtualizarDadosPessoa(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Dados do cliente estão ausentes.");
            }

            var linhasRetorno = repository.AtualizacaoDeCliente(cliente, id);

            if (linhasRetorno > 0)
            {
                return Ok("Dados atualizados com sucesso!");
            }
            else
            {
                return NotFound("Pessoa não encontrada!");

            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirPessoa(int id)
        {
            try
            {
                int numLinhas = repository.ExcluirDadosCliente(id);
                return Ok($"Número de linhas excluídas: {numLinhas}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
