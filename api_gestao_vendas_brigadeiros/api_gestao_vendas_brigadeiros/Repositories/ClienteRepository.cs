using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using api_gestao_vendas_brigadeiros.Models;

namespace api_gestao_vendas_brigadeiros.Repositories
{
    public class ClienteRepository
    {
        //Os repositories abstraem a lógica de acesso ao banco de dados, realizando operações de CRUD.
        private readonly SqlConnection connection;

        public ClienteRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbGestaoBrigadeiros") ??
                throw new Exception("ConnectionString sem valor válido");
            connection = new SqlConnection(connectionString);
        }

        #region | Método de Busca |
        public IEnumerable<Cliente>? BuscarTodosClientes()
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Clientes";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                using SqlDataReader reader = command.ExecuteReader();

                List<Cliente> listaRetorno = new List<Cliente>();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente(reader);
                    listaRetorno.Add(cliente);
                }
                return listaRetorno;
            }
            finally
            {
                connection.Close();
            }

        }

        public IList<Cliente>? BuscarClientePorNome(string nome)
        {

            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Clientes
                             WHERE Nome LIKE @Nome";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Nome", "%" + nome + "%");

                using SqlDataReader reader = command.ExecuteReader();

                List<Cliente> listaRetorno = new List<Cliente>();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente(reader);
                    listaRetorno.Add(cliente);
                }
                return listaRetorno;
            }
            finally
            {
                connection.Close();
            }

        }

        #endregion

        #region | Método de inserção | 
        public int InserirDadosCliente(Cliente cliente)
        {
            try
            {
                connection.Open();
                string sql = "INSERT INTO Clientes (Nome, Telefone, Endereco, Email)" +
                    " VALUES (@Nome, @Telefone, @Endereco, @Email)";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                command.Parameters.AddWithValue("@Email", cliente.Email);
               


                return command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region | Método de atualização | 
        public int AtualizacaoDeCliente(Cliente cliente, int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlUpd = "UPDATE Clientes SET Nome = @Nome, Telefone = @Telefone, Endereco = @Endereco," +
                    "Email = @Email WHERE IdCliente = @Id";
                using SqlCommand commandUpd = new SqlCommand(sqlUpd, connection);
                commandUpd.Parameters.AddWithValue("@Nome", cliente.Nome);
                commandUpd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                commandUpd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                commandUpd.Parameters.AddWithValue("@Email", cliente.Email);
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

        #region | Método de exclusão | 
        public int ExcluirDadosCliente(int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlDelete = "DELETE FROM Clientes WHERE IdCliente = @Id";
                using SqlCommand command = new SqlCommand(sqlDelete, connection);
                command.Parameters.AddWithValue("@Id", id);
                int numLinhas = command.ExecuteNonQuery();

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
