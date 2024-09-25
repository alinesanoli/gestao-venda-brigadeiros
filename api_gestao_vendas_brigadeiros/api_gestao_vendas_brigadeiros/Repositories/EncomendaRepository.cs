using api_gestao_vendas_brigadeiros.Models;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace api_gestao_vendas_brigadeiros.Repositories
{
    public class EncomendaRepository
    {
        private readonly SqlConnection connection;

        public EncomendaRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbGestaoBrigadeiros") ??
                throw new Exception("ConnectionString sem valor válido");
            connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Encomenda>? BuscarTodasAsEncomendas()
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Encomendas";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                using SqlDataReader reader = command.ExecuteReader();

                List<Encomenda> listaRetorno = new List<Encomenda>();

                while (reader.Read())
                {
                    Encomenda encomenda = new Encomenda(reader);
                    listaRetorno.Add(encomenda);
                }
                return listaRetorno;

            }
            finally
            {
                connection.Close();
            }
        }

        public IList<Encomenda> BuscarEncomendaPorId(int Id)
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Encomendas
                             WHERE IdEncomenda = @Id";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Id", Id);

                using SqlDataReader reader = command.ExecuteReader();

                List<Encomenda> listaRetorno = new List<Encomenda>();

                while (reader.Read())
                {
                    Encomenda encomenda = new Encomenda(reader);
                    listaRetorno.Add(encomenda);
                }
                return listaRetorno;
            }
            finally
            {
                connection.Close();
            }
        
        }

        
        public int InserirEncomenda(Encomenda encomenda)
        {
            try
            {
                connection.Open();
                string sql = "INSERT INTO Encomendas (IdCliente, DataSolicitacao, DataEntrega, ValorTotal, FormaPagamento)" +
                    "OUTPUT INSERTED.IdEncomenda " + //retorna o IdEncomenda gerado
                    " VALUES (@IdCliente, @DataSolicitacao, @DataEntrega, @ValorTotal, @FormaPagamento)";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCliente", encomenda.IdCliente);
                command.Parameters.AddWithValue("@DataSolicitacao", encomenda.DataSolicitacao);
                command.Parameters.AddWithValue("@DataEntrega", encomenda.DataEntrega);
                command.Parameters.AddWithValue("ValorTotal", encomenda.ValorTotal);
                command.Parameters.AddWithValue("@FormaPagamento", encomenda.FormaPagamento);

                // Executa o comando e retorna o IdEncomenda gerado
                int idEncomenda = (int)command.ExecuteScalar();

                // Insere os brigadeiros correspondentes na tabela EncomendaBrigadeiros
                foreach (var brigadeiro in encomenda.EncomendaBrigadeiro)
                {
                    string sqlBrigadeiro = "INSERT INTO EncomendaBrigadeiros (IdEncomenda, IdBrigadeiro, Quantidade)" +
                                           " VALUES (@IdEncomenda, @IdBrigadeiro, @Quantidade)";

                    using SqlCommand commandBrigadeiro = new SqlCommand(sqlBrigadeiro, connection);
                    commandBrigadeiro.Parameters.AddWithValue("@IdEncomenda", idEncomenda);
                    commandBrigadeiro.Parameters.AddWithValue("@IdBrigadeiro", brigadeiro.IdBrigadeiro);
                    commandBrigadeiro.Parameters.AddWithValue("@Quantidade", brigadeiro.Quantidade);

                    commandBrigadeiro.ExecuteNonQuery();
                }



                return idEncomenda;
            }
            finally
            {
                connection.Close();
            }
        }

        public int AtualizarEncomenda(Encomenda encomenda, int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlUpd = "UPDATE Encomendas SET IdCliente = @IdCliente, DataSolicitacao = @DataSolicitacao, QuantidadeComprada = @QuantidadeComprada, DataCompra = @DataCompra" +
                    "WHERE IdGasto = @Id";
                using SqlCommand commandUpd = new SqlCommand(sqlUpd, connection);
                commandUpd.Parameters.AddWithValue("@IdCliente", encomenda.IdCliente);
                commandUpd.Parameters.AddWithValue("@DataSolicitacao", encomenda.DataSolicitacao);
                commandUpd.Parameters.AddWithValue("@DataEntrega", encomenda.DataEntrega);
                commandUpd.Parameters.AddWithValue("ValorTotal", encomenda.ValorTotal);
                commandUpd.Parameters.AddWithValue("FormaPagamento", encomenda.FormaPagamento);
                commandUpd.Parameters.AddWithValue("@Id", id);

                int numLinhas = commandUpd.ExecuteNonQuery();

                transaction.Complete();
                return numLinhas;

            }
            finally
            {
                connection.Close();
            }

        }

        public int ExcluirEncomenda(int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlUpd = "DELETE FROM Encomendas WHERE IdGasto = @Id";
                using SqlCommand commandUpd = new SqlCommand(sqlUpd, connection);
                commandUpd.Parameters.AddWithValue("@Id", id);

                int numLinhas = commandUpd.ExecuteNonQuery();

                transaction.Complete();
                return numLinhas;

            }
            finally
            {
                connection.Close();
            }

        }

    }
}
