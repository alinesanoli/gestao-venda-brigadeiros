using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using api_gestao_vendas_brigadeiros.Models;

namespace api_gestao_vendas_brigadeiros.Repositories
{
    public class VendaRepository
    {
        //Os repositories abstraem a lógica de acesso ao banco de dados, realizando operações de CRUD.
        private readonly SqlConnection connection;

      
        public VendaRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbGestaoBrigadeiros") ??
                throw new Exception("ConnectionString sem valor válido");
            connection = new SqlConnection(connectionString);
        }
        #region | Método Busca |
        public IEnumerable<Venda>? BuscarTodasAsVendas()
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Vendas";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                using SqlDataReader reader = command.ExecuteReader();

                List<Venda> listaRetorno = new List<Venda>();

                while (reader.Read()) {
                    Venda venda = new Venda(reader);
                    listaRetorno.Add(venda);
                }
                return listaRetorno;

            }
            finally
            {
                connection.Close();
            }
        }


        public IList<Venda> BuscarPorId(int Id)
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Vendas
                             WHERE IdVenda = @Id";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Id", Id );

                using SqlDataReader reader = command.ExecuteReader();

                List<Venda> listaRetorno = new List<Venda>();

                while (reader.Read())
                {
                    Venda venda = new Venda(reader);
                    listaRetorno.Add(venda);
                }
                return listaRetorno;
            }
            finally
            {
                connection.Close();
            }

        }

        public IList<Venda> BuscarPorData(DateTime data)
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Vendas
                             WHERE DataVenda = @Data";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Data", data);

                using SqlDataReader reader = command.ExecuteReader();

                List<Venda> listaRetorno = new List<Venda>();

                while (reader.Read())
                {
                    Venda venda = new Venda(reader);
                    listaRetorno.Add(venda);
                }
                return listaRetorno;
            }
            finally
            {
                connection.Close();
            }

        }
        #endregion
        #region |Método de inserção |
        public int InserirVendas(Venda venda)
        {
            try
            {
                connection.Open();
                string sql = "INSERT INTO Vendas (IdCliente, DataVenda, ValorTotal, FormaPagamento)" +
                    " VALUES (@IdCliente, @DataVenda, @ValorTotal, @FormaPagamento)";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCliente", venda.IdCliente);
                command.Parameters.AddWithValue("@DataVenda", venda.DataVendaFormatada);
                command.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
                command.Parameters.AddWithValue("@FormaPagamento", venda.FormaPagamento);


                return command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region | Método de atualização | 
        public int AtualizacaoDeVenda(Venda venda, int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlUpd = "UPDATE Vendas SET IdCliente = @IdCliente, DataVenda = @DataVenda, ValorTotal = @ValorTotal, FormaPagamento = @FormaPagamento" +
                    "WHERE IdVenda = @Id";
                using SqlCommand commandUpd = new SqlCommand(sqlUpd, connection);
                commandUpd.Parameters.AddWithValue("@IdCliente", venda.IdCliente);
                commandUpd.Parameters.AddWithValue("@DataVenda", venda.DataVendaFormatada);
                commandUpd.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
                commandUpd.Parameters.AddWithValue("@FormaPagamento", venda.FormaPagamento);
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
        #endregion

        #region | Método de Exclusão | 
        public int ExcluirVenda(int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlUpd = "DELETE FROM Vendas WHERE IdVenda = @Id";
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
        #endregion
      
    }
}

